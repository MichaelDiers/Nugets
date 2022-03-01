namespace Md.GoogleCloudPubSub.Tests.Logic
{
    using System;
    using Md.GoogleCloudPubSub.Base.Contracts.Logic;
    using Md.GoogleCloudPubSub.Logic;
    using Microsoft.Extensions.Logging;

    internal class
        PubSubFunctionImplementation : PubSubFunction<TestMessage, ITestMessage, PubSubFunctionImplementation>
    {
        /// <summary>
        ///     Creates a new instance of <see cref="PubSubFunction{TMessage,TIMessage,TCategoryName}" />.
        /// </summary>
        /// <param name="logger">An error logger.</param>
        /// <param name="provider">Handles the cloud event for messages of type <see name="TestMessage" />.</param>
        /// <exception cref="ArgumentNullException">
        ///     Is thrown if <paramref name="logger" /> or <paramref name="provider" /> is
        ///     null.
        /// </exception>
        public PubSubFunctionImplementation(
            ILogger<PubSubFunctionImplementation> logger,
            IPubSubProvider<ITestMessage> provider
        )
            : base(logger, provider)
        {
        }
    }
}
