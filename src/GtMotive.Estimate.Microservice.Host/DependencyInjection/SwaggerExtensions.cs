using System;
using System.Collections.Generic;
using System.Reflection;
using GtMotive.Estimate.Microservice.Host.Configuration;
using GtMotive.Estimate.Microservice.Host.Infrastructure.Swagger;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace GtMotive.Estimate.Microservice.Host.DependencyInjection
{
    internal static class SwaggerExtensions
    {
        private static string AssemblyName => Assembly.GetEntryAssembly().GetName().Name;

        private static string AssemblyVersion => Assembly.GetEntryAssembly().GetName().Version.ToString();

        public static IServiceCollection AddSwagger(
            this IServiceCollection services,
            AppSettings settings,
            IConfiguration configuration)
        {
            var authority = (configuration.GetValue<string>("Swagger:Authority") ?? settings.JwtAuthority).TrimEnd('/');
            var authorizationUrl = new Uri(authority + (configuration.GetValue<string>("Swagger:AuthorizationPath") ?? "/connect/authorize"));
            var tokenUrl = new Uri(authority + (configuration.GetValue<string>("Swagger:TokenPath") ?? "/connect/token"));
            var scope = configuration.GetValue<string>("Swagger:Scope") ?? "estimate-public-scope";

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(
                options =>
                {
                    options.CustomSchemaIds(type => type.ToString());
                    options.SwaggerDoc($"v{AssemblyVersion}", new OpenApiInfo
                    {
                        Title = $"{AssemblyName} API",
                        Version = $"v{AssemblyVersion}",
                    });

                    if (configuration.GetValue<string>("Swagger:EnableTryIt") == "Yes")
                    {
                        // Define the OAuth2.0 scheme that's in use (i.e. Implicit Flow)
                        options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                        {
                            Type = SecuritySchemeType.OAuth2,
                            Name = "oauth2",
                            Flows = configuration.GetValue<string>("Swagger:AuthFlow") == "AuthorizationCode"
                                ? new OpenApiOAuthFlows
                                {
                                    AuthorizationCode = new OpenApiOAuthFlow
                                    {
                                        AuthorizationUrl = authorizationUrl,
                                        Scopes = new Dictionary<string, string>
                                        {
                                            [scope] = "estimate-api"
                                        },
                                        TokenUrl = tokenUrl
                                    }
                                }
                                : new OpenApiOAuthFlows()
                                {
                                    ClientCredentials = new OpenApiOAuthFlow()
                                    {
                                        Scopes = new Dictionary<string, string>
                                        {
                                            [scope] = "estimate-api"
                                        },
                                        TokenUrl = tokenUrl
                                    }
                                }
                        });

                        options.OperationFilter<IdentityServerApiSecurityOperationFilter>();
                    }
                });

            return services;
        }

        public static IApplicationBuilder UseSwaggerInApplication(
            this IApplicationBuilder app,
            PathBase pathBase,
            IConfiguration configuration)
        {
            ArgumentNullException.ThrowIfNull(pathBase);

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger(options =>
            {
                if (!pathBase.IsDefault)
                {
                    options.SerializeAsV2 = true;
                    options.RouteTemplate = "swagger/{documentName}/swagger.json";
                    options.PreSerializeFilters.Add((document, request) =>
                    {
                        document.Servers =
                        [
                            new OpenApiServer
                            {
                                Url = $"{request.Scheme}://{request.Host.Value}{pathBase.CurrentWithoutTrailingSlash}"
                            }

                        ];
                    });
                }
            });

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
            var url = pathBase.IsDefault
                ? $"/swagger/v{AssemblyVersion}/swagger.json"
                : $"{pathBase.CurrentWithoutTrailingSlash}/swagger/v{AssemblyVersion}/swagger.json";

            app.UseSwaggerUI(
                options =>
                {
                    options.SwaggerEndpoint(url, $"{AssemblyName} API V{AssemblyVersion}");

                    if (configuration.GetValue<string>("Swagger:EnableTryIt") == "No")
                    {
                        options.SupportedSubmitMethods();
                    }

                    options.OAuthClientId(configuration.GetValue<string>("Swagger:ClientId") ?? "client-gtestimate-swagger");
                    var clientSecret = configuration.GetValue<string>("Swagger:ClientSecret") ?? "gtmotive";
                    if (!string.IsNullOrEmpty(clientSecret))
                    {
                        options.OAuthClientSecret(clientSecret);
                    }

                    options.OAuthUsePkce();

                    // Fuerza a que el proveedor de identidad pida credenciales en cada autorizacion.
                    options.OAuthAdditionalQueryStringParams(new Dictionary<string, string>
                    {
                        ["prompt"] = "login"
                    });
                    options.OAuthScopeSeparator(" ");
                });

            return app;
        }
    }
}
