namespace Md.GoogleCloudPubSub.Tests.Model
{
    using Md.Common.Contracts.Model;
    using Md.GoogleCloudPubSub.Contracts.Model;
    using Md.GoogleCloudPubSub.Model;
    using Xunit;

    public class PubSubClientEnvironmentTests
    {
        [Fact]
        public void Ctor()
        {
            var value = new PubSubClientEnvironment();
            Assert.Equal(string.Empty, value.ProjectId);
            Assert.Equal(Environment.None, value.Environment);
            Assert.Equal(string.Empty, value.TopicName);
        }

        [Theory]
        [InlineData(Environment.Test, "projectId", "topicName")]
        public void CtorWithArguments(Environment environment, string projectId, string topicName)
        {
            var value = new PubSubClientEnvironment(environment, projectId, topicName);

            Assert.Equal(projectId, value.ProjectId);
            Assert.Equal(environment, value.Environment);
            Assert.Equal(topicName, value.TopicName);

            var iValue = (IPubSubClientEnvironment) value;

            Assert.Equal(projectId, iValue.ProjectId);
            Assert.Equal(environment, iValue.Environment);
            Assert.Equal(topicName, iValue.TopicName);
        }
    }
}
