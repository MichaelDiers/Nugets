﻿namespace Md.GoogleCloudPubSub.Tests.Logic
{
    using Md.GoogleCloudPubSub.Contracts.Messages;

    /// <summary>
    ///     Specifies a test message.
    /// </summary>
    internal interface ITestMessage : IMessage
    {
        string Data { get; }
    }
}
