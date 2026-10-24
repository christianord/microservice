using System;
using GtMotive.Estimate.Microservice.Domain.Vehicles;

namespace GtMotive.Estimate.Microservice.Domain.Rentals
{
    /// <summary>
    /// Rental aggregate root.
    /// </summary>
    public sealed class Rental : IRental
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Rental"/> class.
        /// </summary>
        /// <param name="vehicleId">Vehicle identifier.</param>
        /// <param name="customerId">Customer identifier.</param>
        /// <param name="rentalDate">Rental date (UTC).</param>
        public Rental(VehicleId vehicleId, Guid customerId, DateTime rentalDate)
            : this(RentalId.New(), vehicleId, customerId, rentalDate, null, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Rental"/> class with persisted values.
        /// </summary>
        /// <param name="id">Rental identifier.</param>
        /// <param name="vehicleId">Vehicle identifier.</param>
        /// <param name="customerId">Customer identifier.</param>
        /// <param name="rentalDate">Rental date (UTC).</param>
        /// <param name="returnDate">Return date (UTC), if closed.</param>
        /// <param name="rentalDays">Rental days, if closed.</param>
        public Rental(RentalId id, VehicleId vehicleId, Guid customerId, DateTime rentalDate, DateTime? returnDate, int? rentalDays)
        {
            Id = id;
            VehicleId = vehicleId;
            CustomerId = customerId;
            RentalDate = rentalDate;
            ReturnDate = returnDate;
            RentalDays = rentalDays;
        }

        /// <inheritdoc/>
        public RentalId Id { get; }

        /// <inheritdoc/>
        public VehicleId VehicleId { get; }

        /// <inheritdoc/>
        public Guid CustomerId { get; }

        /// <inheritdoc/>
        public DateTime RentalDate { get; }

        /// <inheritdoc/>
        public DateTime? ReturnDate { get; private set; }

        /// <inheritdoc/>
        public int? RentalDays { get; private set; }

        /// <summary>
        /// Closes this rental and calculates its duration in days.
        /// </summary>
        /// <param name="returnDate">Return date (UTC).</param>
        public void Close(DateTime returnDate)
        {
            var closure = RentalStateTransitions.Close(RentalDate, ReturnDate, returnDate);
            ReturnDate = closure.ReturnDate;
            RentalDays = closure.RentalDays;
        }
    }
}
