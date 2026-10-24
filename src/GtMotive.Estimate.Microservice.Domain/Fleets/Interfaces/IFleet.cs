using System;
using GtMotive.Estimate.Microservice.Domain.Vehicles;

namespace GtMotive.Estimate.Microservice.Domain.Fleets
{
    /// <summary>
    /// Fleet aggregate contract.
    /// </summary>
    public interface IFleet
    {
        /// <summary>
        /// Gets the fleet identifier.
        /// </summary>
        FleetId Id { get; }

        /// <summary>
        /// Gets the fleet name.
        /// </summary>
        FleetName Name { get; }

        /// <summary>
        /// Gets the fleet admission policy.
        /// </summary>
        AdmissionPolicy AdmissionPolicy { get; }

        /// <summary>
        /// Ensures the vehicle belongs to this fleet and satisfies its admission policy.
        /// </summary>
        /// <param name="vehicle">Vehicle to check.</param>
        /// <param name="now">Current UTC date.</param>
        void EnsureFleetRequirements(IVehicle vehicle, DateTime now);
    }
}
