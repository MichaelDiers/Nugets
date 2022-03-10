﻿namespace Md.GoogleCloud.Base.Logic
{
    using Md.Common.Extensions;
    using Md.GoogleCloud.Base.Contracts.Logic;

    /// <summary>
    ///     Configuration of the database.
    /// </summary>
    public class DatabaseConfiguration : IDatabaseConfiguration
    {
        /// <summary>
        ///     Creates a new instance of <see cref="DatabaseConfiguration" />.
        /// </summary>
        /// <param name="projectId">The id of the google cloud project.</param>
        /// <param name="collectionName">The name of the collection.</param>
        public DatabaseConfiguration(string projectId, string collectionName)
        {
            this.ProjectId = projectId.ValidateIsNotNullOrWhitespace();
            this.CollectionName = collectionName.ValidateIsNotNullOrWhitespace();
        }

        /// <summary>
        ///     Gets the name of the collection.
        /// </summary>
        public string CollectionName { get; }

        /// <summary>
        ///     Gets the id of the google cloud project.
        /// </summary>
        public string ProjectId { get; }
    }
}
