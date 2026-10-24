using System.ComponentModel.DataAnnotations;

namespace GtMotive.Estimate.Microservice.Api.UseCases.RegisterVehicle
{
    /// <summary>
    /// Body of the register vehicle endpoint.
    /// </summary>
    /// <param name="LicensePlate">License plate.</param>
    /// <param name="Brand">Brand.</param>
    /// <param name="Model">Model.</param>
    /// <param name="ManufactureYear">Manufacture year.</param>
    public sealed record RegisterVehicleBody(
        [Required] string LicensePlate,
        [Required] string Brand,
        [Required] string Model,
        [Required] int? ManufactureYear);
}
