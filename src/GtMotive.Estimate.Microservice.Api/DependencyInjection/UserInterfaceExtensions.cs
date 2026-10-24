using GtMotive.Estimate.Microservice.Api.UseCases.RegisterFleet;
using GtMotive.Estimate.Microservice.Api.UseCases.RegisterVehicle;
using GtMotive.Estimate.Microservice.Api.UseCases.RentVehicle;
using GtMotive.Estimate.Microservice.Api.UseCases.ReturnVehicle;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.RegisterFleet;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.RegisterVehicle;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.RentVehicle;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.ReturnVehicle;
using Microsoft.Extensions.DependencyInjection;

namespace GtMotive.Estimate.Microservice.Api.DependencyInjection
{
    public static class UserInterfaceExtensions
    {
        public static IServiceCollection AddPresenters(this IServiceCollection services)
        {
            services.AddScoped<RegisterFleetPresenter>();
            services.AddScoped<IRegisterFleetOutputPort>(sp => sp.GetRequiredService<RegisterFleetPresenter>());
            services.AddScoped<RegisterVehiclePresenter>();
            services.AddScoped<IRegisterVehicleOutputPort>(sp => sp.GetRequiredService<RegisterVehiclePresenter>());
            services.AddScoped<RentVehiclePresenter>();
            services.AddScoped<IRentVehicleOutputPort>(sp => sp.GetRequiredService<RentVehiclePresenter>());
            services.AddScoped<ReturnVehiclePresenter>();
            services.AddScoped<IReturnVehicleOutputPort>(sp => sp.GetRequiredService<ReturnVehiclePresenter>());
            return services;
        }
    }
}
