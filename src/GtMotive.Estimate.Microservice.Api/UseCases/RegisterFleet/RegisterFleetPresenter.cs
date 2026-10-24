using System;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.RegisterFleet;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GtMotive.Estimate.Microservice.Api.UseCases.RegisterFleet
{
    public sealed class RegisterFleetPresenter : IWebApiPresenter, IRegisterFleetOutputPort
    {
        public IActionResult ActionResult { get; private set; }

        public void StandardHandle(RegisterFleetOutput response)
        {
            ArgumentNullException.ThrowIfNull(response);

            var body = new RegisterFleetResponse(response.FleetId, response.Name, response.MaxVehicleAgeYears);
            ActionResult = new ObjectResult(body) { StatusCode = StatusCodes.Status201Created };
        }

        public void InvalidDataHandle(string message)
        {
            ActionResult = new BadRequestObjectResult(new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Invalid data",
                Detail = message,
            });
        }
    }
}
