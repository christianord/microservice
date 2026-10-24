using System;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.ReturnVehicle;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GtMotive.Estimate.Microservice.Api.UseCases.ReturnVehicle
{
    public sealed class ReturnVehiclePresenter : IWebApiPresenter, IReturnVehicleOutputPort
    {
        public IActionResult ActionResult { get; private set; }

        public void StandardHandle(ReturnVehicleOutput response)
        {
            ArgumentNullException.ThrowIfNull(response);

            var body = new ReturnVehicleResponse(response.RentalId, response.RentalDate, response.ReturnDate, response.RentalDays);
            ActionResult = new OkObjectResult(body);
        }

        public void VehicleNotFoundHandle(string message) =>
            ActionResult = Problem(StatusCodes.Status404NotFound, "Vehicle not found", message);

        public void NotRentedHandle(string message) =>
            ActionResult = Problem(StatusCodes.Status409Conflict, "Vehicle not rented", message);

        public void NotRenterHandle(string message) =>
            ActionResult = Problem(StatusCodes.Status403Forbidden, "Not the renter", message);

        public void InvalidDataHandle(string message) =>
            ActionResult = Problem(StatusCodes.Status400BadRequest, "Invalid data", message);

        private static ObjectResult Problem(int status, string title, string detail) =>
            new(new ProblemDetails { Status = status, Title = title, Detail = detail }) { StatusCode = status };
    }
}
