using System;

namespace GtMotive.Estimate.Microservice.Domain.Vehicles
{
    /// <summary>
    /// Raised when the data used to build a vehicle or its value objects is invalid.
    /// </summary>
    public sealed class InvalidVehicleDataException : DomainException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidVehicleDataException"/> class.
        /// </summary>
        public InvalidVehicleDataException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidVehicleDataException"/> class.
        /// </summary>
        /// <param name="message">Message.</param>
        public InvalidVehicleDataException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidVehicleDataException"/> class.
        /// </summary>
        /// <param name="message">Message.</param>
        /// <param name="innerException">Inner exception.</param>
        public InvalidVehicleDataException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
