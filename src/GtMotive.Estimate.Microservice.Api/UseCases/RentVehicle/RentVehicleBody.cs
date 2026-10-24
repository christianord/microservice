using System;
using System.ComponentModel.DataAnnotations;

namespace GtMotive.Estimate.Microservice.Api.UseCases.RentVehicle
{
    /// <summary>
    /// Body of the rent vehicle endpoint.
    /// </summary>
    /// <param name="CustomerId">Customer id.</param>
    public sealed record RentVehicleBody([Required] Guid? CustomerId);
}
