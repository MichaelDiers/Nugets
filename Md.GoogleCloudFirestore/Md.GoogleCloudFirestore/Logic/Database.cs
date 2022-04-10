namespace Md.GoogleCloudFirestore.Logic
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Google.Cloud.Firestore;
    using Md.Common.Contracts.Model;
    using Md.Common.Database;
    using Md.GoogleCloudFirestore.Contracts.Logic;
    using Md.GoogleCloudFirestore.Contracts.Model;

    /// <summary>
    ///     Access to the database.
    /// </summary>
    public class Database<T> : ReadonlyDatabase<T>, IDatabase<T> where T : class
    {
        /// <summary>
        ///     Creates a new instance of <see cref="Database{T}" />.
        /// </summary>
        /// <param name="databaseConfiguration">Configuration of the database.</param>
        /// <param name="factory">Factory method for creating objects.</param>
        public Database(IDatabaseConfiguration databaseConfiguration, Func<IDictionary<string, object>, T> factory)
            : base(databaseConfiguration, factory)
        {
        }

        /// <summary>
        ///     Creates a new instance of <see cref="Database{T}" />.
        /// </summary>
        /// <param name="runtimeEnvironment">The runtime environment.</param>
        /// <param name="collectionNameBase">The base name of the database collection.</param>
        /// <param name="factory">Factory method for creating objects.</param>
        public Database(
            IRuntimeEnvironment runtimeEnvironment,
            string collectionNameBase,
            Func<IDictionary<string, object>, T> factory
        )
            : base(runtimeEnvironment, collectionNameBase, factory)
        {
        }

        /// <summary>
        ///     Insert a new object to the database.
        /// </summary>
        /// <param name="documentId">The id of the document.</param>
        /// <param name="data">The data to be saved.</param>
        /// <returns>A <see cref="Task{T}" /> whose result is the id of the document.</returns>
        public async Task<string> InsertAsync(string documentId, IToDictionary data)
        {
            var documentReference = this.Collection().Document(documentId);
            return await Database<T>.InsertAsync(documentReference, data);
        }

        /// <summary>
        ///     Insert a new object to the database.
        /// </summary>
        /// <param name="data">The data to be saved.</param>
        /// <returns>A <see cref="Task{T}" /> whose result is the id of the document.</returns>
        public async Task<string> InsertAsync(IToDictionary data)
        {
            var documentReference = this.Collection().Document();
            return await Database<T>.InsertAsync(documentReference, data);
        }

        /// <summary>
        ///     Insert a new object to the database.
        /// </summary>
        /// <param name="documentReference">The document reference used for inserting.</param>
        /// <param name="data">The data to be saved.</param>
        /// <returns>A <see cref="Task" /> whose result is the id of the document.</returns>
        private static async Task<string> InsertAsync(DocumentReference documentReference, IToDictionary data)
        {
            var document = data.ToDictionary();
            document.Add(DatabaseObject.CreatedName, FieldValue.ServerTimestamp);
            if (document.ContainsKey(DatabaseObject.DocumentIdName))
            {
                document.Remove(DatabaseObject.DocumentIdName);
            }

            await documentReference.CreateAsync(document);
            return documentReference.Id;
        }
    }
}
