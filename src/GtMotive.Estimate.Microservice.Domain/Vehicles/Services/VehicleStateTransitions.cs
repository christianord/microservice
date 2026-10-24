namespace GtMotive.Estimate.Microservice.Domain.Vehicles
{
    /// <summary>
    /// Applies vehicle state transition rules.
    /// </summary>
    public static class VehicleStateTransitions
    {
        /// <summary>
        /// Transitions an available vehicle to rented.
        /// </summary>
        /// <param name="status">Current vehicle status.</param>
        /// <param name="licensePlate">Vehicle license plate.</param>
        /// <returns>The rented status.</returns>
        public static VehicleStatus Rent(VehicleStatus status, LicensePlate licensePlate)
        {
            if (status != VehicleStatus.Available)
            {
                throw new VehicleNotAvailableException($"The vehicle {licensePlate} is not available.");
            }

            return VehicleStatus.Rented;
        }

        /// <summary>
        /// Transitions a rented vehicle to available.
        /// </summary>
        /// <param name="status">Current vehicle status.</param>
        /// <param name="licensePlate">Vehicle license plate.</param>
        /// <returns>The available status.</returns>
        public static VehicleStatus MarkAvailable(VehicleStatus status, LicensePlate licensePlate)
        {
            if (status != VehicleStatus.Rented)
            {
                throw new VehicleNotRentedException($"The vehicle {licensePlate} is not rented.");
            }

            return VehicleStatus.Available;
        }
    }
}
