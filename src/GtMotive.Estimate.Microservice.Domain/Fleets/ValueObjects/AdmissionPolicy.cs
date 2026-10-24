using System;
using System.Linq.Expressions;

namespace GtMotive.Estimate.Microservice.Domain.Fleets
{
    /// <summary>
    /// Rules a vehicle must satisfy to be admitted in a fleet.
    /// </summary>
    public readonly struct AdmissionPolicy
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AdmissionPolicy"/> struct.
        /// </summary>
        /// <param name="maxVehicleAgeYears">Maximum vehicle age in years (greater than zero).</param>
        public AdmissionPolicy(int maxVehicleAgeYears)
        {
            if (maxVehicleAgeYears <= 0)
            {
                throw new InvalidFleetDataException("The maximum vehicle age must be greater than zero.");
            }

            MaxVehicleAgeYears = maxVehicleAgeYears;
        }

        /// <summary>
        /// Gets the maximum vehicle age in years.
        /// </summary>
        public int MaxVehicleAgeYears { get; }

        /// <summary>
        /// Builds the admission predicate from a manufacture year selector.
        /// </summary>
        /// <typeparam name="T">Type that exposes the manufacture year.</typeparam>
        /// <param name="yearSelector">Selector of the manufacture year.</param>
        /// <param name="currentYear">Current year.</param>
        /// <returns>A predicate expression translatable by query providers.</returns>
        public Expression<Func<T, bool>> ToExpression<T>(Expression<Func<T, int>> yearSelector, int currentYear)
        {
            ArgumentNullException.ThrowIfNull(yearSelector);

            var min = Expression.Constant(currentYear - MaxVehicleAgeYears);
            var max = Expression.Constant(currentYear);
            var body = Expression.AndAlso(
                Expression.GreaterThanOrEqual(yearSelector.Body, min),
                Expression.LessThanOrEqual(yearSelector.Body, max));

            return Expression.Lambda<Func<T, bool>>(body, yearSelector.Parameters);
        }

        /// <summary>
        /// Checks whether a manufacture year is admitted.
        /// </summary>
        /// <param name="manufactureYear">Manufacture year.</param>
        /// <param name="currentYear">Current year.</param>
        /// <returns>True if admitted.</returns>
        public bool IsAdmitted(int manufactureYear, int currentYear) =>
            ToExpression<int>(y => y, currentYear).Compile()(manufactureYear);
    }
}
