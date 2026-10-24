using System;
using GtMotive.Estimate.Microservice.Domain.Fleets;
using GtMotive.Estimate.Microservice.Domain.Rentals;
using GtMotive.Estimate.Microservice.Domain.Vehicles;
using Xunit;

namespace GtMotive.Estimate.Microservice.UnitTests.Domain
{
    public sealed class VehicleValueObjectsTests
    {
        [Fact]
        public void LicensePlateIsNormalized()
        {
            Assert.Equal("ABC-123", new LicensePlate("  abc-123 ").Value);
            Assert.Equal(new LicensePlate("abc"), new LicensePlate("ABC"));
        }

        [Theory]
        [InlineData("")]
        [InlineData("   ")]
        [InlineData(null)]
        public void LicensePlateRejectsEmpty(string value)
        {
            Assert.Throws<InvalidVehicleDataException>(() => new LicensePlate(value));
        }

        [Fact]
        public void LicensePlateRejectsTooLong()
        {
            Assert.Throws<InvalidVehicleDataException>(() => new LicensePlate(new string('A', 21)));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1899)]
        public void ManufactureYearRejectsInvalid(int year)
        {
            Assert.Throws<InvalidVehicleDataException>(() => new ManufactureYear(year));
        }

        [Fact]
        public void EmptyIdsAreRejected()
        {
            Assert.Throws<InvalidVehicleDataException>(() => new VehicleId(Guid.Empty));
            Assert.Throws<InvalidFleetDataException>(() => new FleetId(Guid.Empty));
        }

        [Fact]
        public void IdsAcceptOnlyGuidVersion7()
        {
            var fleetId = Guid.CreateVersion7();
            var vehicleId = Guid.CreateVersion7();
            var rentalId = Guid.CreateVersion7();

            Assert.Equal(fleetId, new FleetId(fleetId).Value);
            Assert.Equal(vehicleId, new VehicleId(vehicleId).Value);
            Assert.Equal(rentalId, new RentalId(rentalId).Value);

            Assert.Throws<InvalidFleetDataException>(() => new FleetId(Guid.NewGuid()));
            Assert.Throws<InvalidVehicleDataException>(() => new VehicleId(Guid.NewGuid()));
            Assert.Throws<InvalidRentalDataException>(() => new RentalId(Guid.NewGuid()));
        }
    }
}
