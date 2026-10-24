using System;
using System.Diagnostics.CodeAnalysis;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.RegisterFleet;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.RegisterVehicle;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.RentVehicle;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.ReturnVehicle;
using GtMotive.Estimate.Microservice.Domain.Rentals;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

[assembly: CLSCompliant(false)]

namespace GtMotive.Estimate.Microservice.ApplicationCore
{
    /// <summary>
    /// Adds Use Cases classes.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public static class ApplicationConfiguration
    {
        /// <summary>
        /// Adds Use Cases to the ServiceCollection.
        /// </summary>
        /// <param name="services">Service Collection.</param>
        /// <returns>The modified instance.</returns>
        public static IServiceCollection AddUseCases(this IServiceCollection services)
        {
            services.AddScoped<IUseCase<RegisterFleetInput>, RegisterFleetUseCase>();
            services.AddScoped<IUseCase<RegisterVehicleInput>, RegisterVehicleUseCase>();
            services.AddScoped<RentalService>();
            services.AddScoped<IUseCase<RentVehicleInput>, RentVehicleUseCase>();
            services.AddScoped<IUseCase<ReturnVehicleInput>, ReturnVehicleUseCase>();
            services.TryAddSingleton(TimeProvider.System);

            return services;
        }
    }
}
