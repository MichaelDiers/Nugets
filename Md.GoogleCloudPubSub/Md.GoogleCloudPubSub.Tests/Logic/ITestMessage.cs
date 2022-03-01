﻿namespace Md.GoogleCloudPubSub.Tests.Logic
{
    using Md.GoogleCloud.Base.Contracts.Messages;

    /// <summary>
    ///     Specifies a test message.
    /// </summary>
    internal interface ITestMessage : IMessage
    {
        string Data { get; }
    }
}
