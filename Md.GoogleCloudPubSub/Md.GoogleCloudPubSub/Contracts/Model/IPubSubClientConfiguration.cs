namespace Md.GoogleCloudPubSub.Contracts.Model
{
    /// <summary>
    ///     The configuration for sending messages to pub/sub.
    /// </summary>
    public interface IPubSubClientConfiguration
    {
        /// <summary>
        ///     Gets the id of the google cloud project.
        /// </summary>
        string ProjectId { get; }

        /// <summary>
        ///     Gets the name of the pub/sub topic.
        /// </summary>
        string TopicName { get; }
    }
}
