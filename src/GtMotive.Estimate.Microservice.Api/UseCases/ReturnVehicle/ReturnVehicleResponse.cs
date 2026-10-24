using System;
using System.ComponentModel.DataAnnotations;

namespace GtMotive.Estimate.Microservice.Api.UseCases.ReturnVehicle
{
    /// <summary>
    /// Closed rental.
    /// </summary>
    public sealed class ReturnVehicleResponse(Guid rentalId, DateTime rentalDate, DateTime returnDate, int rentalDays)
    {
        /// <summary>
        /// Gets the rental id.
        /// </summary>
        [Required]
        public Guid RentalId { get; } = rentalId;

        /// <summary>
        /// Gets the rental date (UTC).
        /// </summary>
        [Required]
        public DateTime RentalDate { get; } = rentalDate;

        /// <summary>
        /// Gets the return date (UTC).
        /// </summary>
        [Required]
        public DateTime ReturnDate { get; } = returnDate;

        /// <summary>
        /// Gets the rental days.
        /// </summary>
        [Required]
        public int RentalDays { get; } = rentalDays;
    }
}
