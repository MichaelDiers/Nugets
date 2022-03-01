namespace Md.GoogleCloudPubSub.Tests.Logic
{
    using System;
    using Md.GoogleCloudPubSub.Base.Messages;
    using Newtonsoft.Json;

    /// <summary>
    ///     Specifies a test message.
    /// </summary>
    internal class TestMessage : Message, ITestMessage
    {
        /// <summary>
        ///     Creates a new instance of <see cref="TestMessage" />.
        /// </summary>
        /// <param name="processId">The id of the business process.</param>
        /// <param name="data">Additional test data.</param>
        /// <exception cref="ArgumentException">Is thrown if <paramref name="processId" /> is null or whitespace.</exception>
        public TestMessage(string processId, string data)
            : base(processId)
        {
            this.Data = data;
        }

        /// <summary>
        ///     Gets the data.
        /// </summary>
        [JsonProperty("data", Required = Required.Always, Order = 2)]
        public string Data { get; }
    }
}
