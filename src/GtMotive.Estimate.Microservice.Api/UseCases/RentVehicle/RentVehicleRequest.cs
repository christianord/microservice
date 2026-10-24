using System;
using MediatR;

namespace GtMotive.Estimate.Microservice.Api.UseCases.RentVehicle
{
    /// <summary>
    /// Rent vehicle message.
    /// </summary>
    /// <param name="VehicleId">Vehicle id.</param>
    /// <param name="CustomerId">Customer id.</param>
    public sealed record RentVehicleRequest(Guid VehicleId, Guid CustomerId) : IRequest<IWebApiPresenter>;
}
