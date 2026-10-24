namespace GtMotive.Estimate.Microservice.Domain.Fleets
{
    /// <summary>
    /// Fleet name.
    /// </summary>
    public readonly struct FleetName
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FleetName"/> struct.
        /// </summary>
        /// <param name="value">Non-empty name.</param>
        public FleetName(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new InvalidFleetDataException("The fleet name is required.");
            }

            Value = value.Trim();
        }

        /// <summary>
        /// Gets the name.
        /// </summary>
        public string Value { get; }

        /// <inheritdoc/>
        public override string ToString() => Value;
    }
}
