using System;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Domain.Fleets;
using GtMotive.Estimate.Microservice.Domain.Interfaces;
using GtMotive.Estimate.Microservice.Domain.Rentals;
using GtMotive.Estimate.Microservice.Domain.Vehicles;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.RentVehicle
{
    /// <summary>
    /// Rents a vehicle to a customer in a single transaction.
    /// </summary>
    public sealed class RentVehicleUseCase(
        IVehicleRepository vehicleRepository,
        IFleetRepository fleetRepository,
        RentalService rentalService,
        IUnitOfWork unitOfWork,
        TimeProvider timeProvider,
        IRentVehicleOutputPort outputPort) : IUseCase<RentVehicleInput>
    {
        /// <inheritdoc/>
        public async Task Execute(RentVehicleInput input)
        {
            ArgumentNullException.ThrowIfNull(input);

            try
            {
                if (input.CustomerId == Guid.Empty)
                {
                    throw new InvalidVehicleDataException("The customer id must not be empty.");
                }

                var vehicle = await vehicleRepository.GetBy(new VehicleId(input.VehicleId));
                var fleet = await fleetRepository.GetBy(vehicle.FleetId);

                var now = timeProvider.GetUtcNow();
                var rental = await rentalService.Rent(vehicle, fleet, input.CustomerId, now.UtcDateTime);

                await unitOfWork.Save();

                outputPort.StandardHandle(new RentVehicleOutput(rental.Id.Value, vehicle.Id.Value, rental.CustomerId, rental.RentalDate));
            }
            catch (VehicleNotFoundException ex)
            {
                outputPort.VehicleNotFoundHandle(ex.Message);
            }
            catch (FleetNotFoundException ex)
            {
                outputPort.VehicleNotFoundHandle(ex.Message);
            }
            catch (VehicleTooOldException ex)
            {
                outputPort.VehicleTooOldHandle(ex.Message);
            }
            catch (VehicleNotAvailableException ex)
            {
                outputPort.VehicleNotAvailableHandle(ex.Message);
            }
            catch (CustomerAlreadyHasActiveRentalException ex)
            {
                outputPort.CustomerAlreadyHasRentalHandle(ex.Message);
            }
            catch (InvalidVehicleDataException ex)
            {
                outputPort.InvalidDataHandle(ex.Message);
            }
        }
    }
}
