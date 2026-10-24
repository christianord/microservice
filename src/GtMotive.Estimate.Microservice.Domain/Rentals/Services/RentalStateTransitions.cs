using System;
using GtMotive.Estimate.Microservice.Domain.Vehicles;

namespace GtMotive.Estimate.Microservice.Domain.Rentals
{
    /// <summary>
    /// Applies rental closing rules.
    /// </summary>
    public static class RentalStateTransitions
    {
        /// <summary>
        /// Calculates the closing state for a rental.
        /// </summary>
        /// <param name="rentalDate">Rental start date.</param>
        /// <param name="currentReturnDate">Current return date, if already closed.</param>
        /// <param name="returnDate">Requested return date.</param>
        /// <returns>Return date and rounded-up rental days.</returns>
        public static (DateTime ReturnDate, int RentalDays) Close(DateTime rentalDate, DateTime? currentReturnDate, DateTime returnDate)
        {
            if (currentReturnDate.HasValue)
            {
                throw new VehicleNotRentedException("The rental is already closed.");
            }

            if (returnDate < rentalDate)
            {
                throw new ArgumentOutOfRangeException(nameof(returnDate), "The return date cannot be before the rental date.");
            }

            return (returnDate, Math.Max(1, (int)Math.Ceiling((returnDate - rentalDate).TotalDays)));
        }
    }
}
