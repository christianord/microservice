using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Api.Queries.GetVehicles;
using GtMotive.Estimate.Microservice.Api.UseCases.RegisterVehicle;
using GtMotive.Estimate.Microservice.ApplicationCore.Queries;
using GtMotive.Estimate.Microservice.Domain.Fleets;
using GtMotive.Estimate.Microservice.Domain.Interfaces;
using GtMotive.Estimate.Microservice.Domain.Vehicles;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GtMotive.Estimate.Microservice.Api.Controllers
{
    /// <summary>
    /// Vehicles endpoints.
    /// </summary>
    [ApiController]
    [Authorize]
    [Route("api/fleets/{fleetId:guid}/vehicles")]
    public sealed class VehiclesController(IMediator mediator, IAppLogger<VehiclesController> appLogger) : ControllerBase
    {
        private const int MaxPageSize = 100;

        /// <summary>
        /// Registers a vehicle in a fleet.
        /// </summary>
        /// <param name="fleetId">Fleet id.</param>
        /// <param name="body">Vehicle data.</param>
        /// <returns>The registered vehicle.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(RegisterVehicleResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Post([FromRoute] Guid fleetId, [FromBody][Required] RegisterVehicleBody body)
        {
            ArgumentNullException.ThrowIfNull(body);

            var request = new RegisterVehicleRequest(fleetId, body.LicensePlate, body.Brand, body.Model, body.ManufactureYear);
            appLogger.LogDebug("Entering Post. Request: {@request}", request);
            var presenter = await mediator.Send(request);
            return presenter.ActionResult;
        }

        /// <summary>
        /// Lists the vehicles of a fleet filtered by status, paginated.
        /// </summary>
        /// <param name="fleetId">Fleet id.</param>
        /// <param name="status">Status filter (Available or Rented).</param>
        /// <param name="page">Page number, starting at 1.</param>
        /// <param name="pageSize">Page size, between 1 and 100.</param>
        /// <returns>A page of vehicles.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(PageResult<VehicleModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(
            [FromRoute] Guid fleetId,
            [FromQuery] VehicleStatus status = VehicleStatus.Available,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 20)
        {
            if (page < 1 || pageSize < 1 || pageSize > MaxPageSize || !Enum.IsDefined(status))
            {
                return BadRequest(new ProblemDetails
                {
                    Status = StatusCodes.Status400BadRequest,
                    Title = "Invalid parameters",
                    Detail = $"page must be at least 1, pageSize between 1 and {MaxPageSize} and status Available or Rented.",
                });
            }

            appLogger.LogDebug("Entering Get. Fleet: {fleetId}, Status: {status}, Page: {page}", fleetId, status, page);

            try
            {
                var result = await mediator.Send(new GetVehiclesQuery(fleetId, status, page, pageSize));
                return Ok(result);
            }
            catch (FleetNotFoundException ex)
            {
                return NotFound(new ProblemDetails
                {
                    Status = StatusCodes.Status404NotFound,
                    Title = "Fleet not found",
                    Detail = ex.Message,
                });
            }
        }
    }
}
