using ShipmentTracking.Domain.Entities;
using ShipmentTracking.Domain.Enums;

namespace ShipmentTracking.Application.Interfaces;

public interface IShipmentRepository
{
    Task AddAsync(Shipment shipment, CancellationToken cancellationToken = default);
    Task<Shipment?> GetByTrackingNumberAsync(string trackingNumber, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Shipment>> GetAllAsync(ShipmentStatus? status = null, CancellationToken cancellationToken = default);
    Task<bool> TrackingNumberExistsAsync(string trackingNumber, CancellationToken cancellationToken = default);
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}
