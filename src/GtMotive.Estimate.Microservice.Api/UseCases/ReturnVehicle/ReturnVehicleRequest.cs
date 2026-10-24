using System;
using MediatR;

namespace GtMotive.Estimate.Microservice.Api.UseCases.ReturnVehicle
{
    /// <summary>
    /// Return vehicle message.
    /// </summary>
    /// <param name="VehicleId">Vehicle id.</param>
    /// <param name="CustomerId">Customer id.</param>
    public sealed record ReturnVehicleRequest(Guid VehicleId, Guid CustomerId) : IRequest<IWebApiPresenter>;
}
