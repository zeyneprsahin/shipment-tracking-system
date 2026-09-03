using ShipmentTracking.Domain.Enums;
using ShipmentTracking.Domain.Exceptions;

namespace ShipmentTracking.Domain.Entities;

public class Shipment
{
    private readonly List<ShipmentStatusHistory> _statusHistory = new();

    private Shipment() { }

    public Shipment(
        string trackingNumber,
        string recipientName,
        string address,
        string phoneNumber,
        string packageInfo,
        string? email,
        string createdBy,
        DateTime createdAtUtc)
    {
        if (string.IsNullOrWhiteSpace(trackingNumber)) throw new DomainRuleException("Tracking number cannot be empty.");
        if (string.IsNullOrWhiteSpace(createdBy)) throw new DomainRuleException("Created by cannot be empty.");

        Id = Guid.NewGuid();
        TrackingNumber = trackingNumber;
        RecipientName = recipientName.Trim();
        Address = address.Trim();
        PhoneNumber = phoneNumber.Trim();
        PackageInfo = packageInfo.Trim();
        Email = string.IsNullOrWhiteSpace(email) ? null : email.Trim();
        Status = ShipmentStatus.Preparing;
        CreatedAtUtc = createdAtUtc;

        _statusHistory.Add(new ShipmentStatusHistory(Id, null, ShipmentStatus.Preparing, createdBy.Trim(), createdAtUtc));
    }

    public Guid Id { get; private set; }
    public string TrackingNumber { get; private set; } = string.Empty;
    public string RecipientName { get; private set; } = string.Empty;
    public string Address { get; private set; } = string.Empty;
    public string PhoneNumber { get; private set; } = string.Empty;
    public string PackageInfo { get; private set; } = string.Empty;
    public string? Email { get; private set; }
    public ShipmentStatus Status { get; private set; }
    public DateTime CreatedAtUtc { get; private set; }
    public IReadOnlyCollection<ShipmentStatusHistory> StatusHistory => _statusHistory.AsReadOnly();

    private static readonly IReadOnlyDictionary<ShipmentStatus, ShipmentStatus[]> AllowedTransitions =
        new Dictionary<ShipmentStatus, ShipmentStatus[]>
        {
            [ShipmentStatus.Preparing] = new[] { ShipmentStatus.Shipped, ShipmentStatus.Cancelled },
            [ShipmentStatus.Shipped] = new[] { ShipmentStatus.InTransit },
            [ShipmentStatus.InTransit] = new[] { ShipmentStatus.OutForDelivery },
            [ShipmentStatus.OutForDelivery] =
    new[] { ShipmentStatus.Delivered, ShipmentStatus.DeliveryFailed },

            [ShipmentStatus.DeliveryFailed] =
    new[]
    {
        ShipmentStatus.OutForDelivery,
        ShipmentStatus.ReturningToSender
    },

            [ShipmentStatus.ReturningToSender] =
    new[] { ShipmentStatus.ReturnedToSender },

            [ShipmentStatus.ReturnedToSender] =
    Array.Empty<ShipmentStatus>(),

            [ShipmentStatus.Delivered] =
    new[] { ShipmentStatus.ReturnRequested },
            [ShipmentStatus.ReturnRequested] = new[] { ShipmentStatus.Returning },
            [ShipmentStatus.Returning] = new[] { ShipmentStatus.Returned },
            [ShipmentStatus.Cancelled] = Array.Empty<ShipmentStatus>(),
            [ShipmentStatus.Returned] = Array.Empty<ShipmentStatus>()
        };

    public void ChangeStatus(ShipmentStatus newStatus, string changedBy, DateTime changedAtUtc)
    {
        if (string.IsNullOrWhiteSpace(changedBy))
            throw new DomainRuleException("The user who changes the status must be recorded.");

        if (newStatus == Status)
            throw new DomainRuleException($"Shipment is already in {Status} status.");

        if (!AllowedTransitions.TryGetValue(Status, out var allowed) || !allowed.Contains(newStatus))
            throw new DomainRuleException($"Invalid status transition: {Status} -> {newStatus}.");

        var oldStatus = Status;
        Status = newStatus;
        _statusHistory.Add(new ShipmentStatusHistory(Id, oldStatus, newStatus, changedBy.Trim(), changedAtUtc));
    }
}
