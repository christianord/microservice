using System;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.ReturnVehicle
{
    /// <summary>
    /// Output of the return vehicle use case.
    /// </summary>
    /// <param name="RentalId">Rental id.</param>
    /// <param name="RentalDate">Rental date (UTC).</param>
    /// <param name="ReturnDate">Return date (UTC).</param>
    /// <param name="RentalDays">Rental days.</param>
    public sealed record ReturnVehicleOutput(Guid RentalId, DateTime RentalDate, DateTime ReturnDate, int RentalDays) : IUseCaseOutput;
}
