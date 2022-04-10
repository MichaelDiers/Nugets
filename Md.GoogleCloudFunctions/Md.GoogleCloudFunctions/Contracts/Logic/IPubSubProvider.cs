namespace Md.GoogleCloudFunctions.Contracts.Logic
{
    using System;
    using System.Threading.Tasks;
    using Md.Common.Contracts.Messages;

    /// <summary>
    ///     Handles the pub/sub messages of type <typeparamref name="TMessage" />.
    /// </summary>
    /// <typeparam name="TMessage">The type of the messages.</typeparam>
    public interface IPubSubProvider<in TMessage> where TMessage : IMessage
    {
        /// <summary>
        ///     Handles the pub/sub messages of type <typeparamref name="TMessage" />.
        /// </summary>
        /// <param name="message">The message that is handled.</param>
        /// <returns>A <see cref="Task" />.</returns>
        /// <exception cref="ArgumentNullException">Is thrown if <paramref name="message" /> is null.</exception>
        Task HandleAsync(TMessage message);

        /// <summary>
        ///     Log an error message.
        /// </summary>
        /// <param name="ex">The raised exception.</param>
        /// <param name="message">An error message.</param>
        /// <returns>A <see cref="Task" />.</returns>
        Task LogErrorAsync(Exception ex, string message);
    }
}
