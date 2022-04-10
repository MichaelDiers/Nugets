namespace Md.GoogleCloudFunctions.Tests.Logic
{
    using System;
    using System.Threading.Tasks;
    using Md.GoogleCloudFunctions.Logic;
    using Microsoft.Extensions.Logging;
    using Xunit;

    internal class PubSubProviderImplementation : PubSubProvider<ITestMessage, PubSubFunctionImplementation>
    {
        private readonly ITestMessage testMessage;

        /// <summary>
        ///     Creates a new instance of <see cref="PubSubProviderImplementation" />.
        /// </summary>
        /// <param name="logger">An error logger.</param>
        /// <param name="testMessage">The expected input for <see cref="HandleMessageAsync" />.</param>
        /// <exception cref="ArgumentNullException">Is thrown if <paramref name="logger" /> is null.</exception>
        public PubSubProviderImplementation(ILogger<PubSubFunctionImplementation> logger, ITestMessage testMessage)
            : base(logger)
        {
            this.testMessage = testMessage;
        }

        /// <summary>
        ///     Handles the pub/sub messages.
        /// </summary>
        /// <param name="message">The message that is handled.</param>
        /// <returns>A <see cref="Task" />.</returns>
        protected override Task HandleMessageAsync(ITestMessage message)
        {
            Assert.Equal(this.testMessage.Data, message.Data);
            Assert.Equal(this.testMessage.ProcessId, message.ProcessId);
            return Task.CompletedTask;
        }
    }
}
