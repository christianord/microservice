using System;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Domain.Fleets;
using GtMotive.Estimate.Microservice.Domain.Vehicles;

namespace GtMotive.Estimate.Microservice.Domain.Rentals
{
    /// <summary>
    /// Domain service that enforces BR-1 (one active rental per customer). It does not persist.
    /// </summary>
    public sealed class RentalService(IRentalRepository rentalRepository, IRentalFactory rentalFactory)
    {
        /// <summary>
        /// Rents a vehicle to a customer.
        /// </summary>
        /// <param name="vehicle">Vehicle.</param>
        /// <param name="fleet">Fleet the vehicle belongs to.</param>
        /// <param name="customerId">Customer id.</param>
        /// <param name="now">Current UTC date.</param>
        /// <returns>The new rental.</returns>
        public async Task<IRental> Rent(IVehicle vehicle, IFleet fleet, Guid customerId, DateTime now)
        {
            ArgumentNullException.ThrowIfNull(vehicle);
            ArgumentNullException.ThrowIfNull(fleet);

            if (await rentalRepository.HasActiveByCustomer(customerId))
            {
                throw new CustomerAlreadyHasActiveRentalException($"The customer {customerId} already has an active rental.");
            }

            vehicle.Rent(fleet, now);

            var rental = rentalFactory.NewRental(vehicle.Id, customerId, now);
            await rentalRepository.Add(rental);

            return rental;
        }
    }
}
