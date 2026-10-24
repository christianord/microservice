using System;
using System.Net.Http;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace GtMotive.Estimate.Microservice.IntegrationTests.Infrastructure
{
    /// <summary>
    /// Starts the real Host against a dedicated SQL Server database (docker-compose by default).
    /// Override the connection with the INTEGRATION_TESTS_CONNECTION environment variable.
    /// </summary>
    public sealed class IntegrationFixture : IAsyncLifetime
    {
        private const string DefaultConnection =
            "Server=localhost,11433;Database=rentcar_integration;User Id=sa;Password=SqlServer_Dev_123!;TrustServerCertificate=True";

        private WebApplicationFactory<Program> _factory;

        public HttpClient Client { get; private set; }

        public async Task InitializeAsync()
        {
            var connection = Environment.GetEnvironmentVariable("INTEGRATION_TESTS_CONNECTION") ?? DefaultConnection;

            Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Development");
            Environment.SetEnvironmentVariable("ConnectionStrings__DefaultConnection", connection);
            Environment.SetEnvironmentVariable("PathBase", "/");
            Environment.SetEnvironmentVariable("AppSettings__JwtAuthority", "http://localhost:18081/realms/microserviceTest");
            Environment.SetEnvironmentVariable("Swagger__AuthFlow", "AuthorizationCode");
            Environment.SetEnvironmentVariable("Swagger__EnableTryIt", "No");
            Environment.SetEnvironmentVariable("Swagger__Authority", "http://localhost:18081/realms/microserviceTest");
            Environment.SetEnvironmentVariable("Swagger__AuthorizationPath", "/protocol/openid-connect/auth");
            Environment.SetEnvironmentVariable("Swagger__TokenPath", "/protocol/openid-connect/token");
            Environment.SetEnvironmentVariable("Swagger__Scope", "openid");
            Environment.SetEnvironmentVariable("Swagger__ClientId", "swagger-ui");
            Environment.SetEnvironmentVariable("Swagger__ClientSecret", string.Empty);

            _factory = new WebApplicationFactory<Program>().WithWebHostBuilder(builder =>
            {
                builder.UseEnvironment("Development");
                builder.ConfigureTestServices(services =>
                {
                    services.AddAuthentication(options =>
                        {
                            options.DefaultScheme = TestAuthHandler.SchemeName;
                            options.DefaultAuthenticateScheme = TestAuthHandler.SchemeName;
                            options.DefaultChallengeScheme = TestAuthHandler.SchemeName;
                        })
                        .AddScheme<AuthenticationSchemeOptions, TestAuthHandler>(TestAuthHandler.SchemeName, _ => { });
                });
            });

            using (var scope = _factory.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<RentCarContext>();
                await context.Database.MigrateAsync();
            }

            Client = _factory.CreateClient();
            Client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", TestAuthHandler.SchemeName);
        }

        public async Task DisposeAsync()
        {
            Client?.Dispose();

            if (_factory != null)
            {
                await _factory.DisposeAsync();
            }
        }

        /// <summary>
        /// Creates a client that does not send credentials.
        /// </summary>
        /// <returns>The client.</returns>
        public HttpClient CreateAnonymousClient() => _factory.CreateClient();

        /// <summary>
        /// Changes the manufacture year directly in the database, simulating a vehicle that aged after admission.
        /// </summary>
        /// <param name="vehicleId">Vehicle id.</param>
        /// <param name="year">New manufacture year.</param>
        /// <returns>Task.</returns>
        public async Task SetManufactureYear(Guid vehicleId, int year)
        {
            using var scope = _factory.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<RentCarContext>();
            await context.Database.ExecuteSqlInterpolatedAsync($"UPDATE [Vehicle] SET [ManufactureYear] = {year} WHERE [Id] = {vehicleId}");
        }
    }
}
