using System;
using System.ComponentModel.DataAnnotations;

namespace GtMotive.Estimate.Microservice.Api.UseCases.RegisterVehicle
{
    /// <summary>
    /// Registered vehicle.
    /// </summary>
    public sealed class RegisterVehicleResponse(
        Guid vehicleId,
        Guid fleetId,
        string licensePlate,
        string brand,
        string model,
        int manufactureYear,
        string status)
    {
        /// <summary>
        /// Gets the vehicle id.
        /// </summary>
        [Required]
        public Guid VehicleId { get; } = vehicleId;

        /// <summary>
        /// Gets the fleet id.
        /// </summary>
        [Required]
        public Guid FleetId { get; } = fleetId;

        /// <summary>
        /// Gets the license plate.
        /// </summary>
        [Required]
        public string LicensePlate { get; } = licensePlate;

        /// <summary>
        /// Gets the brand.
        /// </summary>
        [Required]
        public string Brand { get; } = brand;

        /// <summary>
        /// Gets the model.
        /// </summary>
        [Required]
        public string Model { get; } = model;

        /// <summary>
        /// Gets the manufacture year.
        /// </summary>
        [Required]
        public int ManufactureYear { get; } = manufactureYear;

        /// <summary>
        /// Gets the rental status (Available or Rented).
        /// </summary>
        [Required]
        public string Status { get; } = status;
    }
}
