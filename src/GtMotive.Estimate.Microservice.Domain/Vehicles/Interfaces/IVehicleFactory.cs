using GtMotive.Estimate.Microservice.Domain.Fleets;

namespace GtMotive.Estimate.Microservice.Domain.Vehicles
{
    /// <summary>
    /// Creates vehicles.
    /// </summary>
    public interface IVehicleFactory
    {
        /// <summary>
        /// Creates a new available vehicle.
        /// </summary>
        /// <param name="fleetId">Fleet id.</param>
        /// <param name="licensePlate">License plate.</param>
        /// <param name="brand">Brand.</param>
        /// <param name="model">Model.</param>
        /// <param name="manufactureYear">Manufacture year.</param>
        /// <returns>The new vehicle.</returns>
        IVehicle NewVehicle(FleetId fleetId, LicensePlate licensePlate, string brand, string model, ManufactureYear manufactureYear);
    }
}
