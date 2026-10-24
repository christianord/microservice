using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.ApplicationCore.Queries;
using GtMotive.Estimate.Microservice.Domain.Fleets;
using GtMotive.Estimate.Microservice.Domain.Vehicles;
using Microsoft.EntityFrameworkCore;

namespace GtMotive.Estimate.Microservice.Infrastructure.Persistence.Queries
{
    internal sealed class VehicleQueries(RentCarContext context) : IVehicleQueries
    {
        public async Task<PageResult<VehicleModel>> GetPage(
            FleetId fleetId,
            VehicleStatus status,
            Expression<Func<VehicleModel, bool>> admission,
            int page,
            int pageSize)
        {
            ArgumentNullException.ThrowIfNull(admission);

            var fleetValue = fleetId.Value;

            var query = context.Set<VehicleModel>()
                .AsNoTracking()
                .Where(v => v.FleetId == fleetValue && v.Status == status)
                .Where(admission);

            var totalCount = await query.CountAsync();

            var items = await query
                .OrderBy(v => v.ManufactureYear)
                .ThenBy(v => v.VehicleId)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PageResult<VehicleModel>(items, page, pageSize, totalCount);
        }
    }
}
