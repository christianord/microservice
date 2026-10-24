using System;
using System.ComponentModel.DataAnnotations;

namespace GtMotive.Estimate.Microservice.Api.UseCases.RegisterFleet
{
    /// <summary>
    /// Registered fleet.
    /// </summary>
    public sealed class RegisterFleetResponse(Guid fleetId, string name, int maxVehicleAgeYears)
    {
        /// <summary>
        /// Gets the fleet id.
        /// </summary>
        [Required]
        public Guid FleetId { get; } = fleetId;

        /// <summary>
        /// Gets the fleet name.
        /// </summary>
        [Required]
        public string Name { get; } = name;

        /// <summary>
        /// Gets the maximum vehicle age in years allowed by the admission policy.
        /// </summary>
        [Required]
        public int MaxVehicleAgeYears { get; } = maxVehicleAgeYears;
    }
}
