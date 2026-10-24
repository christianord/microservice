using System;

namespace GtMotive.Estimate.Microservice.Domain.Rentals
{
    /// <summary>
    /// Raised when data used to build a rental or its value objects is invalid.
    /// </summary>
    public sealed class InvalidRentalDataException : DomainException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidRentalDataException"/> class.
        /// </summary>
        public InvalidRentalDataException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidRentalDataException"/> class.
        /// </summary>
        /// <param name="message">Message.</param>
        public InvalidRentalDataException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidRentalDataException"/> class.
        /// </summary>
        /// <param name="message">Message.</param>
        /// <param name="innerException">Inner exception.</param>
        public InvalidRentalDataException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
