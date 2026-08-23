using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ShipmentTracking.Application.Interfaces;
using ShipmentTracking.Infrastructure.Data;
using ShipmentTracking.Infrastructure.Repositories;

namespace ShipmentTracking.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<AppDbContext>(options => options.UseSqlite(connectionString));
        services.AddScoped<IShipmentRepository, ShipmentRepository>();
        services.AddSingleton<ITrackingNumberGenerator, TrackingNumberGenerator>();
        return services;
    }
}
