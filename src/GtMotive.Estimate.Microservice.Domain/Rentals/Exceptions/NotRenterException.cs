using System;

namespace GtMotive.Estimate.Microservice.Domain.Rentals
{
    /// <summary>
    /// Raised when a customer tries to return a vehicle rented by someone else.
    /// </summary>
    public sealed class NotRenterException : DomainException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NotRenterException"/> class.
        /// </summary>
        public NotRenterException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NotRenterException"/> class.
        /// </summary>
        /// <param name="message">Message.</param>
        public NotRenterException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NotRenterException"/> class.
        /// </summary>
        /// <param name="message">Message.</param>
        /// <param name="innerException">Inner exception.</param>
        public NotRenterException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
