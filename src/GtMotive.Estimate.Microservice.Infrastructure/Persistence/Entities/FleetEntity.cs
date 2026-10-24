using System;
using GtMotive.Estimate.Microservice.Domain.Fleets;
using GtMotive.Estimate.Microservice.Domain.Vehicles;

namespace GtMotive.Estimate.Microservice.Infrastructure.Persistence.Entities
{
    internal sealed class FleetEntity : IFleet
    {
        public FleetEntity(FleetName name, AdmissionPolicy admissionPolicy)
        {
            Id = FleetId.New();
            Name = name;
            AdmissionPolicy = admissionPolicy;
        }

        private FleetEntity()
        {
        }

        public FleetId Id { get; private set; }

        public FleetName Name { get; private set; }

        public AdmissionPolicy AdmissionPolicy { get; private set; }

        public void EnsureFleetRequirements(IVehicle vehicle, DateTime now)
        {
            ArgumentNullException.ThrowIfNull(vehicle);

            if (vehicle.FleetId != Id)
            {
                throw new InvalidVehicleDataException($"The vehicle {vehicle.LicensePlate} does not belong to the fleet {Id}.");
            }

            if (!AdmissionPolicy.IsAdmitted(vehicle.ManufactureYear.Value, now.Year))
            {
                throw new VehicleTooOldException($"The vehicle {vehicle.LicensePlate} does not satisfy the fleet admission policy.");
            }
        }
    }
}
