using MediatR;

namespace GtMotive.Estimate.Microservice.Api.UseCases.RegisterFleet
{
    /// <summary>
    /// Register fleet message.
    /// </summary>
    /// <param name="Name">Fleet name.</param>
    /// <param name="MaxVehicleAgeYears">Maximum vehicle age in years.</param>
    public sealed record RegisterFleetRequest(string Name, int? MaxVehicleAgeYears) : IRequest<IWebApiPresenter>;
}
