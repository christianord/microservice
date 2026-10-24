using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Api.UseCases.RegisterFleet;
using GtMotive.Estimate.Microservice.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GtMotive.Estimate.Microservice.Api.Controllers
{
    /// <summary>
    /// Fleets endpoints.
    /// </summary>
    [ApiController]
    [Authorize]
    [Route("api/fleets")]
    public sealed class FleetsController(IMediator mediator, IAppLogger<FleetsController> appLogger) : ControllerBase
    {
        /// <summary>
        /// Registers a fleet.
        /// </summary>
        /// <param name="request">Fleet data.</param>
        /// <returns>The registered fleet.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(RegisterFleetResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromBody][Required] RegisterFleetRequest request)
        {
            ArgumentNullException.ThrowIfNull(request);

            appLogger.LogDebug("Entering Post. Request: {@request}", request);
            var presenter = await mediator.Send(request);
            return presenter.ActionResult;
        }
    }
}
