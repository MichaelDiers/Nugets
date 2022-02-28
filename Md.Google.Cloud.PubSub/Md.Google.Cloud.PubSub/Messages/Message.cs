﻿namespace Md.GoogleCloudPubSub.Messages
{
    using System;
    using Md.GoogleCloudPubSub.Contracts.Messages;

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
            if (string.IsNullOrWhiteSpace(processId))
            {
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(processId));
            }

            this.ProcessId = processId;
        }

        /// <summary>
        ///     Gets the id of the business process.
        /// </summary>
        public string ProcessId { get; }
    }
}
