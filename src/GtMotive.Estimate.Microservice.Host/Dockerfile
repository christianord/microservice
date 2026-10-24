FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /repo
COPY ["global.json", "Directory.Build.props", "Directory.Build.targets", "NuGet.config", "./"]
COPY ["src/GtMotive.Estimate.Microservice.Host/GtMotive.Estimate.Microservice.Host.csproj", "src/GtMotive.Estimate.Microservice.Host/"]
COPY ["src/GtMotive.Estimate.Microservice.Api/GtMotive.Estimate.Microservice.Api.csproj", "src/GtMotive.Estimate.Microservice.Api/"]
COPY ["src/GtMotive.Estimate.Microservice.ApplicationCore/GtMotive.Estimate.Microservice.ApplicationCore.csproj", "src/GtMotive.Estimate.Microservice.ApplicationCore/"]
COPY ["src/GtMotive.Estimate.Microservice.Domain/GtMotive.Estimate.Microservice.Domain.csproj", "src/GtMotive.Estimate.Microservice.Domain/"]
COPY ["src/GtMotive.Estimate.Microservice.Infrastructure/GtMotive.Estimate.Microservice.Infrastructure.csproj", "src/GtMotive.Estimate.Microservice.Infrastructure/"]
RUN dotnet restore "src/GtMotive.Estimate.Microservice.Host/GtMotive.Estimate.Microservice.Host.csproj"
COPY global.json Directory.Build.props Directory.Build.targets NuGet.config .editorconfig ./
COPY src/ src/
WORKDIR /repo/src/GtMotive.Estimate.Microservice.Host
RUN dotnet build "GtMotive.Estimate.Microservice.Host.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "GtMotive.Estimate.Microservice.Host.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GtMotive.Estimate.Microservice.Host.dll"]
