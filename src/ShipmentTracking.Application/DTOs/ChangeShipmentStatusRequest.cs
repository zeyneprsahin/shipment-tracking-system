using ShipmentTracking.Domain.Enums;

namespace ShipmentTracking.Application.DTOs;

public sealed record ChangeShipmentStatusRequest(ShipmentStatus NewStatus, string ChangedBy);
