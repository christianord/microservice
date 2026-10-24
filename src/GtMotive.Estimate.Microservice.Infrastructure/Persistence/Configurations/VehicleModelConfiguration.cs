using System;
using GtMotive.Estimate.Microservice.ApplicationCore.Queries;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GtMotive.Estimate.Microservice.Infrastructure.Persistence.Configurations
{
    internal sealed class VehicleModelConfiguration : IEntityTypeConfiguration<VehicleModel>
    {
        public void Configure(EntityTypeBuilder<VehicleModel> builder)
        {
            ArgumentNullException.ThrowIfNull(builder);

            builder.HasNoKey();
            builder.ToView("Vehicle");

            builder.Property(v => v.VehicleId).HasColumnName("Id");
            builder.Property(v => v.FleetId).HasColumnName("FleetId");
            builder.Property(v => v.LicensePlate).HasColumnName("LicensePlate");
            builder.Property(v => v.Brand).HasColumnName("Brand");
            builder.Property(v => v.Model).HasColumnName("Model");
            builder.Property(v => v.ManufactureYear).HasColumnName("ManufactureYear");
            builder.Property(v => v.Status).HasColumnName("Status").HasConversion<int>();
        }
    }
}
