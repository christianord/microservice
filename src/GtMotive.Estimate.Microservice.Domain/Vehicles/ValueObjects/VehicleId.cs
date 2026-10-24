using System;
using System.Diagnostics.CodeAnalysis;

namespace GtMotive.Estimate.Microservice.Domain.Vehicles
{
    /// <summary>
    /// Vehicle identifier.
    /// </summary>
    public readonly record struct VehicleId
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VehicleId"/> record struct.
        /// </summary>
        /// <param name="value">GUID version 7 value.</param>
        [SuppressMessage("Style", "SA1642:Constructor summary documentation should begin with standard text", Justification = "The analyzer does not recognize record struct constructors.")]
        public VehicleId(Guid value)
        {
            Span<byte> bytes = stackalloc byte[16];
            value.TryWriteBytes(bytes, bigEndian: true, out _);
            if ((bytes[6] & 0xF0) != 0x70 || (bytes[8] & 0xC0) != 0x80)
            {
                throw new InvalidVehicleDataException("The vehicle id must be a GUID version 7.");
            }

            Value = value;
        }

        /// <summary>
        /// Gets the identifier value.
        /// </summary>
        public Guid Value { get; }

        /// <summary>
        /// Creates a new GUID version 7 vehicle identifier.
        /// </summary>
        /// <returns>A new vehicle identifier.</returns>
        public static VehicleId New() => new(Guid.CreateVersion7());
    }
}
