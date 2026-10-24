using System;
using MediatR;

namespace GtMotive.Estimate.Microservice.Api.UseCases.RegisterVehicle
{
    /// <summary>
    /// Register vehicle message.
    /// </summary>
    /// <param name="FleetId">Fleet id.</param>
    /// <param name="LicensePlate">License plate.</param>
    /// <param name="Brand">Brand.</param>
    /// <param name="Model">Model.</param>
    /// <param name="ManufactureYear">Manufacture year.</param>
    public sealed record RegisterVehicleRequest(Guid FleetId, string LicensePlate, string Brand, string Model, int? ManufactureYear) : IRequest<IWebApiPresenter>;
}
