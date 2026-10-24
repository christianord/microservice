namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.ReturnVehicle
{
    /// <summary>
    /// Output port of the return vehicle use case.
    /// </summary>
    public interface IReturnVehicleOutputPort : IOutputPortStandard<ReturnVehicleOutput>
    {
        /// <summary>
        /// Informs the vehicle was not found.
        /// </summary>
        /// <param name="message">Text description.</param>
        void VehicleNotFoundHandle(string message);

        /// <summary>
        /// Informs the vehicle is not rented.
        /// </summary>
        /// <param name="message">Text description.</param>
        void NotRentedHandle(string message);

        /// <summary>
        /// Informs the customer is not the renter.
        /// </summary>
        /// <param name="message">Text description.</param>
        void NotRenterHandle(string message);

        /// <summary>
        /// Informs the request data is invalid.
        /// </summary>
        /// <param name="message">Text description.</param>
        void InvalidDataHandle(string message);
    }
}
