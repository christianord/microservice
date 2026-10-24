using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Api.UseCases.RentVehicle;
using GtMotive.Estimate.Microservice.Api.UseCases.ReturnVehicle;
using GtMotive.Estimate.Microservice.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GtMotive.Estimate.Microservice.Api.Controllers
{
    /// <summary>
    /// Rental endpoints.
    /// </summary>
    [ApiController]
    [Authorize]
    [Route("api/vehicles/{vehicleId:guid}")]
    public sealed class RentalsController(IMediator mediator, IAppLogger<RentalsController> appLogger) : ControllerBase
    {
        /// <summary>
        /// Rents a vehicle to a customer.
        /// </summary>
        /// <param name="vehicleId">Vehicle id.</param>
        /// <param name="body">Customer data.</param>
        /// <returns>The created rental.</returns>
        [HttpPost("rentals")]
        [ProducesResponseType(typeof(RentVehicleResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Rent([FromRoute] Guid vehicleId, [FromBody][Required] RentVehicleBody body)
        {
            ArgumentNullException.ThrowIfNull(body);

            appLogger.LogDebug("Entering Rent. Vehicle: {vehicleId}", vehicleId);
            var presenter = await mediator.Send(new RentVehicleRequest(vehicleId, body.CustomerId ?? Guid.Empty));
            return presenter.ActionResult;
        }

        /// <summary>
        /// Returns a rented vehicle.
        /// </summary>
        /// <param name="vehicleId">Vehicle id.</param>
        /// <param name="body">Customer data.</param>
        /// <returns>The closed rental.</returns>
        [HttpPost("returns")]
        [ProducesResponseType(typeof(ReturnVehicleResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Return([FromRoute] Guid vehicleId, [FromBody][Required] ReturnVehicleBody body)
        {
            ArgumentNullException.ThrowIfNull(body);

            appLogger.LogDebug("Entering Return. Vehicle: {vehicleId}", vehicleId);
            var presenter = await mediator.Send(new ReturnVehicleRequest(vehicleId, body.CustomerId ?? Guid.Empty));
            return presenter.ActionResult;
        }
    }
}
