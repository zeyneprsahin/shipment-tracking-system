using ShipmentTracking.Domain.Enums;

namespace ShipmentTracking.Application.DTOs;

public sealed record ShipmentDto(
    Guid Id,
    string TrackingNumber,
    string RecipientName,
    string Address,
    string PhoneNumber,
    string PackageInfo,
    string? Email,
    ShipmentStatus Status,
    DateTime CreatedAtUtc,
    IReadOnlyCollection<ShipmentHistoryDto> StatusHistory);
