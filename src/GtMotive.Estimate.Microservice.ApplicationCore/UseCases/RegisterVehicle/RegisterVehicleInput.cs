using System;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.RegisterVehicle
{
    /// <summary>
    /// Input of the register vehicle use case.
    /// </summary>
    /// <param name="FleetId">Fleet id.</param>
    /// <param name="LicensePlate">License plate.</param>
    /// <param name="Brand">Brand.</param>
    /// <param name="Model">Model.</param>
    /// <param name="ManufactureYear">Manufacture year.</param>
    public sealed record RegisterVehicleInput(Guid FleetId, string LicensePlate, string Brand, string Model, int ManufactureYear) : IUseCaseInput;
}
