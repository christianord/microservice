using System;
using GtMotive.Estimate.Microservice.Domain.Rentals;
using GtMotive.Estimate.Microservice.Domain.Vehicles;
using GtMotive.Estimate.Microservice.Infrastructure.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GtMotive.Estimate.Microservice.Infrastructure.Persistence.Configurations
{
    internal sealed class RentalConfiguration : IEntityTypeConfiguration<RentalEntity>
    {
        public void Configure(EntityTypeBuilder<RentalEntity> builder)
        {
            ArgumentNullException.ThrowIfNull(builder);

            builder.ToTable("Rental");
            builder.HasKey(r => r.Id);

            builder.Property(r => r.Id)
                .HasConversion(id => id.Value, value => new RentalId(value))
                .ValueGeneratedNever();

            builder.Property(r => r.VehicleId)
                .HasConversion(id => id.Value, value => new VehicleId(value))
                .IsRequired();

            builder.Property(r => r.CustomerId).IsRequired();
            builder.Property(r => r.RentalDate).IsRequired();
            builder.Property(r => r.ReturnDate);
            builder.Property(r => r.RentalDays);

            builder.Property(r => r.RowVersion).IsRowVersion();

            builder.HasIndex(r => r.CustomerId)
                .IsUnique()
                .HasFilter("[ReturnDate] IS NULL")
                .HasDatabaseName("UX_Rental_Customer_Active");

            builder.HasIndex(r => r.VehicleId)
                .IsUnique()
                .HasFilter("[ReturnDate] IS NULL")
                .HasDatabaseName("UX_Rental_Vehicle_Active");
        }
    }
}
