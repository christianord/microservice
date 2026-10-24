using System;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.RegisterFleet
{
    /// <summary>
    /// Output of the register fleet use case.
    /// </summary>
    /// <param name="FleetId">Fleet id.</param>
    /// <param name="Name">Fleet name.</param>
    /// <param name="MaxVehicleAgeYears">Maximum vehicle age in years.</param>
    public sealed record RegisterFleetOutput(Guid FleetId, string Name, int MaxVehicleAgeYears) : IUseCaseOutput;
}
