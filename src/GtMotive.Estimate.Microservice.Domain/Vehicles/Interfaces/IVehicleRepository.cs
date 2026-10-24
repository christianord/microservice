using System.Threading.Tasks;

namespace GtMotive.Estimate.Microservice.Domain.Vehicles
{
    /// <summary>
    /// Vehicle repository. It only tracks changes; persisting is done by the unit of work.
    /// </summary>
    public interface IVehicleRepository
    {
        /// <summary>
        /// Tracks a new vehicle.
        /// </summary>
        /// <param name="vehicle">Vehicle.</param>
        /// <returns>Task.</returns>
        Task Add(IVehicle vehicle);

        /// <summary>
        /// Checks whether a vehicle with the license plate exists.
        /// </summary>
        /// <param name="licensePlate">License plate.</param>
        /// <returns>True if it exists.</returns>
        Task<bool> Exists(LicensePlate licensePlate);

        /// <summary>
        /// Gets a tracked vehicle.
        /// </summary>
        /// <param name="vehicleId">Vehicle id.</param>
        /// <returns>The vehicle.</returns>
        /// <exception cref="VehicleNotFoundException">When it does not exist.</exception>
        Task<IVehicle> GetBy(VehicleId vehicleId);
    }
}
