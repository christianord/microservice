namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.RegisterVehicle
{
    /// <summary>
    /// Output port of the register vehicle use case.
    /// </summary>
    public interface IRegisterVehicleOutputPort : IOutputPortStandard<RegisterVehicleOutput>
    {
        /// <summary>
        /// Informs the fleet was not found.
        /// </summary>
        /// <param name="message">Text description.</param>
        void FleetNotFoundHandle(string message);

        /// <summary>
        /// Informs the vehicle is too old for the fleet admission policy.
        /// </summary>
        /// <param name="message">Text description.</param>
        void VehicleTooOldHandle(string message);

        /// <summary>
        /// Informs the vehicle is already registered.
        /// </summary>
        /// <param name="message">Text description.</param>
        void AlreadyExistsHandle(string message);

        /// <summary>
        /// Informs the request data is invalid.
        /// </summary>
        /// <param name="message">Text description.</param>
        void InvalidDataHandle(string message);
    }
}
