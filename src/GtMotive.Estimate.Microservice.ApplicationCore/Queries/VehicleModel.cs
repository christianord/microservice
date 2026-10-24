using System;
using GtMotive.Estimate.Microservice.Domain.Vehicles;

namespace GtMotive.Estimate.Microservice.ApplicationCore.Queries
{
    /// <summary>
    /// Read model of a vehicle.
    /// </summary>
    public sealed class VehicleModel
    {
        /// <summary>
        /// Gets or sets the vehicle id.
        /// </summary>
        public Guid VehicleId { get; set; }

        /// <summary>
        /// Gets or sets the fleet id.
        /// </summary>
        public Guid FleetId { get; set; }

        /// <summary>
        /// Gets or sets the license plate.
        /// </summary>
        public string LicensePlate { get; set; }

        /// <summary>
        /// Gets or sets the brand.
        /// </summary>
        public string Brand { get; set; }

        /// <summary>
        /// Gets or sets the model.
        /// </summary>
        public string Model { get; set; }

        /// <summary>
        /// Gets or sets the manufacture year.
        /// </summary>
        public int ManufactureYear { get; set; }

        /// <summary>
        /// Gets or sets the rental status.
        /// </summary>
        public VehicleStatus Status { get; set; }
    }
}
