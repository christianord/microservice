using System;
using System.Threading;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.RegisterVehicle;
using MediatR;

namespace GtMotive.Estimate.Microservice.Api.UseCases.RegisterVehicle
{
    public sealed class RegisterVehicleRequestHandler(
        IUseCase<RegisterVehicleInput> useCase,
        RegisterVehiclePresenter presenter) : IRequestHandler<RegisterVehicleRequest, IWebApiPresenter>
    {
        public async Task<IWebApiPresenter> Handle(RegisterVehicleRequest request, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(request);

            await useCase.Execute(new RegisterVehicleInput(
                request.FleetId,
                request.LicensePlate,
                request.Brand,
                request.Model,
                request.ManufactureYear ?? 0));
            return presenter;
        }
    }
}
