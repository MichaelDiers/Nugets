namespace Md.GoogleCloudFirestore.Logic
{
    using System;
    using Google.Cloud.Firestore;
    using Md.Common.Contracts.Model;
    using Md.GoogleCloudFirestore.Contracts.Model;
    using Md.GoogleCloudFirestore.Model;

    /// <summary>
    ///     Base class for database access.
    /// </summary>
    public abstract class DatabaseBase
    {
        /// <summary>
        ///     Gets the configuration of the database.
        /// </summary>
        private readonly IDatabaseConfiguration databaseConfiguration;

        /// <summary>
        ///     Access to the database implementation.
        /// </summary>
        private readonly FirestoreDb firestoreDb;

        /// <summary>
        ///     Creates a new instance of <see cref="DatabaseBase" />.
        /// </summary>
        /// <param name="runtimeEnvironment">The runtime environment.</param>
        /// <param name="collectionNameBase">The base name of the database collection.</param>
        protected DatabaseBase(IRuntimeEnvironment runtimeEnvironment, string collectionNameBase)
            : this(
                new DatabaseConfiguration(
                    runtimeEnvironment.ProjectId,
                    $"{collectionNameBase}-{runtimeEnvironment.Environment.ToString().ToLowerInvariant()}"))
        {
        }

        /// <summary>
        ///     Creates a new instance of <see cref="DatabaseBase" />.
        /// </summary>
        /// <param name="databaseConfiguration">Configuration of the database.</param>
        protected DatabaseBase(IDatabaseConfiguration databaseConfiguration)
        {
            this.databaseConfiguration =
                databaseConfiguration ?? throw new ArgumentNullException(nameof(databaseConfiguration));
            this.firestoreDb = FirestoreDb.Create(databaseConfiguration.ProjectId);
        }

        /// <summary>
        ///     Gets a reference to the database collection.
        /// </summary>
        /// <returns>A <see cref="CollectionReference" />.</returns>
        protected CollectionReference Collection()
        {
            return this.firestoreDb.Collection(this.databaseConfiguration.CollectionName);
        }
    }
}
