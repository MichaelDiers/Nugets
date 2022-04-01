namespace Md.GoogleCloudPubSub.Logic
{
    using Md.Common.Contracts;

    /// <summary>
    ///     Specifies the pub/sub client environment.
    /// </summary>
    public class PubSubClientEnvironment : IPubSubClientEnvironment
    {
        /// <summary>
        ///     Creates a new instance of <see cref="PubSubClientEnvironment" />.
        /// </summary>
        /// <param name="environment">The runtime environment.</param>
        /// <param name="projectId">The id of the project.</param>
        /// <param name="topicName">The name of the pub/sub topic.</param>
        public PubSubClientEnvironment(Environment environment, string projectId, string topicName)
        {
            this.Environment = environment;
            this.ProjectId = projectId;
            this.TopicName = topicName;
        }

        /// <summary>
        ///     Gets the runtime environment.
        /// </summary>
        public Environment Environment { get; }

        /// <summary>
        ///     Gets the project id.
        /// </summary>
        public string ProjectId { get; }

        /// <summary>
        ///     Gets the name of the topic.
        /// </summary>
        public string TopicName { get; }
    }
}
