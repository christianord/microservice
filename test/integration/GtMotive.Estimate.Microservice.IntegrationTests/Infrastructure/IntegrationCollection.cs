using Xunit;

namespace GtMotive.Estimate.Microservice.IntegrationTests.Infrastructure
{
    [CollectionDefinition(Name)]
    public sealed class IntegrationCollection : ICollectionFixture<IntegrationFixture>
    {
        public const string Name = "Integration";
    }
}
