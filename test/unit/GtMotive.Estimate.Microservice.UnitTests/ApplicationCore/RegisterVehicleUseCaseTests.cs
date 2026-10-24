using System;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.RegisterVehicle;
using GtMotive.Estimate.Microservice.Domain.Fleets;
using GtMotive.Estimate.Microservice.Domain.Interfaces;
using GtMotive.Estimate.Microservice.Domain.Vehicles;
using Moq;
using Xunit;

namespace GtMotive.Estimate.Microservice.UnitTests.ApplicationCore
{
    public sealed class RegisterVehicleUseCaseTests
    {
        private readonly Guid _fleetGuid = Guid.CreateVersion7();
        private readonly Mock<IFleetRepository> _fleets = new();
        private readonly Mock<IVehicleRepository> _vehicles = new();
        private readonly Mock<IVehicleFactory> _factory = new();
        private readonly Mock<IUnitOfWork> _unitOfWork = new();
        private readonly Mock<IRegisterVehicleOutputPort> _outputPort = new();
        private readonly RegisterVehicleUseCase _useCase;

        public RegisterVehicleUseCaseTests()
        {
            var clock = new FixedTimeProvider(new DateTimeOffset(2026, 6, 1, 0, 0, 0, TimeSpan.Zero));
            _useCase = new RegisterVehicleUseCase(
                _fleets.Object,
                _vehicles.Object,
                _factory.Object,
                _unitOfWork.Object,
                clock,
                _outputPort.Object);
        }

        [Fact]
        public async Task ValidVehicleIsRegistered()
        {
            _fleets.Setup(f => f.GetBy(It.IsAny<FleetId>())).ReturnsAsync(new Fleet(new FleetName("Test"), new AdmissionPolicy(5)));
            _factory
                .Setup(f => f.NewVehicle(It.IsAny<FleetId>(), It.IsAny<LicensePlate>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<ManufactureYear>()))
                .Returns(new Vehicle(new FleetId(_fleetGuid), new LicensePlate("ABC"), "Seat", "Ibiza", new ManufactureYear(2022)));

            await _useCase.Execute(new RegisterVehicleInput(_fleetGuid, "abc", "Seat", "Ibiza", 2022));

            _vehicles.Verify(v => v.Add(It.IsAny<IVehicle>()), Times.Once);
            _unitOfWork.Verify(u => u.Save(), Times.Once);
            _outputPort.Verify(o => o.StandardHandle(It.Is<RegisterVehicleOutput>(x => x.LicensePlate == "ABC" && x.Status == "Available")), Times.Once);
        }

        [Fact]
        public async Task TooOldVehicleIsRejected()
        {
            _fleets.Setup(f => f.GetBy(It.IsAny<FleetId>())).ReturnsAsync(new Fleet(new FleetName("Test"), new AdmissionPolicy(5)));

            await _useCase.Execute(new RegisterVehicleInput(_fleetGuid, "ABC", "Seat", "Ibiza", 2020));

            _outputPort.Verify(o => o.VehicleTooOldHandle(It.IsAny<string>()), Times.Once);
            _unitOfWork.Verify(u => u.Save(), Times.Never);
        }

        [Fact]
        public async Task FutureYearIsRejected()
        {
            _fleets.Setup(f => f.GetBy(It.IsAny<FleetId>())).ReturnsAsync(new Fleet(new FleetName("Test"), new AdmissionPolicy(5)));

            await _useCase.Execute(new RegisterVehicleInput(_fleetGuid, "ABC", "Seat", "Ibiza", 2027));

            _outputPort.Verify(o => o.VehicleTooOldHandle(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async Task UnknownFleetIsReported()
        {
            _fleets.Setup(f => f.GetBy(It.IsAny<FleetId>())).ThrowsAsync(new FleetNotFoundException("missing"));

            await _useCase.Execute(new RegisterVehicleInput(_fleetGuid, "ABC", "Seat", "Ibiza", 2024));

            _outputPort.Verify(o => o.FleetNotFoundHandle("missing"), Times.Once);
            _unitOfWork.Verify(u => u.Save(), Times.Never);
        }

        [Fact]
        public async Task DuplicatePlateIsReported()
        {
            _fleets.Setup(f => f.GetBy(It.IsAny<FleetId>())).ReturnsAsync(new Fleet(new FleetName("Test"), new AdmissionPolicy(5)));
            _vehicles.Setup(v => v.Exists(It.IsAny<LicensePlate>())).ReturnsAsync(true);

            await _useCase.Execute(new RegisterVehicleInput(_fleetGuid, "ABC", "Seat", "Ibiza", 2024));

            _outputPort.Verify(o => o.AlreadyExistsHandle(It.IsAny<string>()), Times.Once);
            _unitOfWork.Verify(u => u.Save(), Times.Never);
        }

        [Fact]
        public async Task InvalidDataIsReported()
        {
            await _useCase.Execute(new RegisterVehicleInput(_fleetGuid, " ", "Seat", "Ibiza", 2024));

            _outputPort.Verify(o => o.InvalidDataHandle(It.IsAny<string>()), Times.Once);
            _fleets.Verify(f => f.GetBy(It.IsAny<FleetId>()), Times.Never);
        }

        private sealed class FixedTimeProvider(DateTimeOffset now) : TimeProvider
        {
            public override DateTimeOffset GetUtcNow() => now;
        }
    }
}
