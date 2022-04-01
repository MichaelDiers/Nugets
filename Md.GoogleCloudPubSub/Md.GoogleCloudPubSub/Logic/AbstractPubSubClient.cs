namespace Md.GoogleCloudPubSub.Logic
{
    using System.Threading.Tasks;
    using Md.GoogleCloud.Base.Contracts.Logic;
    using Md.GoogleCloud.Base.Contracts.Messages;
    using Md.GoogleCloud.Base.Logic;

    /// <summary>
    ///     Base pub/sub client.
    /// </summary>
    public abstract class AbstractPubSubClient<T> where T : IMessage
    {
        /// <summary>
        ///     Client for google cloud pub/sub.
        /// </summary>
        private readonly IPubSubClient pubSubClient;

        /// <summary>
        ///     Creates a new instance of <see cref="AbstractPubSubClient{T}" />.
        /// </summary>
        /// <param name="environment">The runtime environment of the client.</param>
        protected AbstractPubSubClient(IPubSubClientEnvironment environment)

        {
            this.pubSubClient = new PubSubClient(
                new PubSubClientConfiguration(
                    environment.ProjectId,
                    $"{environment.TopicName}_{environment.Environment.ToString().ToUpper()}"));
        }

        /// <summary>
        ///     Publish a message to google pub/pub.
        /// </summary>
        /// <param name="message">The message to publish.</param>
        /// <returns>A <see cref="Task" /> that indicates completion.</returns>
        // ReSharper disable once UnusedMember.Global
        public async Task PublishAsync(T message)
        {
            await this.pubSubClient.PublishAsync(message);
        }
    }
}
