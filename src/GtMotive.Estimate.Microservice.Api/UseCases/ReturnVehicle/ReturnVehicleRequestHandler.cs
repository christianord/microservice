using System;
using System.Threading;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.ReturnVehicle;
using MediatR;

namespace GtMotive.Estimate.Microservice.Api.UseCases.ReturnVehicle
{
    public sealed class ReturnVehicleRequestHandler(
        IUseCase<ReturnVehicleInput> useCase,
        ReturnVehiclePresenter presenter) : IRequestHandler<ReturnVehicleRequest, IWebApiPresenter>
    {
        public async Task<IWebApiPresenter> Handle(ReturnVehicleRequest request, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(request);

            await useCase.Execute(new ReturnVehicleInput(request.VehicleId, request.CustomerId));
            return presenter;
        }
    }
}
