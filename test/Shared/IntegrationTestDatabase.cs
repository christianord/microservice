using System;

namespace GtMotive.Estimate.Microservice.Testing
{
    internal static class IntegrationTestDatabase
    {
        private const string DefaultConnection =
            "Server=localhost,11433;Database=rentcar_integration;User Id=sa;Password=SqlServer_Dev_123!;TrustServerCertificate=True";

        public static string GetConnectionString(string configuredConnection = null) =>
            Environment.GetEnvironmentVariable("INTEGRATION_TESTS_CONNECTION")
            ?? configuredConnection
            ?? DefaultConnection;
    }
}
