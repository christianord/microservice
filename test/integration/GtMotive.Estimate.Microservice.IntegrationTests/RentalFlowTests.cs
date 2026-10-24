using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.IntegrationTests.Infrastructure;
using Xunit;

namespace GtMotive.Estimate.Microservice.IntegrationTests
{
    [Collection(IntegrationCollection.Name)]
    public sealed class RentalFlowTests(IntegrationFixture fixture)
    {
        private readonly ApiDriver _api = new(fixture.Client);

        [Fact]
        public async Task RequestsWithoutCredentialsAreRejected()
        {
            using var anonymous = fixture.CreateAnonymousClient();

            var response = await anonymous.GetAsync($"/api/fleets/{Guid.NewGuid()}/vehicles");

            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async Task FullFlowRegisterListRentListReturnList()
        {
            var fleetId = await _api.CreateFleet();
            var vehicleId = await _api.CreateVehicle(fleetId);
            var customerId = Guid.NewGuid();

            var available = await _api.ReadJson(await _api.List(fleetId));
            Assert.Equal(1, available.GetProperty("totalCount").GetInt32());

            var rent = await _api.Rent(vehicleId, customerId);
            Assert.Equal(HttpStatusCode.Created, rent.StatusCode);

            var availableAfterRent = await _api.ReadJson(await _api.List(fleetId));
            var rented = await _api.ReadJson(await _api.List(fleetId, "?status=Rented"));
            Assert.Equal(0, availableAfterRent.GetProperty("totalCount").GetInt32());
            Assert.Equal(1, rented.GetProperty("totalCount").GetInt32());

            var returned = await _api.Return(vehicleId, customerId);
            Assert.Equal(HttpStatusCode.OK, returned.StatusCode);
            var body = await _api.ReadJson(returned);
            Assert.Equal(1, body.GetProperty("rentalDays").GetInt32());

            var availableAgain = await _api.ReadJson(await _api.List(fleetId));
            Assert.Equal(1, availableAgain.GetProperty("totalCount").GetInt32());
        }

        [Fact]
        public async Task RegisteringVehicleOlderThanPolicyReturnsBadRequest()
        {
            var fleetId = await _api.CreateFleet(maxAge: 5);

            var response = await _api.RegisterVehicle(fleetId, $"OLD-{Guid.NewGuid():N}"[..12], ApiDriver.CurrentYear - 6);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task RegisteringDuplicatedLicensePlateReturnsConflict()
        {
            var fleetId = await _api.CreateFleet();
            var plate = $"DUP-{Guid.NewGuid():N}"[..12];

            var first = await _api.RegisterVehicle(fleetId, plate, ApiDriver.CurrentYear);
            var second = await _api.RegisterVehicle(fleetId, plate, ApiDriver.CurrentYear);

            Assert.Equal(HttpStatusCode.Created, first.StatusCode);
            Assert.Equal(HttpStatusCode.Conflict, second.StatusCode);
        }

        [Fact]
        public async Task ListingUnknownFleetReturnsNotFoundAndInvalidPageSizeReturnsBadRequest()
        {
            var unknown = await _api.List(Guid.CreateVersion7());
            Assert.Equal(HttpStatusCode.NotFound, unknown.StatusCode);

            var fleetId = await _api.CreateFleet();
            var invalid = await _api.List(fleetId, "?pageSize=500");
            Assert.Equal(HttpStatusCode.BadRequest, invalid.StatusCode);
        }

        [Fact]
        public async Task ListingIsPaginatedWithStableOrderAndTotals()
        {
            var fleetId = await _api.CreateFleet();
            for (var i = 0; i < 5; i++)
            {
                await _api.CreateVehicle(fleetId);
            }

            var page1 = await _api.ReadJson(await _api.List(fleetId, "?page=1&pageSize=2"));
            var page2 = await _api.ReadJson(await _api.List(fleetId, "?page=2&pageSize=2"));
            var page3 = await _api.ReadJson(await _api.List(fleetId, "?page=3&pageSize=2"));

            Assert.Equal(5, page1.GetProperty("totalCount").GetInt32());
            Assert.Equal(3, page1.GetProperty("totalPages").GetInt32());
            Assert.Equal(2, page1.GetProperty("items").GetArrayLength());
            Assert.Equal(1, page3.GetProperty("items").GetArrayLength());

            var ids = new[] { page1, page2, page3 }
                .SelectMany(p => p.GetProperty("items").EnumerateArray().Select(i => i.GetProperty("vehicleId").GetGuid()))
                .ToList();
            Assert.Equal(5, ids.Distinct().Count());
        }

        [Fact]
        public async Task ListingExcludesVehiclesOutsideThePolicyYearLimit()
        {
            var fleetId = await _api.CreateFleet(maxAge: 5);
            var recent = await _api.CreateVehicle(fleetId);
            var aged = await _api.CreateVehicle(fleetId);
            await fixture.SetManufactureYear(aged, ApiDriver.CurrentYear - 10);

            var list = await _api.ReadJson(await _api.List(fleetId));

            var ids = list.GetProperty("items").EnumerateArray().Select(i => i.GetProperty("vehicleId").GetGuid()).ToList();
            Assert.Contains(recent, ids);
            Assert.DoesNotContain(aged, ids);
        }

        [Fact]
        public async Task VehicleThatBecameTooOldCannotBeRented()
        {
            var fleetId = await _api.CreateFleet(maxAge: 5);
            var vehicleId = await _api.CreateVehicle(fleetId);
            await fixture.SetManufactureYear(vehicleId, ApiDriver.CurrentYear - 10);

            var response = await _api.Rent(vehicleId, Guid.NewGuid());

            Assert.Equal(HttpStatusCode.Conflict, response.StatusCode);
        }

        [Fact]
        public async Task RentalErrorsMapToTheExpectedStatusCodes()
        {
            var fleetId = await _api.CreateFleet();
            var vehicleId = await _api.CreateVehicle(fleetId);
            var otherVehicleId = await _api.CreateVehicle(fleetId);
            var customer = Guid.NewGuid();
            var other = Guid.NewGuid();

            Assert.Equal(HttpStatusCode.NotFound, (await _api.Rent(Guid.CreateVersion7(), customer)).StatusCode);
            Assert.Equal(HttpStatusCode.Conflict, (await _api.Return(vehicleId, customer)).StatusCode);

            Assert.Equal(HttpStatusCode.Created, (await _api.Rent(vehicleId, customer)).StatusCode);
            Assert.Equal(HttpStatusCode.Conflict, (await _api.Rent(vehicleId, other)).StatusCode);
            Assert.Equal(HttpStatusCode.Conflict, (await _api.Rent(otherVehicleId, customer)).StatusCode);
            Assert.Equal(HttpStatusCode.Forbidden, (await _api.Return(vehicleId, other)).StatusCode);
        }

        [Fact]
        public async Task CustomerCanRentAgainAfterReturning()
        {
            var fleetId = await _api.CreateFleet();
            var first = await _api.CreateVehicle(fleetId);
            var second = await _api.CreateVehicle(fleetId);
            var customer = Guid.NewGuid();

            Assert.Equal(HttpStatusCode.Created, (await _api.Rent(first, customer)).StatusCode);
            Assert.Equal(HttpStatusCode.OK, (await _api.Return(first, customer)).StatusCode);
            Assert.Equal(HttpStatusCode.Created, (await _api.Rent(second, customer)).StatusCode);
        }
    }
}
