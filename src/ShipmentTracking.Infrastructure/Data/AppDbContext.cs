using Microsoft.EntityFrameworkCore;
using ShipmentTracking.Domain.Entities;

namespace ShipmentTracking.Infrastructure.Data;

public sealed class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Shipment> Shipments => Set<Shipment>();
    public DbSet<ShipmentStatusHistory> ShipmentStatusHistories => Set<ShipmentStatusHistory>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var shipment = modelBuilder.Entity<Shipment>();
        shipment.HasKey(x => x.Id);
        shipment.HasIndex(x => x.TrackingNumber).IsUnique();
        shipment.Property(x => x.TrackingNumber).HasMaxLength(40).IsRequired();
        shipment.Property(x => x.RecipientName).HasMaxLength(150).IsRequired();
        shipment.Property(x => x.Address).HasMaxLength(500).IsRequired();
        shipment.Property(x => x.PhoneNumber).HasMaxLength(50).IsRequired();
        shipment.Property(x => x.PackageInfo).HasMaxLength(500).IsRequired();
        shipment.Property(x => x.Email).HasMaxLength(200);
        shipment.Property(x => x.Status).HasConversion<string>().HasMaxLength(40).IsRequired();
        shipment.HasMany(x => x.StatusHistory)
            .WithOne()
            .HasForeignKey(x => x.ShipmentId)
            .OnDelete(DeleteBehavior.Cascade);

        var history = modelBuilder.Entity<ShipmentStatusHistory>();
        history.HasKey(x => x.Id);
        history.Property(x => x.OldStatus).HasConversion<string>().HasMaxLength(40);
        history.Property(x => x.NewStatus).HasConversion<string>().HasMaxLength(40).IsRequired();
        history.Property(x => x.ChangedBy).HasMaxLength(150).IsRequired();
    }
}
