namespace Md.GoogleCloudPubSub.Base.Tests.Logic
{
    using Md.GoogleCloudPubSub.Base.Contracts.Messages;

    /// <summary>
    ///     Specifies a test message.
    /// </summary>
    internal interface ITestMessage : IMessage
    {
        string Data { get; }
    }
}
