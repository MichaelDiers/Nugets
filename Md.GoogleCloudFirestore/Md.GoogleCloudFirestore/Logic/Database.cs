namespace Md.GoogleCloudFirestore.Logic
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Google.Cloud.Firestore;
    using Md.Common.Contracts;
    using Md.GoogleCloud.Base.Contracts.Logic;

    /// <summary>
    ///     Access to the database.
    /// </summary>
    public class Database<T> : ReadonlyDatabase<T>, IDatabase<T> where T : class
    {
        /// <summary>
        ///     Name of the crated field.
        /// </summary>
        public const string CreatedName = "created";

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
        ///     Update a document by its document id.
        /// </summary>
        /// <param name="documentId">The id of the document.</param>
        /// <param name="updates">The fields to be updated.</param>
        /// <returns>A <see cref="Task" />.</returns>
        public async Task UpdateByDocumentIdAsync(string documentId, IDictionary<string, object> updates)
        {
            await this.Collection().Document(documentId).UpdateAsync(updates);
        }

        /// <summary>
        ///     Update a document that matches <paramref name="fieldPath" /> and <paramref name="value" />.
        /// </summary>
        /// <param name="fieldPath">The document is selected by the field path.</param>
        /// <param name="value">The field path should be this value.</param>
        /// <param name="updates">The updates for the document.</param>
        /// <returns>A <see cref="Task" />.</returns>
        public async Task UpdateOneAsync(string fieldPath, object value, IDictionary<string, object> updates)
        {
            var result = await this.Collection().WhereEqualTo(fieldPath, value).Limit(1).GetSnapshotAsync();
            if (result.Count != 1)
            {
                return;
            }

            await this.UpdateByDocumentIdAsync(result.First().Id, updates);
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
            document.Add(Database<T>.CreatedName, FieldValue.ServerTimestamp);
            await documentReference.CreateAsync(document);
            return documentReference.Id;
        }
    }
}
