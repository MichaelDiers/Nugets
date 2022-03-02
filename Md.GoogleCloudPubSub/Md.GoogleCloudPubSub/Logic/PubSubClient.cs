namespace Md.GoogleCloudPubSub.Logic
{
    using System;
    using System.Threading.Tasks;
    using Google.Cloud.PubSub.V1;
    using Md.GoogleCloud.Base.Contracts.Logic;
    using Newtonsoft.Json;

    /// <summary>
    ///     Access google pub/sub.
    /// </summary>
    public class PubSubClient : IPubSubClient
    {
        /// <summary>
        ///     The configuration for sending messages to pub/sub.
        /// </summary>
        private readonly IPubSubClientConfiguration configuration;

        /// <summary>
        ///     Access google cloud Pub/Sub.
        /// </summary>
        private PublisherClient? client;

        /// <summary>
        ///     Creates a new instance of <see cref="PubSubClient" />.
        /// </summary>
        /// <param name="configuration">The configuration for sending messages to pub/sub.</param>
        public PubSubClient(IPubSubClientConfiguration configuration)
        {
            this.configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        /// <summary>
        ///     Publish a message to a pub/sub topic.
        /// </summary>
        /// <typeparam name="T">The type of the message.</typeparam>
        /// <param name="message">The message that is sent to pub/sub.</param>
        /// <returns>A <see cref="Task" />.</returns>
        public async Task PublishAsync<T>(T message)
        {
            if (this.client == null)
            {
                var topic = TopicName.FromProjectTopic(this.configuration.ProjectId, this.configuration.TopicName);
                this.client = await PublisherClient.CreateAsync(topic);
            }

            var json = JsonConvert.SerializeObject(message);
            await this.client.PublishAsync(json);
        }
    }
}
