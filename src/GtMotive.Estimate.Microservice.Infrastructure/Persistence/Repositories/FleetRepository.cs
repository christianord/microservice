using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Domain.Fleets;
using Microsoft.EntityFrameworkCore;

namespace GtMotive.Estimate.Microservice.Infrastructure.Persistence.Repositories
{
    internal sealed class FleetRepository(RentCarContext context) : IFleetRepository
    {
        public async Task Add(IFleet fleet)
        {
            await context.AddAsync(fleet);
        }

        public async Task<IFleet> GetBy(FleetId fleetId)
        {
            var fleet = await context.Fleets.SingleOrDefaultAsync(f => f.Id == fleetId);

            return fleet ?? throw new FleetNotFoundException($"The fleet {fleetId} does not exist.");
        }
    }
}
