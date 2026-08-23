using ShipmentTracking.Domain.Enums;

namespace ShipmentTracking.Domain.Entities;

public class ShipmentStatusHistory
{
    private ShipmentStatusHistory() { }

    internal ShipmentStatusHistory(Guid shipmentId, ShipmentStatus? oldStatus, ShipmentStatus newStatus, string changedBy, DateTime changedAtUtc)
    {
        Id = Guid.NewGuid();
        ShipmentId = shipmentId;
        OldStatus = oldStatus;
        NewStatus = newStatus;
        ChangedBy = changedBy;
        ChangedAtUtc = changedAtUtc;
    }

    public Guid Id { get; private set; }
    public Guid ShipmentId { get; private set; }
    public ShipmentStatus? OldStatus { get; private set; }
    public ShipmentStatus NewStatus { get; private set; }
    public string ChangedBy { get; private set; } = string.Empty;
    public DateTime ChangedAtUtc { get; private set; }
}
