namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.RegisterFleet
{
    /// <summary>
    /// Output port of the register fleet use case.
    /// </summary>
    public interface IRegisterFleetOutputPort : IOutputPortStandard<RegisterFleetOutput>
    {
        /// <summary>
        /// Informs the request data is invalid.
        /// </summary>
        /// <param name="message">Text description.</param>
        void InvalidDataHandle(string message);
    }
}
