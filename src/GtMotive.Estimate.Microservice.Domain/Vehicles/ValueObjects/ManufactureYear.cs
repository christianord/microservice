namespace GtMotive.Estimate.Microservice.Domain.Vehicles
{
    /// <summary>
    /// Vehicle manufacture year.
    /// </summary>
    public readonly struct ManufactureYear
    {
        private const int MinYear = 1900;

        /// <summary>
        /// Initializes a new instance of the <see cref="ManufactureYear"/> struct.
        /// </summary>
        /// <param name="value">Year, 1900 or later.</param>
        public ManufactureYear(int value)
        {
            if (value < MinYear)
            {
                throw new InvalidVehicleDataException($"The manufacture year must be {MinYear} or later.");
            }

            Value = value;
        }

        /// <summary>
        /// Gets the year.
        /// </summary>
        public int Value { get; }

        /// <inheritdoc/>
        public override string ToString() => Value.ToString(System.Globalization.CultureInfo.InvariantCulture);
    }
}
