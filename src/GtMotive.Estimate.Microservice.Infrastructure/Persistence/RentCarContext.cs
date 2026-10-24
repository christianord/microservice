using System;
using GtMotive.Estimate.Microservice.Infrastructure.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace GtMotive.Estimate.Microservice.Infrastructure.Persistence
{
    internal sealed class RentCarContext(DbContextOptions<RentCarContext> options) : DbContext(options)
    {
        public DbSet<FleetEntity> Fleets => Set<FleetEntity>();

        public DbSet<VehicleEntity> Vehicles => Set<VehicleEntity>();

        public DbSet<RentalEntity> Rentals => Set<RentalEntity>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ArgumentNullException.ThrowIfNull(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(RentCarContext).Assembly);
        }
    }
}
