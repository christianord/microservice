using System;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.RegisterVehicle;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GtMotive.Estimate.Microservice.Api.UseCases.RegisterVehicle
{
    public sealed class RegisterVehiclePresenter : IWebApiPresenter, IRegisterVehicleOutputPort
    {
        public IActionResult ActionResult { get; private set; }

        public void StandardHandle(RegisterVehicleOutput response)
        {
            ArgumentNullException.ThrowIfNull(response);

            var body = new RegisterVehicleResponse(
                response.VehicleId,
                response.FleetId,
                response.LicensePlate,
                response.Brand,
                response.Model,
                response.ManufactureYear,
                response.Status);
            ActionResult = new ObjectResult(body) { StatusCode = StatusCodes.Status201Created };
        }

        public void FleetNotFoundHandle(string message) =>
            ActionResult = Problem(StatusCodes.Status404NotFound, "Fleet not found", message);

        public void VehicleTooOldHandle(string message) =>
            ActionResult = Problem(StatusCodes.Status400BadRequest, "Vehicle too old", message);

        public void AlreadyExistsHandle(string message) =>
            ActionResult = Problem(StatusCodes.Status409Conflict, "Vehicle already exists", message);

        public void InvalidDataHandle(string message) =>
            ActionResult = Problem(StatusCodes.Status400BadRequest, "Invalid data", message);

        private static ObjectResult Problem(int status, string title, string detail) =>
            new(new ProblemDetails { Status = status, Title = title, Detail = detail }) { StatusCode = status };
    }
}
