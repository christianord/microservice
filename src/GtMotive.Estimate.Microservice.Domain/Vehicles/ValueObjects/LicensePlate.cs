using System;

namespace GtMotive.Estimate.Microservice.Domain.Vehicles
{
    /// <summary>
    /// Vehicle license plate.
    /// </summary>
    public readonly struct LicensePlate : IEquatable<LicensePlate>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LicensePlate"/> struct.
        /// </summary>
        /// <param name="value">Non-empty license plate, at most 20 characters.</param>
        public LicensePlate(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new InvalidVehicleDataException("The license plate is required.");
            }

            var normalized = value.Trim().ToUpperInvariant();

            if (normalized.Length > 20)
            {
                throw new InvalidVehicleDataException("The license plate must have at most 20 characters.");
            }

            Value = normalized;
        }

        /// <summary>
        /// Gets the normalized license plate.
        /// </summary>
        public string Value { get; }

        /// <summary>
        /// Equality operator.
        /// </summary>
        /// <param name="left">Left.</param>
        /// <param name="right">Right.</param>
        /// <returns>True if equal.</returns>
        public static bool operator ==(LicensePlate left, LicensePlate right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// Inequality operator.
        /// </summary>
        /// <param name="left">Left.</param>
        /// <param name="right">Right.</param>
        /// <returns>True if different.</returns>
        public static bool operator !=(LicensePlate left, LicensePlate right)
        {
            return !left.Equals(right);
        }

        /// <inheritdoc/>
        public bool Equals(LicensePlate other) => string.Equals(Value, other.Value, StringComparison.Ordinal);

        /// <inheritdoc/>
        public override bool Equals(object obj) => obj is LicensePlate other && Equals(other);

        /// <inheritdoc/>
        public override int GetHashCode() => StringComparer.Ordinal.GetHashCode(Value ?? string.Empty);

        /// <inheritdoc/>
        public override string ToString() => Value;
    }
}
