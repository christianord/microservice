using System;

namespace GtMotive.Estimate.Microservice.Domain.Vehicles
{
    /// <summary>
    /// Raised when a vehicle is older than the fleet admission policy allows.
    /// </summary>
    public sealed class VehicleTooOldException : DomainException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VehicleTooOldException"/> class.
        /// </summary>
        public VehicleTooOldException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VehicleTooOldException"/> class.
        /// </summary>
        /// <param name="message">Message.</param>
        public VehicleTooOldException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VehicleTooOldException"/> class.
        /// </summary>
        /// <param name="message">Message.</param>
        /// <param name="innerException">Inner exception.</param>
        public VehicleTooOldException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
