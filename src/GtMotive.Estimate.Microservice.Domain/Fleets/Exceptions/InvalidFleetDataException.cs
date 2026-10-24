using System;

namespace GtMotive.Estimate.Microservice.Domain.Fleets
{
    /// <summary>
    /// Raised when the data used to build a fleet or its value objects is invalid.
    /// </summary>
    public sealed class InvalidFleetDataException : DomainException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidFleetDataException"/> class.
        /// </summary>
        public InvalidFleetDataException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidFleetDataException"/> class.
        /// </summary>
        /// <param name="message">Message.</param>
        public InvalidFleetDataException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidFleetDataException"/> class.
        /// </summary>
        /// <param name="message">Message.</param>
        /// <param name="innerException">Inner exception.</param>
        public InvalidFleetDataException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
