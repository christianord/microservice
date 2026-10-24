using System;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Domain.Fleets;
using GtMotive.Estimate.Microservice.Domain.Interfaces;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.RegisterFleet
{
    /// <summary>
    /// Registers a fleet with its admission policy.
    /// </summary>
    public sealed class RegisterFleetUseCase(
        IFleetFactory fleetFactory,
        IFleetRepository fleetRepository,
        IUnitOfWork unitOfWork,
        IRegisterFleetOutputPort outputPort) : IUseCase<RegisterFleetInput>
    {
        /// <inheritdoc/>
        public async Task Execute(RegisterFleetInput input)
        {
            ArgumentNullException.ThrowIfNull(input);

            try
            {
                var fleet = fleetFactory.NewFleet(
                    new FleetName(input.Name),
                    new AdmissionPolicy(input.MaxVehicleAgeYears));

                await fleetRepository.Add(fleet);
                await unitOfWork.Save();

                outputPort.StandardHandle(new RegisterFleetOutput(
                    fleet.Id.Value,
                    fleet.Name.Value,
                    fleet.AdmissionPolicy.MaxVehicleAgeYears));
            }
            catch (InvalidFleetDataException ex)
            {
                outputPort.InvalidDataHandle(ex.Message);
            }
        }
    }
}
