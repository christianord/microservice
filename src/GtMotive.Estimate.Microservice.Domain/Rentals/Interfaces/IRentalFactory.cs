using System;
using GtMotive.Estimate.Microservice.Domain.Vehicles;

namespace GtMotive.Estimate.Microservice.Domain.Rentals
{
    /// <summary>
    /// Rental factory.
    /// </summary>
    public interface IRentalFactory
    {
        /// <summary>
        /// Creates a new active rental.
        /// </summary>
        /// <param name="vehicleId">Vehicle id.</param>
        /// <param name="customerId">Customer id.</param>
        /// <param name="rentalDate">Rental date (UTC).</param>
        /// <returns>The rental.</returns>
        IRental NewRental(VehicleId vehicleId, Guid customerId, DateTime rentalDate);
    }
}
