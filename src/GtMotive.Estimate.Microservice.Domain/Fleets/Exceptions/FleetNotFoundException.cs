using System;

namespace GtMotive.Estimate.Microservice.Domain.Fleets
{
    /// <summary>
    /// Raised when a fleet does not exist.
    /// </summary>
    public sealed class FleetNotFoundException : DomainException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FleetNotFoundException"/> class.
        /// </summary>
        public FleetNotFoundException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FleetNotFoundException"/> class.
        /// </summary>
        /// <param name="message">Message.</param>
        public FleetNotFoundException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FleetNotFoundException"/> class.
        /// </summary>
        /// <param name="message">Message.</param>
        /// <param name="innerException">Inner exception.</param>
        public FleetNotFoundException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
