using ShipmentTracking.Application.Interfaces;

namespace ShipmentTracking.Infrastructure.Repositories;

public sealed class TrackingNumberGenerator : ITrackingNumberGenerator
{
    public string Generate()
        => $"SHP-{DateTime.UtcNow:yyyyMMdd}-{Guid.NewGuid().ToString("N")[..10].ToUpperInvariant()}";
}
