using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace GtMotive.Estimate.Microservice.IntegrationTests.Infrastructure
{
    /// <summary>
    /// Small helper that wraps the API calls used by the scenarios.
    /// </summary>
    internal sealed class ApiDriver(HttpClient client)
    {
        public static int CurrentYear => DateTime.UtcNow.Year;

        public async Task<Guid> CreateFleet(int maxAge = 5)
        {
            var response = await client.PostAsJsonAsync("/api/fleets", new { name = $"Fleet {Guid.NewGuid():N}", maxVehicleAgeYears = maxAge });
            response.EnsureSuccessStatusCode();
            return (await ReadJson(response)).GetProperty("fleetId").GetGuid();
        }

        public async Task<Guid> CreateVehicle(Guid fleetId, int? year = null)
        {
            var response = await RegisterVehicle(fleetId, $"IT-{Guid.NewGuid():N}"[..14], year ?? CurrentYear);
            response.EnsureSuccessStatusCode();
            return (await ReadJson(response)).GetProperty("vehicleId").GetGuid();
        }

        public Task<HttpResponseMessage> RegisterVehicle(Guid fleetId, string plate, int year) =>
            client.PostAsJsonAsync($"/api/fleets/{fleetId}/vehicles", new { licensePlate = plate, brand = "Seat", model = "Leon", manufactureYear = year });

        public Task<HttpResponseMessage> Rent(Guid vehicleId, Guid customerId) =>
            client.PostAsJsonAsync($"/api/vehicles/{vehicleId}/rentals", new { customerId });

        public Task<HttpResponseMessage> Return(Guid vehicleId, Guid customerId) =>
            client.PostAsJsonAsync($"/api/vehicles/{vehicleId}/returns", new { customerId });

        public Task<HttpResponseMessage> List(Guid fleetId, string query = "") =>
            client.GetAsync($"/api/fleets/{fleetId}/vehicles{query}");

        public async Task<JsonElement> ReadJson(HttpResponseMessage response)
        {
            var text = await response.Content.ReadAsStringAsync();
            return JsonDocument.Parse(text).RootElement.Clone();
        }
    }
}
