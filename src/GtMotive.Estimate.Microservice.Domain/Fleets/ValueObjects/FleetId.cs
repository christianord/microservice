using System;
using System.Diagnostics.CodeAnalysis;

namespace GtMotive.Estimate.Microservice.Domain.Fleets
{
    /// <summary>
    /// Fleet identifier.
    /// </summary>
    public readonly record struct FleetId
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FleetId"/> record struct.
        /// </summary>
        /// <param name="value">GUID version 7 value.</param>
        [SuppressMessage("Style", "SA1642:Constructor summary documentation should begin with standard text", Justification = "The analyzer does not recognize record struct constructors.")]
        public FleetId(Guid value)
        {
            Span<byte> bytes = stackalloc byte[16];
            value.TryWriteBytes(bytes, bigEndian: true, out _);
            if ((bytes[6] & 0xF0) != 0x70 || (bytes[8] & 0xC0) != 0x80)
            {
                throw new InvalidFleetDataException("The fleet id must be a GUID version 7.");
            }

            Value = value;
        }

        /// <summary>
        /// Gets the identifier value.
        /// </summary>
        public Guid Value { get; }

        /// <summary>
        /// Creates a new GUID version 7 fleet identifier.
        /// </summary>
        /// <returns>A new fleet identifier.</returns>
        public static FleetId New() => new(Guid.CreateVersion7());
    }
}
