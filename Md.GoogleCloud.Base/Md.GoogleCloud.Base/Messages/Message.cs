namespace Md.GoogleCloud.Base.Messages
{
    using System;
    using Md.Common.Extensions;
    using Md.GoogleCloud.Base.Contracts.Messages;
    using Newtonsoft.Json;

    /// <summary>
    ///     Specifies a pub/sub message.
    /// </summary>
    public class Message : IMessage
    {
        /// <summary>
        ///     Creates a new instance of <see cref="Message" />.
        /// </summary>
        /// <param name="processId">The id of the business process.</param>
        /// <exception cref="ArgumentException">Is thrown if <paramref name="processId" /> is null or whitespace.</exception>
        public Message(string processId)
        {
            this.ProcessId = processId.ValidateIsAGuid();
        }

        /// <summary>
        ///     Gets the id of the business process.
        /// </summary>
        [JsonProperty("processId", Required = Required.Always, Order = 1)]
        public string ProcessId { get; }
    }
}
