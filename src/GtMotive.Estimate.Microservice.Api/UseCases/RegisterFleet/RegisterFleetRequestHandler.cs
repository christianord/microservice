using System;
using System.Threading;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.RegisterFleet;
using MediatR;

namespace GtMotive.Estimate.Microservice.Api.UseCases.RegisterFleet
{
    public sealed class RegisterFleetRequestHandler(
        IUseCase<RegisterFleetInput> useCase,
        RegisterFleetPresenter presenter) : IRequestHandler<RegisterFleetRequest, IWebApiPresenter>
    {
        public async Task<IWebApiPresenter> Handle(RegisterFleetRequest request, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(request);

            await useCase.Execute(new RegisterFleetInput(request.Name, request.MaxVehicleAgeYears ?? 0));
            return presenter;
        }
    }
}
