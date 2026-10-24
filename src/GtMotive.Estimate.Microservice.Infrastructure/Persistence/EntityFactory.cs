using GtMotive.Estimate.Microservice.Domain.Fleets;
using GtMotive.Estimate.Microservice.Domain.Rentals;
using GtMotive.Estimate.Microservice.Domain.Vehicles;
using GtMotive.Estimate.Microservice.Infrastructure.Persistence.Entities;

namespace GtMotive.Estimate.Microservice.Infrastructure.Persistence
{
    internal sealed class EntityFactory : IFleetFactory, IVehicleFactory, IRentalFactory
    {
        public IRental NewRental(VehicleId vehicleId, System.Guid customerId, System.DateTime rentalDate) =>
            new RentalEntity(vehicleId, customerId, rentalDate);

        public IFleet NewFleet(FleetName name, AdmissionPolicy admissionPolicy) =>
            new FleetEntity(name, admissionPolicy);

        public IVehicle NewVehicle(FleetId fleetId, LicensePlate licensePlate, string brand, string model, ManufactureYear manufactureYear) =>
            new VehicleEntity(fleetId, licensePlate, brand, model, manufactureYear);
    }
}
