using GtMotive.Estimate.Microservice.Domain.Vehicles;
using System;

namespace GtMotive.Estimate.Microservice.Domain.Fleets
{
    /// <summary>
    /// Fleet aggregate root.
    /// </summary>
    public sealed class Fleet : IFleet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Fleet"/> class.
        /// </summary>
        /// <param name="name">Fleet name.</param>
        /// <param name="admissionPolicy">Admission policy.</param>
        public Fleet(FleetName name, AdmissionPolicy admissionPolicy)
            : this(FleetId.New(), name, admissionPolicy)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Fleet"/> class with persisted values.
        /// </summary>
        /// <param name="id">Fleet identifier.</param>
        /// <param name="name">Fleet name.</param>
        /// <param name="admissionPolicy">Admission policy.</param>
        public Fleet(FleetId id, FleetName name, AdmissionPolicy admissionPolicy)
        {
            Id = id;
            Name = name;
            AdmissionPolicy = admissionPolicy;
        }

        /// <inheritdoc/>
        public FleetId Id { get; }

        /// <inheritdoc/>
        public FleetName Name { get; }

        /// <inheritdoc/>
        public AdmissionPolicy AdmissionPolicy { get; }

        /// <inheritdoc/>
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
