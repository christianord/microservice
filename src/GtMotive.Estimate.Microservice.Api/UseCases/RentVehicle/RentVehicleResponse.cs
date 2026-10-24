using System;
using System.ComponentModel.DataAnnotations;

namespace GtMotive.Estimate.Microservice.Api.UseCases.RentVehicle
{
    /// <summary>
    /// Created rental.
    /// </summary>
    public sealed class RentVehicleResponse(Guid rentalId, Guid vehicleId, Guid customerId, DateTime rentalDate)
    {
        /// <summary>
        /// Gets the rental id.
        /// </summary>
        [Required]
        public Guid RentalId { get; } = rentalId;

        /// <summary>
        /// Gets the vehicle id.
        /// </summary>
        [Required]
        public Guid VehicleId { get; } = vehicleId;

        /// <summary>
        /// Gets the customer id.
        /// </summary>
        [Required]
        public Guid CustomerId { get; } = customerId;

        /// <summary>
        /// Gets the rental date (UTC).
        /// </summary>
        [Required]
        public DateTime RentalDate { get; } = rentalDate;
    }
}
