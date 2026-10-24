using System;

namespace GtMotive.Estimate.Microservice.Domain.Vehicles
{
    /// <summary>
    /// Raised when a vehicle does not exist.
    /// </summary>
    public sealed class VehicleNotFoundException : DomainException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VehicleNotFoundException"/> class.
        /// </summary>
        public VehicleNotFoundException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VehicleNotFoundException"/> class.
        /// </summary>
        /// <param name="message">Message.</param>
        public VehicleNotFoundException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VehicleNotFoundException"/> class.
        /// </summary>
        /// <param name="message">Message.</param>
        /// <param name="innerException">Inner exception.</param>
        public VehicleNotFoundException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
