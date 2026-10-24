using System;
using GtMotive.Estimate.Microservice.Domain.Fleets;
using GtMotive.Estimate.Microservice.Domain.Vehicles;
using GtMotive.Estimate.Microservice.Infrastructure.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GtMotive.Estimate.Microservice.Infrastructure.Persistence.Configurations
{
    internal sealed class VehicleConfiguration : IEntityTypeConfiguration<VehicleEntity>
    {
        public void Configure(EntityTypeBuilder<VehicleEntity> builder)
        {
            ArgumentNullException.ThrowIfNull(builder);

            builder.ToTable("Vehicle");
            builder.HasKey(v => v.Id);

            builder.Property(v => v.Id)
                .HasConversion(id => id.Value, value => new VehicleId(value))
                .ValueGeneratedNever();

            builder.Property(v => v.FleetId)
                .HasConversion(id => id.Value, value => new FleetId(value))
                .IsRequired();

            builder.Property(v => v.LicensePlate)
                .HasConversion(plate => plate.Value, value => new LicensePlate(value))
                .HasMaxLength(20)
                .IsRequired();

            builder.Property(v => v.Brand).HasMaxLength(100).IsRequired();
            builder.Property(v => v.Model).HasMaxLength(100).IsRequired();

            builder.Property(v => v.ManufactureYear)
                .HasConversion(year => year.Value, value => new ManufactureYear(value))
                .IsRequired();

            builder.Property(v => v.Status)
                .HasConversion<int>()
                .IsRequired();

            builder.Property(v => v.RowVersion).IsRowVersion();

            builder.HasIndex(v => v.LicensePlate).IsUnique();
            builder.HasIndex(v => new { v.FleetId, v.Status, v.ManufactureYear });
        }
    }
}
