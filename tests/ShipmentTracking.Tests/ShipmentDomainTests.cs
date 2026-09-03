using Xunit;
using ShipmentTracking.Domain.Entities;
using ShipmentTracking.Domain.Enums;
using ShipmentTracking.Domain.Exceptions;


namespace ShipmentTracking.Tests;

public class ShipmentDomainTests
{
    private static Shipment CreateShipment()
        => new("SHP-TEST-001", "Ada Lovelace", "Test Address", "5550000000", "Small box", null, "employee-1", DateTime.UtcNow);

    [Fact]
    public void New_shipment_starts_as_preparing_and_writes_history()
    {
        var shipment = CreateShipment();
        Assert.Equal(ShipmentStatus.Preparing, shipment.Status);
        Assert.Single(shipment.StatusHistory);
        Assert.Equal(ShipmentStatus.Preparing, shipment.StatusHistory.Single().NewStatus);
    }

    [Fact]
    public void Normal_flow_can_reach_delivered()
    {
        var shipment = CreateShipment();
        shipment.ChangeStatus(ShipmentStatus.Shipped, "employee-1", DateTime.UtcNow);
        shipment.ChangeStatus(ShipmentStatus.InTransit, "employee-1", DateTime.UtcNow);
        shipment.ChangeStatus(ShipmentStatus.OutForDelivery, "employee-1", DateTime.UtcNow);
        shipment.ChangeStatus(ShipmentStatus.Delivered, "employee-1", DateTime.UtcNow);
        Assert.Equal(ShipmentStatus.Delivered, shipment.Status);
        Assert.Equal(5, shipment.StatusHistory.Count);
    }

    [Fact]
    public void Invalid_transition_is_rejected()
    {
        var shipment = CreateShipment();
        Assert.Throws<DomainRuleException>(() =>
            shipment.ChangeStatus(ShipmentStatus.Delivered, "employee-1", DateTime.UtcNow));
    }

    [Fact]
    public void Shipment_can_only_be_cancelled_from_preparing()
    {
        var shipment = CreateShipment();
        shipment.ChangeStatus(ShipmentStatus.Shipped, "employee-1", DateTime.UtcNow);
        Assert.Throws<DomainRuleException>(() =>
            shipment.ChangeStatus(ShipmentStatus.Cancelled, "employee-1", DateTime.UtcNow));
    }

    [Fact]
    public void Cancelled_shipment_cannot_be_shipped_again()
    {
        var shipment = CreateShipment();
        shipment.ChangeStatus(ShipmentStatus.Cancelled, "employee-1", DateTime.UtcNow);
        Assert.Throws<DomainRuleException>(() =>
            shipment.ChangeStatus(ShipmentStatus.Shipped, "employee-1", DateTime.UtcNow));
    }

    [Fact]
    public void Delivery_failed_can_return_to_out_for_delivery()
    {
        var shipment = CreateShipment();
        shipment.ChangeStatus(ShipmentStatus.Shipped, "employee-1", DateTime.UtcNow);
        shipment.ChangeStatus(ShipmentStatus.InTransit, "employee-1", DateTime.UtcNow);
        shipment.ChangeStatus(ShipmentStatus.OutForDelivery, "employee-1", DateTime.UtcNow);
        shipment.ChangeStatus(ShipmentStatus.DeliveryFailed, "employee-1", DateTime.UtcNow);
        shipment.ChangeStatus(ShipmentStatus.OutForDelivery, "employee-1", DateTime.UtcNow);
        Assert.Equal(ShipmentStatus.OutForDelivery, shipment.Status);
    }

    [Fact]
    public void Delivered_shipment_can_only_enter_return_process()
    {
        var shipment = CreateShipment();
        shipment.ChangeStatus(ShipmentStatus.Shipped, "employee-1", DateTime.UtcNow);
        shipment.ChangeStatus(ShipmentStatus.InTransit, "employee-1", DateTime.UtcNow);
        shipment.ChangeStatus(ShipmentStatus.OutForDelivery, "employee-1", DateTime.UtcNow);
        shipment.ChangeStatus(ShipmentStatus.Delivered, "employee-1", DateTime.UtcNow);

        Assert.Throws<DomainRuleException>(() =>
            shipment.ChangeStatus(ShipmentStatus.Preparing, "employee-1", DateTime.UtcNow));

        shipment.ChangeStatus(ShipmentStatus.ReturnRequested, "employee-1", DateTime.UtcNow);
        shipment.ChangeStatus(ShipmentStatus.Returning, "employee-1", DateTime.UtcNow);
        shipment.ChangeStatus(ShipmentStatus.Returned, "employee-1", DateTime.UtcNow);
        Assert.Equal(ShipmentStatus.Returned, shipment.Status);
    }
    [Fact]
    public void Delivery_failed_shipment_can_be_returned_to_sender()
    {
        var shipment = CreateShipment();

        shipment.ChangeStatus(
            ShipmentStatus.Shipped,
            "employee-1",
            DateTime.UtcNow);

        shipment.ChangeStatus(
            ShipmentStatus.InTransit,
            "employee-1",
            DateTime.UtcNow);

        shipment.ChangeStatus(
            ShipmentStatus.OutForDelivery,
            "employee-1",
            DateTime.UtcNow);

        shipment.ChangeStatus(
            ShipmentStatus.DeliveryFailed,
            "employee-1",
            DateTime.UtcNow);

        shipment.ChangeStatus(
            ShipmentStatus.ReturningToSender,
            "employee-1",
            DateTime.UtcNow);

        shipment.ChangeStatus(
            ShipmentStatus.ReturnedToSender,
            "employee-1",
            DateTime.UtcNow);

        Assert.Equal(
            ShipmentStatus.ReturnedToSender,
            shipment.Status);
    }

    //kötü geçiþ test

    [Fact]
    public void Returned_to_sender_shipment_cannot_go_out_for_delivery_again()
    {
        var shipment = CreateShipment();

        shipment.ChangeStatus(
            ShipmentStatus.Shipped,
            "employee-1",
            DateTime.UtcNow);

        shipment.ChangeStatus(
            ShipmentStatus.InTransit,
            "employee-1",
            DateTime.UtcNow);

        shipment.ChangeStatus(
            ShipmentStatus.OutForDelivery,
            "employee-1",
            DateTime.UtcNow);

        shipment.ChangeStatus(
            ShipmentStatus.DeliveryFailed,
            "employee-1",
            DateTime.UtcNow);

        shipment.ChangeStatus(
            ShipmentStatus.ReturningToSender,
            "employee-1",
            DateTime.UtcNow);

        shipment.ChangeStatus(
            ShipmentStatus.ReturnedToSender,
            "employee-1",
            DateTime.UtcNow);

        Assert.Throws<DomainRuleException>(() =>
            shipment.ChangeStatus(
                ShipmentStatus.OutForDelivery,
                "employee-1",
                DateTime.UtcNow));
    }
}
