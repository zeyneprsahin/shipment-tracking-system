using ShipmentTracking.Application.DTOs;
using ShipmentTracking.Application.Exceptions;
using ShipmentTracking.Application.Interfaces;
using ShipmentTracking.Domain.Entities;
using ShipmentTracking.Domain.Enums;

namespace ShipmentTracking.Application.Services;

public sealed class ShipmentService
{
    private readonly IShipmentRepository _repository;
    private readonly ITrackingNumberGenerator _trackingNumberGenerator;

    public ShipmentService(IShipmentRepository repository, ITrackingNumberGenerator trackingNumberGenerator)
    {
        _repository = repository;
        _trackingNumberGenerator = trackingNumberGenerator;
    }

    public async Task<ShipmentDto> CreateAsync(CreateShipmentRequest request, CancellationToken cancellationToken = default)
    {
        ValidateCreateRequest(request);

        string trackingNumber;
        do
        {
            trackingNumber = _trackingNumberGenerator.Generate();
        }
        while (await _repository.TrackingNumberExistsAsync(trackingNumber, cancellationToken));

        var shipment = new Shipment(
            trackingNumber,
            request.RecipientName,
            request.Address,
            request.PhoneNumber,
            request.PackageInfo,
            request.Email,
            request.CreatedBy,
            DateTime.UtcNow);

        await _repository.AddAsync(shipment, cancellationToken);
        await _repository.SaveChangesAsync(cancellationToken);
        return Map(shipment);
    }

    public async Task<IReadOnlyList<ShipmentDto>> GetAllAsync(ShipmentStatus? status = null, CancellationToken cancellationToken = default)
        => (await _repository.GetAllAsync(status, cancellationToken)).Select(Map).ToList();

    public async Task<ShipmentDto> GetByTrackingNumberAsync(string trackingNumber, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(trackingNumber))
            throw new ValidationException("Tracking number is required.");

        var shipment = await _repository.GetByTrackingNumberAsync(trackingNumber.Trim(), cancellationToken)
            ?? throw new NotFoundException("Shipment was not found.");

        return Map(shipment);
    }

    public async Task<CustomerTrackingDto> GetCustomerTrackingAsync(
    string trackingNumber,
    CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(trackingNumber))
            throw new ValidationException("Tracking number is required.");

        var shipment = await _repository.GetByTrackingNumberAsync(
            trackingNumber.Trim(),
            cancellationToken)
            ?? throw new NotFoundException("Shipment was not found.");

        var lastUpdatedAtUtc = shipment.StatusHistory
            .OrderByDescending(x => x.ChangedAtUtc)
            .Select(x => x.ChangedAtUtc)
            .FirstOrDefault();

        return new CustomerTrackingDto(
            shipment.TrackingNumber,
            shipment.Status,
            lastUpdatedAtUtc);
    }
    public async Task<ShipmentDto> ChangeStatusAsync(string trackingNumber, ChangeShipmentStatusRequest request, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(request.ChangedBy))
            throw new ValidationException("ChangedBy is required.");

        var shipment = await _repository.GetByTrackingNumberAsync(trackingNumber.Trim(), cancellationToken)
            ?? throw new NotFoundException("Shipment was not found.");

        shipment.ChangeStatus(request.NewStatus, request.ChangedBy, DateTime.UtcNow);
        await _repository.SaveChangesAsync(cancellationToken);
        return Map(shipment);
    }

    private static void ValidateCreateRequest(CreateShipmentRequest request)
    {
        var missing = new List<string>();
        if (string.IsNullOrWhiteSpace(request.RecipientName)) missing.Add(nameof(request.RecipientName));
        if (string.IsNullOrWhiteSpace(request.Address)) missing.Add(nameof(request.Address));
        if (string.IsNullOrWhiteSpace(request.PhoneNumber)) missing.Add(nameof(request.PhoneNumber));
        if (string.IsNullOrWhiteSpace(request.PackageInfo)) missing.Add(nameof(request.PackageInfo));
        if (string.IsNullOrWhiteSpace(request.CreatedBy)) missing.Add(nameof(request.CreatedBy));

        if (missing.Count > 0)
            throw new ValidationException($"Required fields are missing: {string.Join(", ", missing)}.");
    }

    private static ShipmentDto Map(Shipment shipment) => new(
        shipment.Id,
        shipment.TrackingNumber,
        shipment.RecipientName,
        shipment.Address,
        shipment.PhoneNumber,
        shipment.PackageInfo,
        shipment.Email,
        shipment.Status,
        shipment.CreatedAtUtc,
        shipment.StatusHistory
            .OrderBy(x => x.ChangedAtUtc)
            .Select(x => new ShipmentHistoryDto(x.OldStatus, x.NewStatus, x.ChangedBy, x.ChangedAtUtc))
            .ToList());
}
