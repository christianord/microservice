namespace GtMotive.Estimate.Microservice.Domain.Fleets
{
    /// <summary>
    /// Creates fleets.
    /// </summary>
    public interface IFleetFactory
    {
        /// <summary>
        /// Creates a new fleet.
        /// </summary>
        /// <param name="name">Fleet name.</param>
        /// <param name="admissionPolicy">Admission policy.</param>
        /// <returns>The new fleet.</returns>
        IFleet NewFleet(FleetName name, AdmissionPolicy admissionPolicy);
    }
}
