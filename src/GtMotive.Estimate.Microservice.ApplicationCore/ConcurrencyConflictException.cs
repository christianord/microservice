using System;

namespace GtMotive.Estimate.Microservice.ApplicationCore
{
    /// <summary>
    /// System exception raised when a concurrency or unique constraint conflict happens while saving.
    /// </summary>
    public sealed class ConcurrencyConflictException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConcurrencyConflictException"/> class.
        /// </summary>
        public ConcurrencyConflictException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConcurrencyConflictException"/> class.
        /// </summary>
        /// <param name="message">Message.</param>
        public ConcurrencyConflictException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConcurrencyConflictException"/> class.
        /// </summary>
        /// <param name="message">Message.</param>
        /// <param name="innerException">Inner exception.</param>
        public ConcurrencyConflictException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
