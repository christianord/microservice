using System;
using System.Threading;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.ApplicationCore.Queries;
using GtMotive.Estimate.Microservice.Domain.Fleets;
using MediatR;

namespace GtMotive.Estimate.Microservice.Api.Queries.GetVehicles
{
    public sealed class GetVehiclesQueryHandler(
        IFleetRepository fleetRepository,
        IVehicleQueries vehicleQueries,
        TimeProvider timeProvider) : IRequestHandler<GetVehiclesQuery, PageResult<VehicleModel>>
    {
        public async Task<PageResult<VehicleModel>> Handle(GetVehiclesQuery request, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(request);

            var fleetId = new FleetId(request.FleetId);
            var fleet = await fleetRepository.GetBy(fleetId);

            var admission = fleet.AdmissionPolicy.ToExpression<VehicleModel>(
                v => v.ManufactureYear,
                timeProvider.GetUtcNow().Year);

            return await vehicleQueries.GetPage(fleetId, request.Status, admission, request.Page, request.PageSize);
        }
    }
}
