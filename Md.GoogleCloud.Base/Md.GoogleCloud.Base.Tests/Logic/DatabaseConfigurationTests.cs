namespace Md.GoogleCloud.Base.Tests.Logic
{
    using System;
    using Md.GoogleCloud.Base.Contracts.Logic;
    using Md.GoogleCloud.Base.Logic;
    using Xunit;

    public class DatabaseConfigurationTests
    {
        [Fact]
        public void Ctor()
        {
            const string projectId = nameof(projectId);
            const string collectionName = nameof(collectionName);
            var configuration = new DatabaseConfiguration(projectId, collectionName);
            Assert.Equal(projectId, configuration.ProjectId);
            Assert.Equal(collectionName, configuration.CollectionName);

            var iConfiguration = configuration as IDatabaseConfiguration;
            Assert.NotNull(iConfiguration);
            Assert.Equal(projectId, iConfiguration.ProjectId);
            Assert.Equal(collectionName, iConfiguration.CollectionName);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void CtorThrowsArgumentExceptionForInvalidCollectionName(string collectionName)
        {
            Assert.Throws<ArgumentException>(() => new DatabaseConfiguration("projectId", collectionName));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void CtorThrowsArgumentExceptionForInvalidProjectId(string projectId)
        {
            Assert.Throws<ArgumentException>(() => new DatabaseConfiguration(projectId, "collectionName"));
        }

        [Fact]
        public void ImplementsIDatabaseConfiguration()
        {
            Assert.IsAssignableFrom<IDatabaseConfiguration>(new DatabaseConfiguration("project", "collection"));
        }
    }
}
