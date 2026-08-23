namespace ShipmentTracking.Application.DTOs;

public sealed record CreateShipmentRequest(
    string RecipientName,
    string Address,
    string PhoneNumber,
    string PackageInfo,
    string? Email,
    string CreatedBy);
