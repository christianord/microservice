using System;
using GtMotive.Estimate.Microservice.ApplicationCore.Queries;
using GtMotive.Estimate.Microservice.Domain.Vehicles;
using MediatR;

namespace GtMotive.Estimate.Microservice.Api.Queries.GetVehicles
{
    /// <summary>
    /// Query that lists the vehicles of a fleet.
    /// </summary>
    /// <param name="FleetId">Fleet id.</param>
    /// <param name="Status">Status filter.</param>
    /// <param name="Page">Page number (1-based).</param>
    /// <param name="PageSize">Page size.</param>
    public sealed record GetVehiclesQuery(Guid FleetId, VehicleStatus Status, int Page, int PageSize) : IRequest<PageResult<VehicleModel>>;
}
