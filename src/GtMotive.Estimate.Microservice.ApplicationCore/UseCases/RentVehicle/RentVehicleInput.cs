using System;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.RentVehicle
{
    /// <summary>
    /// Input of the rent vehicle use case.
    /// </summary>
    /// <param name="VehicleId">Vehicle id.</param>
    /// <param name="CustomerId">Customer id.</param>
    public sealed record RentVehicleInput(Guid VehicleId, Guid CustomerId) : IUseCaseInput;
}
