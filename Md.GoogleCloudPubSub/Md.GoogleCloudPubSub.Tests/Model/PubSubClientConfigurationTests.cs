namespace Md.GoogleCloudPubSub.Tests.Model
{
    using System;
    using Md.GoogleCloudPubSub.Contracts.Model;
    using Md.GoogleCloudPubSub.Model;
    using Xunit;

    public class PubSubClientConfigurationTests
    {
        [Fact]
        public void Ctor()
        {
            const string projectId = nameof(projectId);
            const string topicName = nameof(topicName);
            var configuration = new PubSubClientConfiguration(projectId, topicName);
            Assert.Equal(projectId, configuration.ProjectId);
            Assert.Equal(topicName, configuration.TopicName);

            var iConfiguration = (IPubSubClientConfiguration) configuration;
            Assert.NotNull(iConfiguration);
            Assert.Equal(projectId, iConfiguration.ProjectId);
            Assert.Equal(topicName, iConfiguration.TopicName);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void CtorThrowsArgumentExceptionForInvalidProjectId(string projectId)
        {
            Assert.Throws<ArgumentException>(() => new PubSubClientConfiguration(projectId, "topicName"));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void CtorThrowsArgumentExceptionForInvalidTopicName(string topicName)
        {
            Assert.Throws<ArgumentException>(() => new PubSubClientConfiguration("projectId", topicName));
        }

        [Fact]
        public void ImplementsIPubSubClientConfiguration()
        {
            Assert.IsAssignableFrom<IPubSubClientConfiguration>(new PubSubClientConfiguration("project", "topicName"));
        }
    }
}
