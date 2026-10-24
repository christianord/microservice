using System;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.ReturnVehicle
{
    /// <summary>
    /// Input of the return vehicle use case.
    /// </summary>
    /// <param name="VehicleId">Vehicle id.</param>
    /// <param name="CustomerId">Customer id.</param>
    public sealed record ReturnVehicleInput(Guid VehicleId, Guid CustomerId) : IUseCaseInput;
}
