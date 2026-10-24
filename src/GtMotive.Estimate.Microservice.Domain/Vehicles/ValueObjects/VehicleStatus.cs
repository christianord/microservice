namespace GtMotive.Estimate.Microservice.Domain.Vehicles
{
    /// <summary>
    /// Vehicle rental status.
    /// </summary>
    public enum VehicleStatus
    {
        /// <summary>
        /// The vehicle can be rented.
        /// </summary>
        Available = 0,

        /// <summary>
        /// The vehicle is currently rented.
        /// </summary>
        Rented = 1,
    }
}
