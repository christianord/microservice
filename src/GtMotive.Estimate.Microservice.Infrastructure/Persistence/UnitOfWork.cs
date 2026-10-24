using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.ApplicationCore;
using GtMotive.Estimate.Microservice.Domain.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace GtMotive.Estimate.Microservice.Infrastructure.Persistence
{
    internal sealed class UnitOfWork(RentCarContext context) : IUnitOfWork
    {
        private const int SqlDuplicateKey = 2601;
        private const int SqlUniqueConstraint = 2627;

        public async Task<int> Save()
        {
            try
            {
                return await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new ConcurrencyConflictException("The data was modified by another request.", ex);
            }
            catch (DbUpdateException ex) when (ex.InnerException is SqlException { Number: SqlDuplicateKey or SqlUniqueConstraint })
            {
                throw new ConcurrencyConflictException("A unique constraint was violated.", ex);
            }
        }
    }
}
