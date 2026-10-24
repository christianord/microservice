using System;
using GtMotive.Estimate.Microservice.Domain.Fleets;

namespace GtMotive.Estimate.Microservice.Domain.Vehicles
{
    /// <summary>
    /// Vehicle aggregate contract.
    /// </summary>
    public interface IVehicle
    {
        /// <summary>
        /// Gets the vehicle identifier.
        /// </summary>
        VehicleId Id { get; }

        /// <summary>
        /// Gets the identifier of the fleet this vehicle belongs to.
        /// </summary>
        FleetId FleetId { get; }

        /// <summary>
        /// Gets the license plate.
        /// </summary>
        LicensePlate LicensePlate { get; }

        /// <summary>
        /// Gets the vehicle brand.
        /// </summary>
        string Brand { get; }

        /// <summary>
        /// Gets the vehicle model.
        /// </summary>
        string Model { get; }

        /// <summary>
        /// Gets the manufacture year.
        /// </summary>
        ManufactureYear ManufactureYear { get; }

        /// <summary>
        /// Gets the vehicle status.
        /// </summary>
        VehicleStatus Status { get; }

        /// <summary>
        /// Marks the vehicle as rented, once the fleet confirms it admits the vehicle.
        /// </summary>
        /// <param name="fleet">Fleet the vehicle belongs to.</param>
        /// <param name="now">Current UTC date.</param>
        void Rent(IFleet fleet, DateTime now);

        /// <summary>
        /// Marks the vehicle as available.
        /// </summary>
        void MarkAvailable();
    }
}
