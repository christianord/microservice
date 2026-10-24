using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Domain.Vehicles;
using Microsoft.EntityFrameworkCore;

namespace GtMotive.Estimate.Microservice.Infrastructure.Persistence.Repositories
{
    internal sealed class VehicleRepository(RentCarContext context) : IVehicleRepository
    {
        public async Task Add(IVehicle vehicle)
        {
            await context.AddAsync(vehicle);
        }

        public async Task<bool> Exists(LicensePlate licensePlate)
        {
            return await context.Vehicles.AsNoTracking().AnyAsync(v => v.LicensePlate == licensePlate);
        }

        public async Task<IVehicle> GetBy(VehicleId vehicleId)
        {
            var vehicle = await context.Vehicles.FirstOrDefaultAsync(v => v.Id == vehicleId);

            return vehicle ?? throw new VehicleNotFoundException($"The vehicle {vehicleId.Value} was not found.");
        }
    }
}
