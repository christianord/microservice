using System;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.RentVehicle
{
    /// <summary>
    /// Output of the rent vehicle use case.
    /// </summary>
    /// <param name="RentalId">Rental id.</param>
    /// <param name="VehicleId">Vehicle id.</param>
    /// <param name="CustomerId">Customer id.</param>
    /// <param name="RentalDate">Rental date (UTC).</param>
    public sealed record RentVehicleOutput(Guid RentalId, Guid VehicleId, Guid CustomerId, DateTime RentalDate) : IUseCaseOutput;
}
