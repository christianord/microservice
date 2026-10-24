using System;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Domain.Fleets;
using GtMotive.Estimate.Microservice.Domain.Interfaces;
using GtMotive.Estimate.Microservice.Domain.Vehicles;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.RegisterVehicle
{
    /// <summary>
    /// Registers a vehicle in a fleet, validated against the fleet admission policy.
    /// </summary>
    public sealed class RegisterVehicleUseCase(
        IFleetRepository fleetRepository,
        IVehicleRepository vehicleRepository,
        IVehicleFactory vehicleFactory,
        IUnitOfWork unitOfWork,
        TimeProvider timeProvider,
        IRegisterVehicleOutputPort outputPort) : IUseCase<RegisterVehicleInput>
    {
        /// <inheritdoc/>
        public async Task Execute(RegisterVehicleInput input)
        {
            ArgumentNullException.ThrowIfNull(input);

            try
            {
                var fleetId = new FleetId(input.FleetId);
                var licensePlate = new LicensePlate(input.LicensePlate);
                var manufactureYear = new ManufactureYear(input.ManufactureYear);

                if (string.IsNullOrWhiteSpace(input.Brand) || string.IsNullOrWhiteSpace(input.Model))
                {
                    throw new InvalidVehicleDataException("Brand and model are required.");
                }

                var fleet = await fleetRepository.GetBy(fleetId);

                var currentYear = timeProvider.GetUtcNow().Year;
                if (!fleet.AdmissionPolicy.IsAdmitted(manufactureYear.Value, currentYear))
                {
                    throw new VehicleTooOldException(
                        $"The manufacture year {manufactureYear} is not admitted by the fleet policy (maximum age {fleet.AdmissionPolicy.MaxVehicleAgeYears} years).");
                }

                if (await vehicleRepository.Exists(licensePlate))
                {
                    throw new VehicleAlreadyExistsException($"The vehicle with license plate {licensePlate} is already registered.");
                }

                var vehicle = vehicleFactory.NewVehicle(fleetId, licensePlate, input.Brand.Trim(), input.Model.Trim(), manufactureYear);

                await vehicleRepository.Add(vehicle);
                await unitOfWork.Save();

                outputPort.StandardHandle(new RegisterVehicleOutput(
                    vehicle.Id.Value,
                    vehicle.FleetId.Value,
                    vehicle.LicensePlate.Value,
                    vehicle.Brand,
                    vehicle.Model,
                    vehicle.ManufactureYear.Value,
                    vehicle.Status.ToString()));
            }
            catch (FleetNotFoundException ex)
            {
                outputPort.FleetNotFoundHandle(ex.Message);
            }
            catch (VehicleTooOldException ex)
            {
                outputPort.VehicleTooOldHandle(ex.Message);
            }
            catch (VehicleAlreadyExistsException ex)
            {
                outputPort.AlreadyExistsHandle(ex.Message);
            }
            catch (InvalidVehicleDataException ex)
            {
                outputPort.InvalidDataHandle(ex.Message);
            }
            catch (InvalidFleetDataException ex)
            {
                outputPort.InvalidDataHandle(ex.Message);
            }
        }
    }
}
