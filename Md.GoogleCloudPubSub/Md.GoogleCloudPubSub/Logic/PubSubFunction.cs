namespace Md.GoogleCloudPubSub.Logic
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using CloudNative.CloudEvents;
    using Google.Cloud.Functions.Framework;
    using Google.Events.Protobuf.Cloud.PubSub.V1;
    using Md.GoogleCloud.Base.Contracts.Logic;
    using Md.GoogleCloud.Base.Contracts.Messages;
    using Microsoft.Extensions.Logging;
    using Newtonsoft.Json;

    /// <summary>
    ///     Base implementation for handling pub/sub cloud events.
    /// </summary>
    /// <typeparam name="TMessage">The type of the pub/sub messages.</typeparam>
    /// <typeparam name="TIMessage">The interface that is implemented by <typeparamref name="TMessage" />.</typeparam>
    /// <typeparam name="TCategoryName">The type whose name will be the logger's category name.</typeparam>
    public abstract class PubSubFunction<TMessage, TIMessage, TCategoryName> : ICloudEventFunction<MessagePublishedData>
        where TMessage : class, TIMessage where TIMessage : IMessage
    {
        /// <summary>
        ///     An error logger.
        /// </summary>
        private readonly ILogger<TCategoryName> logger;

        /// <summary>
        ///     Handles the cloud event for messages of type <typeparamref name="TIMessage" />.
        /// </summary>
        private readonly IPubSubProvider<TIMessage> provider;

        /// <summary>
        ///     Creates a new instance of <see cref="PubSubFunction{TMessage,TIMessage,TCategoryName}" />.
        /// </summary>
        /// <param name="logger">An error logger.</param>
        /// <param name="provider">Handles the cloud event for messages of type <typeparamref name="TIMessage" />.</param>
        /// <exception cref="ArgumentNullException">
        ///     Is thrown if <paramref name="logger" /> or <paramref name="provider" /> is
        ///     null.
        /// </exception>
        protected PubSubFunction(ILogger<TCategoryName> logger, IPubSubProvider<TIMessage> provider)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.provider = provider ?? throw new ArgumentNullException(nameof(provider));
        }

        /// <summary>Asynchronously handles the specified CloudEvent.</summary>
        /// <param name="cloudEvent">The original CloudEvent extracted from the request.</param>
        /// <param name="data">The deserialized object constructed from the data.</param>
        /// <param name="cancellationToken">A cancellation token which indicates if the request is aborted.</param>
        /// <returns>
        ///     A task representing the potentially-asynchronous handling of the event.
        ///     If the task completes, the function is deemed to be successful.
        /// </returns>
        public async Task HandleAsync(
            CloudEvent cloudEvent,
            MessagePublishedData data,
            CancellationToken cancellationToken
        )
        {
            try
            {
                var json = data.Message?.TextData;
                if (string.IsNullOrWhiteSpace(json))
                {
                    throw new ArgumentException(
                        "Empty incoming json message",
                        nameof(MessagePublishedData.Message.TextData));
                }

                var message = JsonConvert.DeserializeObject<TMessage>(json);
                if (message == null)
                {
                    throw new ArgumentException(
                        $"Cannot deserialize json to ${nameof(TMessage)}: '{json}'",
                        nameof(MessagePublishedData.Message.TextData));
                }

                await this.provider.HandleAsync(message);
            }
            catch (Exception e)
            {
                this.logger.LogError(e, "Unexpected error!");
            }
        }
    }
}
