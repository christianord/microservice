namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.RegisterFleet
{
    /// <summary>
    /// Input of the register fleet use case.
    /// </summary>
    /// <param name="Name">Fleet name.</param>
    /// <param name="MaxVehicleAgeYears">Maximum vehicle age in years.</param>
    public sealed record RegisterFleetInput(string Name, int MaxVehicleAgeYears) : IUseCaseInput;
}
