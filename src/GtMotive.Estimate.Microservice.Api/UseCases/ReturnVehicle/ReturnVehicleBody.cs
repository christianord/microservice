using System;
using System.ComponentModel.DataAnnotations;

namespace GtMotive.Estimate.Microservice.Api.UseCases.ReturnVehicle
{
    /// <summary>
    /// Body of the return vehicle endpoint.
    /// </summary>
    /// <param name="CustomerId">Customer id.</param>
    public sealed record ReturnVehicleBody([Required] Guid? CustomerId);
}
