using Microsoft.EntityFrameworkCore;
using ShipmentTracking.Application.Interfaces;
using ShipmentTracking.Domain.Entities;
using ShipmentTracking.Domain.Enums;
using ShipmentTracking.Infrastructure.Data;

namespace ShipmentTracking.Infrastructure.Repositories;

public sealed class ShipmentRepository : IShipmentRepository
{
    private readonly AppDbContext _dbContext;

    public ShipmentRepository(AppDbContext dbContext) => _dbContext = dbContext;

    public Task AddAsync(Shipment shipment, CancellationToken cancellationToken = default)
        => _dbContext.Shipments.AddAsync(shipment, cancellationToken).AsTask();

    public Task<bool> TrackingNumberExistsAsync(string trackingNumber, CancellationToken cancellationToken = default)
        => _dbContext.Shipments.AnyAsync(x => x.TrackingNumber == trackingNumber, cancellationToken);

    public Task<Shipment?> GetByTrackingNumberAsync(string trackingNumber, CancellationToken cancellationToken = default)
        => _dbContext.Shipments
            .Include(x => x.StatusHistory)
            .SingleOrDefaultAsync(x => x.TrackingNumber == trackingNumber, cancellationToken);

    public async Task<IReadOnlyList<Shipment>> GetAllAsync(ShipmentStatus? status = null, CancellationToken cancellationToken = default)
    {
        IQueryable<Shipment> query = _dbContext.Shipments.Include(x => x.StatusHistory).AsNoTracking();
        if (status.HasValue)
            query = query.Where(x => x.Status == status.Value);

        return await query.OrderByDescending(x => x.CreatedAtUtc).ToListAsync(cancellationToken);
    }

    public Task SaveChangesAsync(CancellationToken cancellationToken = default)
        => _dbContext.SaveChangesAsync(cancellationToken);
}
