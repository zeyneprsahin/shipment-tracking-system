namespace ShipmentTracking.Domain.Enums;

public enum ShipmentStatus
{
    Preparing = 1,
    Shipped = 2,
    InTransit = 3,
    OutForDelivery = 4,
    DeliveryFailed = 5,
    Delivered = 6,
    Cancelled = 7,
    ReturnRequested = 8,
    Returning = 9,
    Returned = 10,

    ReturningToSender = 11,
    ReturnedToSender = 12
        //"paketin gönderene dönmesi"ni süreç olarak düþünüyorum.
}
