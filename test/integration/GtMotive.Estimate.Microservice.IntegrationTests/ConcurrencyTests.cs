using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.IntegrationTests.Infrastructure;
using Xunit;

namespace GtMotive.Estimate.Microservice.IntegrationTests
{
    [Collection(IntegrationCollection.Name)]
    public sealed class ConcurrencyTests(IntegrationFixture fixture)
    {
        private readonly ApiDriver _api = new(fixture.Client);

        [Fact]
        public async Task DifferentCustomersRentingTheSameVehicleSimultaneouslyOnlyOneSucceeds()
        {
            var fleetId = await _api.CreateFleet();
            var vehicleId = await _api.CreateVehicle(fleetId);

            var responses = await Task.WhenAll(Enumerable.Range(0, 10).Select(_ => Task.Run(() => _api.Rent(vehicleId, Guid.NewGuid()))));

            AssertOneSuccessRestConflict(responses, HttpStatusCode.Created);
        }

        [Fact]
        public async Task SameCustomerRentingDifferentVehiclesSimultaneouslyOnlyOneSucceeds()
        {
            var fleetId = await _api.CreateFleet();
            var customer = Guid.NewGuid();
            var vehicles = new Guid[6];
            for (var i = 0; i < vehicles.Length; i++)
            {
                vehicles[i] = await _api.CreateVehicle(fleetId);
            }

            var responses = await Task.WhenAll(vehicles.Select(v => Task.Run(() => _api.Rent(v, customer))));

            AssertOneSuccessRestConflict(responses, HttpStatusCode.Created);
        }

        [Fact]
        public async Task ReturningTheSameVehicleSimultaneouslyOnlyOneSucceeds()
        {
            var fleetId = await _api.CreateFleet();
            var vehicleId = await _api.CreateVehicle(fleetId);
            var customer = Guid.NewGuid();
            Assert.Equal(HttpStatusCode.Created, (await _api.Rent(vehicleId, customer)).StatusCode);

            var responses = await Task.WhenAll(Enumerable.Range(0, 6).Select(_ => Task.Run(() => _api.Return(vehicleId, customer))));

            AssertOneSuccessRestConflict(responses, HttpStatusCode.OK);
        }

        private static void AssertOneSuccessRestConflict(HttpResponseMessage[] responses, HttpStatusCode success)
        {
            Assert.Equal(1, responses.Count(r => r.StatusCode == success));
            Assert.Equal(responses.Length - 1, responses.Count(r => r.StatusCode == HttpStatusCode.Conflict));
        }
    }
}
