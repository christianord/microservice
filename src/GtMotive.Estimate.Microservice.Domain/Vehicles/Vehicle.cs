using System;
using GtMotive.Estimate.Microservice.Domain.Fleets;

namespace GtMotive.Estimate.Microservice.Domain.Vehicles
{
    /// <summary>
    /// Vehicle aggregate root.
    /// </summary>
    public sealed class Vehicle : IVehicle
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Vehicle"/> class.
        /// </summary>
        /// <param name="fleetId">Fleet identifier.</param>
        /// <param name="licensePlate">License plate.</param>
        /// <param name="brand">Brand.</param>
        /// <param name="model">Model.</param>
        /// <param name="manufactureYear">Manufacture year.</param>
        public Vehicle(FleetId fleetId, LicensePlate licensePlate, string brand, string model, ManufactureYear manufactureYear)
            : this(VehicleId.New(), fleetId, licensePlate, brand, model, manufactureYear, VehicleStatus.Available)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Vehicle"/> class with persisted values.
        /// </summary>
        /// <param name="id">Vehicle identifier.</param>
        /// <param name="fleetId">Fleet identifier.</param>
        /// <param name="licensePlate">License plate.</param>
        /// <param name="brand">Brand.</param>
        /// <param name="model">Model.</param>
        /// <param name="manufactureYear">Manufacture year.</param>
        /// <param name="status">Vehicle status.</param>
        public Vehicle(
            VehicleId id,
            FleetId fleetId,
            LicensePlate licensePlate,
            string brand,
            string model,
            ManufactureYear manufactureYear,
            VehicleStatus status)
        {
            Id = id;
            FleetId = fleetId;
            LicensePlate = licensePlate;
            Brand = brand;
            Model = model;
            ManufactureYear = manufactureYear;
            Status = status;
        }

        /// <inheritdoc/>
        public VehicleId Id { get; }

        /// <inheritdoc/>
        public FleetId FleetId { get; }

        /// <inheritdoc/>
        public LicensePlate LicensePlate { get; }

        /// <inheritdoc/>
        public string Brand { get; }

        /// <inheritdoc/>
        public string Model { get; }

        /// <inheritdoc/>
        public ManufactureYear ManufactureYear { get; }

        /// <inheritdoc/>
        public VehicleStatus Status { get; private set; }

        /// <inheritdoc/>
        public void Rent(IFleet fleet, DateTime now)
        {
            ArgumentNullException.ThrowIfNull(fleet);

            fleet.EnsureFleetRequirements(this, now);
            Status = VehicleStateTransitions.Rent(Status, LicensePlate);
        }

        /// <summary>
        /// Marks the vehicle as available again.
        /// </summary>
        public void MarkAvailable()
        {
            Status = VehicleStateTransitions.MarkAvailable(Status, LicensePlate);
        }
    }
}
