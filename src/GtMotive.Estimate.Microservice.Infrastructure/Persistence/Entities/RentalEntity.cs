using System;
using System.Diagnostics.CodeAnalysis;
using GtMotive.Estimate.Microservice.Domain.Rentals;
using GtMotive.Estimate.Microservice.Domain.Vehicles;

namespace GtMotive.Estimate.Microservice.Infrastructure.Persistence.Entities
{
    internal sealed class RentalEntity : IRental
    {
        public RentalEntity(VehicleId vehicleId, Guid customerId, DateTime rentalDate)
        {
            Id = RentalId.New();
            VehicleId = vehicleId;
            CustomerId = customerId;
            RentalDate = rentalDate;
        }

        private RentalEntity()
        {
        }

        public RentalId Id { get; private set; }

        public VehicleId VehicleId { get; private set; }

        public Guid CustomerId { get; private set; }

        public DateTime RentalDate { get; private set; }

        public DateTime? ReturnDate { get; private set; }

        public int? RentalDays { get; private set; }

        [SuppressMessage("Performance", "CA1819:Properties should not return arrays", Justification = "Concurrency token mapped to a rowversion column.")]
        public byte[] RowVersion { get; private set; }

        public void Close(DateTime returnDate)
        {
            var closure = RentalStateTransitions.Close(RentalDate, ReturnDate, returnDate);
            ReturnDate = closure.ReturnDate;
            RentalDays = closure.RentalDays;
        }
    }
}
