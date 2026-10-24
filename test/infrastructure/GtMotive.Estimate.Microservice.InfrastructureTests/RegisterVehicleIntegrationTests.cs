using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.InfrastructureTests.Infrastructure;
using Xunit;

namespace GtMotive.Estimate.Microservice.InfrastructureTests
{
    [Collection(TestCollections.TestServer)]
    public sealed class RegisterVehicleIntegrationTests : IDisposable
    {
        private readonly HttpClient _client;

        public RegisterVehicleIntegrationTests(GenericInfrastructureTestServerFixture fixture)
        {
            ArgumentNullException.ThrowIfNull(fixture);

            _client = fixture.Server.CreateClient()
                .WithDefaultIdentity([new Claim("sub", "integration-test")]);
        }

        [Fact]
        public async Task RegisterVehicleInExistingFleetReturnsCreated()
        {
            var fleetId = await CreateFleet();
            var plate = $"IT-{Guid.NewGuid():N}"[..12];

            var response = await RegisterVehicle(fleetId, plate);

            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            var body = JsonDocument.Parse(await response.Content.ReadAsStringAsync()).RootElement;
            Assert.Equal(fleetId, body.GetProperty("fleetId").GetGuid());
            Assert.Equal(plate.ToUpperInvariant(), body.GetProperty("licensePlate").GetString());
        }

        [Fact]
        public async Task RegisterDuplicatedLicensePlateReturnsConflict()
        {
            var fleetId = await CreateFleet();
            var plate = $"DP-{Guid.NewGuid():N}"[..12];

            var first = await RegisterVehicle(fleetId, plate);
            var second = await RegisterVehicle(fleetId, plate);

            Assert.Equal(HttpStatusCode.Created, first.StatusCode);
            Assert.Equal(HttpStatusCode.Conflict, second.StatusCode);
        }

        [Fact]
        public async Task RegisterVehicleInUnknownFleetReturnsNotFound()
        {
            var response = await RegisterVehicle(Guid.CreateVersion7(), "X-1");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        public void Dispose()
        {
            _client.Dispose();
        }

        private async Task<Guid> CreateFleet()
        {
            var response = await _client.PostAsJsonAsync(
                new Uri("/api/fleets", UriKind.Relative),
                new { name = $"Fleet {Guid.NewGuid():N}", maxVehicleAgeYears = 5 });
            response.EnsureSuccessStatusCode();
            var body = JsonDocument.Parse(await response.Content.ReadAsStringAsync()).RootElement;
            return body.GetProperty("fleetId").GetGuid();
        }

        private Task<HttpResponseMessage> RegisterVehicle(Guid fleetId, string plate) =>
            _client.PostAsJsonAsync(
                new Uri($"/api/fleets/{fleetId}/vehicles", UriKind.Relative),
                new { licensePlate = plate, brand = "Seat", model = "Ibiza", manufactureYear = DateTime.UtcNow.Year });
    }
}
