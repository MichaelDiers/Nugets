namespace Md.GoogleCloud.Base.Logic
{
    using System;
    using System.Threading.Tasks;
    using Md.GoogleCloud.Base.Contracts.Logic;
    using Md.GoogleCloud.Base.Contracts.Messages;
    using Microsoft.Extensions.Logging;

    /// <summary>
    ///     Handles the pub/sub messages of type <typeparamref name="TMessage" />.
    /// </summary>
    /// <typeparam name="TMessage">The type of the messages.</typeparam>
    /// <typeparam name="TCategoryName">The type whose name will be the logger's category name.</typeparam>
    public abstract class PubSubProvider<TMessage, TCategoryName> : IPubSubProvider<TMessage> where TMessage : IMessage
    {
        /// <summary>
        ///     An error logger.
        /// </summary>
        private readonly ILogger<TCategoryName> logger;

        /// <summary>
        ///     Creates a new instance of <see cref="PubSubProvider{TMessage, TCategoryName}" />.
        /// </summary>
        /// <param name="logger">An error logger.</param>
        /// <exception cref="ArgumentNullException">Is thrown if <paramref name="logger" /> is null.</exception>
        protected PubSubProvider(ILogger<TCategoryName> logger)
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
        ///     Log an error message.
        /// </summary>
        /// <param name="ex">The raised exception.</param>
        /// <param name="message">An error message.</param>
        /// <returns>A <see cref="Task" />.</returns>
        public Task LogErrorAsync(Exception ex, string message)
        {
            this.logger.LogError(ex, message);
            return Task.CompletedTask;
        }

        /// <summary>
        ///     Handles the pub/sub messages of type <typeparamref name="TMessage" />.
        /// </summary>
        /// <param name="message">The message that is handled.</param>
        /// <returns>A <see cref="Task" />.</returns>
        protected abstract Task HandleMessageAsync(TMessage message);
    }
}
