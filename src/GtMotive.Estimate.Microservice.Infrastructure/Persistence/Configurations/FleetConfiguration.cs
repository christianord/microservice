using System;
using GtMotive.Estimate.Microservice.Domain.Fleets;
using GtMotive.Estimate.Microservice.Infrastructure.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GtMotive.Estimate.Microservice.Infrastructure.Persistence.Configurations
{
    internal sealed class FleetConfiguration : IEntityTypeConfiguration<FleetEntity>
    {
        public void Configure(EntityTypeBuilder<FleetEntity> builder)
        {
            ArgumentNullException.ThrowIfNull(builder);

            builder.ToTable("Fleet");
            builder.HasKey(f => f.Id);

            builder.Property(f => f.Id)
                .HasConversion(id => id.Value, value => new FleetId(value))
                .ValueGeneratedNever();

            builder.Property(f => f.Name)
                .HasConversion(name => name.Value, value => new FleetName(value))
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(f => f.AdmissionPolicy)
                .HasColumnName("MaxVehicleAgeYears")
                .HasConversion(policy => policy.MaxVehicleAgeYears, value => new AdmissionPolicy(value))
                .IsRequired();
        }
    }
}
