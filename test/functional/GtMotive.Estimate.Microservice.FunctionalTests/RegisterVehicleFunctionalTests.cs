using System;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Api.UseCases.RegisterFleet;
using GtMotive.Estimate.Microservice.Api.UseCases.RegisterVehicle;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.RegisterFleet;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.RegisterVehicle;
using GtMotive.Estimate.Microservice.FunctionalTests.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace GtMotive.Estimate.Microservice.FunctionalTests
{
    [Collection(TestCollections.Functional)]
    public sealed class RegisterVehicleFunctionalTests(CompositionRootTestFixture fixture)
    {
        [Fact]
        public async Task RegisterVehicleInExistingFleetPersistsAndReturnsCreated()
        {
            var fleetId = await CreateFleet();
            var plate = $"F-{Guid.NewGuid():N}"[..12];

            var result = await RegisterVehicle(fleetId, plate);

            Assert.Equal(StatusCodes.Status201Created, result.StatusCode);
            var body = Assert.IsType<RegisterVehicleResponse>(result.Value);
            Assert.Equal(fleetId, body.FleetId);
            Assert.Equal(plate.ToUpperInvariant(), body.LicensePlate);
            Assert.Equal("Available", body.Status);
        }

        [Fact]
        public async Task RegisterDuplicatedLicensePlateReturnsConflict()
        {
            var fleetId = await CreateFleet();
            var plate = $"D-{Guid.NewGuid():N}"[..12];

            var first = await RegisterVehicle(fleetId, plate);
            var second = await RegisterVehicle(fleetId, plate);

            Assert.Equal(StatusCodes.Status201Created, first.StatusCode);
            Assert.Equal(StatusCodes.Status409Conflict, second.StatusCode);
        }

        [Fact]
        public async Task RegisterVehicleInUnknownFleetReturnsNotFound()
        {
            var result = await RegisterVehicle(Guid.CreateVersion7(), "X-1");

            Assert.Equal(StatusCodes.Status404NotFound, result.StatusCode);
        }

        private async Task<Guid> CreateFleet()
        {
            ObjectResult result = null;

            await fixture.UsingRepository<IServiceProvider>(async sp =>
            {
                await sp.GetRequiredService<IUseCase<RegisterFleetInput>>()
                    .Execute(new RegisterFleetInput($"Fleet-{Guid.NewGuid():N}", 5));
                result = (ObjectResult)sp.GetRequiredService<RegisterFleetPresenter>().ActionResult;
            });

            return Assert.IsType<RegisterFleetResponse>(result.Value).FleetId;
        }

        private async Task<ObjectResult> RegisterVehicle(Guid fleetId, string plate)
        {
            ObjectResult result = null;

            await fixture.UsingRepository<IServiceProvider>(async sp =>
            {
                await sp.GetRequiredService<IUseCase<RegisterVehicleInput>>()
                    .Execute(new RegisterVehicleInput(fleetId, plate, "Seat", "Ibiza", DateTime.UtcNow.Year));
                result = (ObjectResult)sp.GetRequiredService<RegisterVehiclePresenter>().ActionResult;
            });

            return result;
        }
    }
}
