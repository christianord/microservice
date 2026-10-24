using System;
using System.Threading;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.RentVehicle;
using MediatR;

namespace GtMotive.Estimate.Microservice.Api.UseCases.RentVehicle
{
    public sealed class RentVehicleRequestHandler(
        IUseCase<RentVehicleInput> useCase,
        RentVehiclePresenter presenter) : IRequestHandler<RentVehicleRequest, IWebApiPresenter>
    {
        public async Task<IWebApiPresenter> Handle(RentVehicleRequest request, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(request);

            await useCase.Execute(new RentVehicleInput(request.VehicleId, request.CustomerId));
            return presenter;
        }
    }
}
