using System;
using System.Diagnostics.CodeAnalysis;
using GtMotive.Estimate.Microservice.Domain.Fleets;
using GtMotive.Estimate.Microservice.Domain.Vehicles;

namespace GtMotive.Estimate.Microservice.Infrastructure.Persistence.Entities
{
    internal sealed class VehicleEntity : IVehicle
    {
        public VehicleEntity(FleetId fleetId, LicensePlate licensePlate, string brand, string model, ManufactureYear manufactureYear)
        {
            Id = VehicleId.New();
            FleetId = fleetId;
            LicensePlate = licensePlate;
            Brand = brand;
            Model = model;
            ManufactureYear = manufactureYear;
            Status = VehicleStatus.Available;
        }

        private VehicleEntity()
        {
        }

        public VehicleId Id { get; private set; }

        public FleetId FleetId { get; private set; }

        public LicensePlate LicensePlate { get; private set; }

        public string Brand { get; private set; }

        public string Model { get; private set; }

        public ManufactureYear ManufactureYear { get; private set; }

        public VehicleStatus Status { get; private set; }

        [SuppressMessage("Performance", "CA1819:Properties should not return arrays", Justification = "Concurrency token mapped to a rowversion column.")]
        public byte[] RowVersion { get; private set; }

        public void Rent(IFleet fleet, DateTime now)
        {
            ArgumentNullException.ThrowIfNull(fleet);

            fleet.EnsureFleetRequirements(this, now);
            Status = VehicleStateTransitions.Rent(Status, LicensePlate);
        }

        public void MarkAvailable()
        {
            Status = VehicleStateTransitions.MarkAvailable(Status, LicensePlate);
        }
    }
}
