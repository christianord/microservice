using System;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.RentVehicle;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GtMotive.Estimate.Microservice.Api.UseCases.RentVehicle
{
    public sealed class RentVehiclePresenter : IWebApiPresenter, IRentVehicleOutputPort
    {
        public IActionResult ActionResult { get; private set; }

        public void StandardHandle(RentVehicleOutput response)
        {
            ArgumentNullException.ThrowIfNull(response);

            var body = new RentVehicleResponse(response.RentalId, response.VehicleId, response.CustomerId, response.RentalDate);
            ActionResult = new ObjectResult(body) { StatusCode = StatusCodes.Status201Created };
        }

        public void VehicleNotFoundHandle(string message) =>
            ActionResult = Problem(StatusCodes.Status404NotFound, "Vehicle not found", message);

        public void VehicleNotAvailableHandle(string message) =>
            ActionResult = Problem(StatusCodes.Status409Conflict, "Vehicle not available", message);

        public void CustomerAlreadyHasRentalHandle(string message) =>
            ActionResult = Problem(StatusCodes.Status409Conflict, "Customer already has an active rental", message);

        public void VehicleTooOldHandle(string message) =>
            ActionResult = Problem(StatusCodes.Status409Conflict, "Vehicle too old", message);

        public void InvalidDataHandle(string message) =>
            ActionResult = Problem(StatusCodes.Status400BadRequest, "Invalid data", message);

        private static ObjectResult Problem(int status, string title, string detail) =>
            new(new ProblemDetails { Status = status, Title = title, Detail = detail }) { StatusCode = status };
    }
}
