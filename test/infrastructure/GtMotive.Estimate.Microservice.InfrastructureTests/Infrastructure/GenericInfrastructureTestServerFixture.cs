using System;
using System.IO;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Xunit;

[assembly: CLSCompliant(false)]

namespace GtMotive.Estimate.Microservice.InfrastructureTests.Infrastructure
{
    public sealed class GenericInfrastructureTestServerFixture : IAsyncLifetime
    {
        public GenericInfrastructureTestServerFixture()
        {
            var hostBuilder = new WebHostBuilder()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseEnvironment("IntegrationTest")
                .UseDefaultServiceProvider(options => { options.ValidateScopes = true; })
                .ConfigureAppConfiguration((context, builder) => { builder.AddEnvironmentVariables(); })
                .UseStartup<Startup>();

            Server = new TestServer(hostBuilder);
        }

        public TestServer Server { get; }

        public Task InitializeAsync() => Server.Services.MigrateDatabaseAsync();

        public Task DisposeAsync()
        {
            Server.Dispose();
            return Task.CompletedTask;
        }
    }
}
