namespace Md.GoogleCloudPubSub.Model
{
    using System.ComponentModel.DataAnnotations;
    using Md.Common.Contracts.Model;
    using Md.Common.DataAnnotations;
    using Md.GoogleCloudPubSub.Contracts.Model;

    /// <summary>
    ///     Specifies the pub/sub client environment.
    /// </summary>
    public class PubSubClientEnvironment : IPubSubClientEnvironment
    {
        /// <summary>
        ///     Creates a new instance of <see cref="PubSubClientEnvironment" />.
        /// </summary>
        public PubSubClientEnvironment()
            : this(Environment.None, string.Empty, string.Empty)
        {
        }

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
        [Required]
        [NonZeroAndDefinedEnum(typeof(Environment))]
        public Environment Environment { get; set; }

        /// <summary>
        ///     Gets the project id.
        /// </summary>
        [Required]
        [ProjectId]
        public string ProjectId { get; set; }

        /// <summary>
        ///     Gets the name of the topic.
        /// </summary>
        [Required]
        [TopicName]
        public string TopicName { get; set; }
    }
}
