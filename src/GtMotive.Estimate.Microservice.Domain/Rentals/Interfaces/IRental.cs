using System;
using GtMotive.Estimate.Microservice.Domain.Vehicles;

namespace GtMotive.Estimate.Microservice.Domain.Rentals
{
    /// <summary>
    /// Rental aggregate contract.
    /// </summary>
    public interface IRental
    {
        /// <summary>
        /// Gets the rental identifier.
        /// </summary>
        RentalId Id { get; }

        /// <summary>
        /// Gets the vehicle identifier.
        /// </summary>
        VehicleId VehicleId { get; }

        /// <summary>
        /// Gets the customer identifier.
        /// </summary>
        Guid CustomerId { get; }

        /// <summary>
        /// Gets the rental date (UTC).
        /// </summary>
        DateTime RentalDate { get; }

        /// <summary>
        /// Gets the return date (UTC), null while the rental is active.
        /// </summary>
        DateTime? ReturnDate { get; }

        /// <summary>
        /// Gets the rental days, null while the rental is active.
        /// </summary>
        int? RentalDays { get; }

        /// <summary>
        /// Closes the rental.
        /// </summary>
        /// <param name="returnDate">Return date (UTC).</param>
        void Close(DateTime returnDate);
    }
}
