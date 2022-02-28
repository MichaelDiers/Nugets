namespace Md.GoogleCloudPubSub.Contracts.Messages
{
    /// <summary>
    ///     Specifies a pub/sub message.
    /// </summary>
    public interface IMessage
    {
        /// <summary>
        ///     Gets the id of the business process.
        /// </summary>
        string ProcessId { get; }
    }
}
