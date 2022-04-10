namespace Md.GoogleCloudPubSub.Contracts.Model
{
    using Md.Common.Contracts.Model;

    /// <summary>
    ///     Specifies the pub/sub client environment.
    /// </summary>
    public interface IPubSubClientEnvironment
    {
        /// <summary>
        ///     Gets the runtime environment.
        /// </summary>
        Environment Environment { get; }

        /// <summary>
        ///     Gets the project id.
        /// </summary>
        string ProjectId { get; }

        /// <summary>
        ///     Gets the name of the topic.
        /// </summary>
        string TopicName { get; }
    }
}
