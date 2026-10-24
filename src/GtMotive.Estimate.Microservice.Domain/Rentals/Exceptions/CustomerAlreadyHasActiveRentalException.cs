using System;

namespace GtMotive.Estimate.Microservice.Domain.Rentals
{
    /// <summary>
    /// Raised when a customer tries to rent a second vehicle (BR-1).
    /// </summary>
    public sealed class CustomerAlreadyHasActiveRentalException : DomainException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerAlreadyHasActiveRentalException"/> class.
        /// </summary>
        public CustomerAlreadyHasActiveRentalException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerAlreadyHasActiveRentalException"/> class.
        /// </summary>
        /// <param name="message">Message.</param>
        public CustomerAlreadyHasActiveRentalException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerAlreadyHasActiveRentalException"/> class.
        /// </summary>
        /// <param name="message">Message.</param>
        /// <param name="innerException">Inner exception.</param>
        public CustomerAlreadyHasActiveRentalException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
