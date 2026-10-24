using System;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.RegisterVehicle
{
    /// <summary>
    /// Output of the register vehicle use case.
    /// </summary>
    /// <param name="VehicleId">Vehicle id.</param>
    /// <param name="FleetId">Fleet id.</param>
    /// <param name="LicensePlate">License plate.</param>
    /// <param name="Brand">Brand.</param>
    /// <param name="Model">Model.</param>
    /// <param name="ManufactureYear">Manufacture year.</param>
    /// <param name="Status">Rental status.</param>
    public sealed record RegisterVehicleOutput(Guid VehicleId, Guid FleetId, string LicensePlate, string Brand, string Model, int ManufactureYear, string Status) : IUseCaseOutput;
}
