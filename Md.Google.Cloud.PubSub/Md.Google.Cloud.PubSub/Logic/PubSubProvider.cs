namespace Md.GoogleCloudPubSub.Logic
{
    using System;
    using System.Threading.Tasks;
    using Md.GoogleCloudPubSub.Contracts.Logic;
    using Md.GoogleCloudPubSub.Contracts.Messages;
    using Microsoft.Extensions.Logging;

    /// <summary>
    ///     Handles the pub/sub messages of type <typeparamref name="TMessage" />.
    /// </summary>
    /// <typeparam name="TMessage">The type of the messages.</typeparam>
    public abstract class PubSubProvider<TMessage> : IPubSubProvider<TMessage> where TMessage : IMessage
    {
        /// <summary>
        ///     An error logger.
        /// </summary>
        private readonly ILogger logger;

        /// <summary>
        ///     Creates a new instance of <see cref="PubSubProvider{TMessage}" />.
        /// </summary>
        /// <param name="logger">An error logger.</param>
        /// <exception cref="ArgumentNullException">Is thrown if <paramref name="logger" /> is null.</exception>
        protected PubSubProvider(ILogger logger)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        ///     Handles the pub/sub messages of type <typeparamref name="TMessage" />.
        /// </summary>
        /// <param name="message">The message that is handled.</param>
        /// <returns>A <see cref="Task" />.</returns>
        /// <exception cref="ArgumentNullException">Is thrown if <paramref name="message" /> is null.</exception>
        public async Task HandleAsync(TMessage message)
        {
            if (message == null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            try
            {
                await this.HandleMessageAsync(message);
            }
            catch (Exception e)
            {
                this.logger.LogError(e, $"Unexpected error! (processId: {message.ProcessId})");
            }
        }

        /// <summary>
        ///     Handles the pub/sub messages of type <typeparamref name="TMessage" />.
        /// </summary>
        /// <param name="message">The message that is handled.</param>
        /// <returns>A <see cref="Task" />.</returns>
        protected abstract Task HandleMessageAsync(TMessage message);
    }
}
