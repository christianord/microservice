using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.ApplicationCore.Queries;
using GtMotive.Estimate.Microservice.Domain.Fleets;
using GtMotive.Estimate.Microservice.Domain.Interfaces;
using GtMotive.Estimate.Microservice.Domain.Rentals;
using GtMotive.Estimate.Microservice.Domain.Vehicles;
using GtMotive.Estimate.Microservice.Infrastructure.Interfaces;
using GtMotive.Estimate.Microservice.Infrastructure.Logging;
using GtMotive.Estimate.Microservice.Infrastructure.Persistence;
using GtMotive.Estimate.Microservice.Infrastructure.Persistence.Queries;
using GtMotive.Estimate.Microservice.Infrastructure.Persistence.Repositories;
using GtMotive.Estimate.Microservice.Infrastructure.Telemetry;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

[assembly: CLSCompliant(false)]

namespace GtMotive.Estimate.Microservice.Infrastructure
{
    public static class InfrastructureConfiguration
    {
        [ExcludeFromCodeCoverage]
        public static IInfrastructureBuilder AddBaseInfrastructure(
            this IServiceCollection services,
            bool isDevelopment)
        {
            services.AddScoped(typeof(IAppLogger<>), typeof(LoggerAdapter<>));

            if (!isDevelopment)
            {
                services.AddScoped<ITelemetry, AppTelemetry>();
            }
            else
            {
                services.AddScoped<ITelemetry, NoOpTelemetry>();
            }

            return new InfrastructureBuilder(services);
        }

        [ExcludeFromCodeCoverage]
        public static IServiceCollection AddPersistence(
            this IServiceCollection services,
            string connectionString)
        {
            services.AddDbContext<RentCarContext>(options => options.UseSqlServer(connectionString));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<EntityFactory>();
            services.AddScoped<IFleetFactory>(sp => sp.GetRequiredService<EntityFactory>());
            services.AddScoped<IFleetRepository, FleetRepository>();
            services.AddScoped<IVehicleFactory>(sp => sp.GetRequiredService<EntityFactory>());
            services.AddScoped<IVehicleRepository, VehicleRepository>();
            services.AddScoped<IRentalFactory>(sp => sp.GetRequiredService<EntityFactory>());
            services.AddScoped<IRentalRepository, RentalRepository>();
            services.AddScoped<IVehicleQueries, VehicleQueries>();

            return services;
        }

        [ExcludeFromCodeCoverage]
        public static async Task MigrateDatabaseAsync(this IServiceProvider serviceProvider)
        {
            ArgumentNullException.ThrowIfNull(serviceProvider);

            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<RentCarContext>();
            await context.Database.MigrateAsync();
        }

        private sealed class InfrastructureBuilder(IServiceCollection services) : IInfrastructureBuilder
        {
            public IServiceCollection Services { get; } = services;
        }
    }
}
