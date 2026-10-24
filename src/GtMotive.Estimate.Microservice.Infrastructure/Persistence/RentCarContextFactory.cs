using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace GtMotive.Estimate.Microservice.Infrastructure.Persistence
{
    internal sealed class RentCarContextFactory : IDesignTimeDbContextFactory<RentCarContext>
    {
        private const string DefaultConnection =
            "Server=localhost,11433;Database=rentcar;User Id=sa;Password=SqlServer_Dev_123!;TrustServerCertificate=True";

        public RentCarContext CreateDbContext(string[] args)
        {
            var connectionString = Environment.GetEnvironmentVariable("ConnectionStrings__DefaultConnection") ?? DefaultConnection;
            var options = new DbContextOptionsBuilder<RentCarContext>()
                .UseSqlServer(connectionString)
                .Options;

            return new RentCarContext(options);
        }
    }
}
