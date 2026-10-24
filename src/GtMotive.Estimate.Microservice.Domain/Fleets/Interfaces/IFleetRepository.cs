using System.Threading.Tasks;

namespace GtMotive.Estimate.Microservice.Domain.Fleets
{
    /// <summary>
    /// Fleet repository.
    /// </summary>
    public interface IFleetRepository
    {
        /// <summary>
        /// Tracks a new fleet.
        /// </summary>
        /// <param name="fleet">Fleet.</param>
        /// <returns>Task.</returns>
        Task Add(IFleet fleet);

        /// <summary>
        /// Gets a fleet.
        /// </summary>
        /// <param name="fleetId">Fleet id.</param>
        /// <returns>The fleet.</returns>
        /// <exception cref="FleetNotFoundException">When the fleet does not exist.</exception>
        Task<IFleet> GetBy(FleetId fleetId);
    }
}
