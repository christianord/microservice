namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.RentVehicle
{
    /// <summary>
    /// Output port of the rent vehicle use case.
    /// </summary>
    public interface IRentVehicleOutputPort : IOutputPortStandard<RentVehicleOutput>
    {
        /// <summary>
        /// Informs the vehicle was not found.
        /// </summary>
        /// <param name="message">Text description.</param>
        void VehicleNotFoundHandle(string message);

        /// <summary>
        /// Informs the vehicle is not available.
        /// </summary>
        /// <param name="message">Text description.</param>
        void VehicleNotAvailableHandle(string message);

        /// <summary>
        /// Informs the customer already has an active rental.
        /// </summary>
        /// <param name="message">Text description.</param>
        void CustomerAlreadyHasRentalHandle(string message);

        /// <summary>
        /// Informs the vehicle no longer satisfies the fleet admission policy.
        /// </summary>
        /// <param name="message">Text description.</param>
        void VehicleTooOldHandle(string message);

        /// <summary>
        /// Informs the request data is invalid.
        /// </summary>
        /// <param name="message">Text description.</param>
        void InvalidDataHandle(string message);
    }
}
