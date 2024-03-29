﻿namespace Md.GoogleCloudPubSub.Model
{
    using System;
    using Md.GoogleCloudPubSub.Contracts.Model;

    /// <summary>
    ///     The configuration for sending messages to pub/sub.
    /// </summary>
    public class PubSubClientConfiguration : IPubSubClientConfiguration
    {
        /// <summary>
        ///     Creates a new instance of <see cref="PubSubClientConfiguration" />.
        /// </summary>
        /// <param name="projectId">The id of the google cloud project.</param>
        /// <param name="topicName">The name of the pub/sub topic.</param>
        public PubSubClientConfiguration(string projectId, string topicName)
        {
            if (string.IsNullOrWhiteSpace(projectId))
            {
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(projectId));
            }

            if (string.IsNullOrWhiteSpace(topicName))
            {
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(topicName));
            }

            this.ProjectId = projectId;
            this.TopicName = topicName;
        }

        /// <summary>
        ///     Gets the id of the google cloud project.
        /// </summary>
        public string ProjectId { get; }

        /// <summary>
        ///     Gets the name of the pub/sub topic.
        /// </summary>
        public string TopicName { get; }
    }
}
