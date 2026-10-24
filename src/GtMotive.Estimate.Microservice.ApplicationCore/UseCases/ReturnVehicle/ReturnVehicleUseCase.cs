using System;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Domain.Interfaces;
using GtMotive.Estimate.Microservice.Domain.Rentals;
using GtMotive.Estimate.Microservice.Domain.Vehicles;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.ReturnVehicle
{
    /// <summary>
    /// Returns a rented vehicle in a single transaction.
    /// </summary>
    public sealed class ReturnVehicleUseCase(
        IVehicleRepository vehicleRepository,
        IRentalRepository rentalRepository,
        IUnitOfWork unitOfWork,
        TimeProvider timeProvider,
        IReturnVehicleOutputPort outputPort) : IUseCase<ReturnVehicleInput>
    {
        /// <inheritdoc/>
        public async Task Execute(ReturnVehicleInput input)
        {
            ArgumentNullException.ThrowIfNull(input);

            try
            {
                if (input.CustomerId == Guid.Empty)
                {
                    throw new InvalidVehicleDataException("The customer id must not be empty.");
                }

                var vehicle = await vehicleRepository.GetBy(new VehicleId(input.VehicleId));
                var rental = await rentalRepository.GetActiveByVehicle(vehicle.Id);

                if (rental.CustomerId != input.CustomerId)
                {
                    throw new NotRenterException("The customer is not the renter of the vehicle.");
                }

                vehicle.MarkAvailable();
                rental.Close(timeProvider.GetUtcNow().UtcDateTime);

                await unitOfWork.Save();

                outputPort.StandardHandle(new ReturnVehicleOutput(rental.Id.Value, rental.RentalDate, rental.ReturnDate.Value, rental.RentalDays.Value));
            }
            catch (VehicleNotFoundException ex)
            {
                outputPort.VehicleNotFoundHandle(ex.Message);
            }
            catch (VehicleNotRentedException ex)
            {
                outputPort.NotRentedHandle(ex.Message);
            }
            catch (NotRenterException ex)
            {
                outputPort.NotRenterHandle(ex.Message);
            }
            catch (InvalidVehicleDataException ex)
            {
                outputPort.InvalidDataHandle(ex.Message);
            }
        }
    }
}
