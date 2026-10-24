using System;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Domain.Rentals;
using GtMotive.Estimate.Microservice.Domain.Vehicles;
using Microsoft.EntityFrameworkCore;

namespace GtMotive.Estimate.Microservice.Infrastructure.Persistence.Repositories
{
    internal sealed class RentalRepository(RentCarContext context) : IRentalRepository
    {
        public async Task Add(IRental rental)
        {
            await context.AddAsync(rental);
        }

        public async Task<bool> HasActiveByCustomer(Guid customerId)
        {
            return await context.Rentals.AsNoTracking().AnyAsync(r => r.CustomerId == customerId && r.ReturnDate == null);
        }

        public async Task<IRental> GetActiveByVehicle(VehicleId vehicleId)
        {
            var rental = await context.Rentals.FirstOrDefaultAsync(r => r.VehicleId == vehicleId && r.ReturnDate == null);

            return rental ?? throw new VehicleNotRentedException($"The vehicle {vehicleId.Value} has no active rental.");
        }
    }
}
