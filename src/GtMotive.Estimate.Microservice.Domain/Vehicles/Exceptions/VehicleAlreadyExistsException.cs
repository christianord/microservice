using System;

namespace GtMotive.Estimate.Microservice.Domain.Vehicles
{
    /// <summary>
    /// Raised when a vehicle with the same license plate is already registered.
    /// </summary>
    public sealed class VehicleAlreadyExistsException : DomainException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VehicleAlreadyExistsException"/> class.
        /// </summary>
        public VehicleAlreadyExistsException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VehicleAlreadyExistsException"/> class.
        /// </summary>
        /// <param name="message">Message.</param>
        public VehicleAlreadyExistsException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VehicleAlreadyExistsException"/> class.
        /// </summary>
        /// <param name="message">Message.</param>
        /// <param name="innerException">Inner exception.</param>
        public VehicleAlreadyExistsException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
