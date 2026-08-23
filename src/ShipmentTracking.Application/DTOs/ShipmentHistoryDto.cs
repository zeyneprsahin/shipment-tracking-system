using ShipmentTracking.Domain.Enums;

namespace ShipmentTracking.Application.DTOs;

public sealed record ShipmentHistoryDto(
    ShipmentStatus? OldStatus,
    ShipmentStatus NewStatus,
    string ChangedBy,
    DateTime ChangedAtUtc);
