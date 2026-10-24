using System;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Domain.Vehicles;

namespace GtMotive.Estimate.Microservice.Domain.Rentals
{
    /// <summary>
    /// Rental repository. It only tracks changes; persisting is done by the unit of work.
    /// </summary>
    public interface IRentalRepository
    {
        /// <summary>
        /// Tracks a new rental.
        /// </summary>
        /// <param name="rental">Rental.</param>
        /// <returns>Task.</returns>
        Task Add(IRental rental);

        /// <summary>
        /// Checks whether the customer has an active rental.
        /// </summary>
        /// <param name="customerId">Customer id.</param>
        /// <returns>True if there is an active rental.</returns>
        Task<bool> HasActiveByCustomer(Guid customerId);

        /// <summary>
        /// Gets the tracked active rental of a vehicle.
        /// </summary>
        /// <param name="vehicleId">Vehicle id.</param>
        /// <returns>The active rental.</returns>
        /// <exception cref="VehicleNotRentedException">When the vehicle has no active rental.</exception>
        Task<IRental> GetActiveByVehicle(VehicleId vehicleId);
    }
}
