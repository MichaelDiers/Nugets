namespace Md.GoogleCloudFirestore.Logic
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Md.GoogleCloud.Base.Contracts.Logic;

    /// <summary>
    ///     Access database in readonly mode.
    /// </summary>
    public class ReadonlyDatabase : DatabaseBase, IReadOnlyDatabase
    {
        /// <summary>
        ///     Creates a new instance of <see cref="ReadonlyDatabase" />.
        /// </summary>
        /// <param name="databaseConfiguration">Configuration of the database.</param>
        public ReadonlyDatabase(IDatabaseConfiguration databaseConfiguration)
            : base(databaseConfiguration)
        {
        }

        /// <summary>
        ///     Read a document of a database collection by its id.
        /// </summary>
        /// <param name="documentId">The id of the document.</param>
        /// <returns>A <see cref="Task" /> whose is result is a <see cref="IDictionary{TKey,TValue}" />.</returns>
        public async Task<IDictionary<string, object>?> ReadByDocumentIdAsync(string documentId)
        {
            var snapshot = await this.Collection().Document(documentId).GetSnapshotAsync();
            return snapshot.Exists ? snapshot.ToDictionary() : null;
        }

        /// <summary>
        ///     Read all entries of a database collection.
        /// </summary>
        /// <param name="fieldPath">Defines the field path.</param>
        /// <param name="value">Defines the expected value of <paramref name="fieldPath" />.</param>
        /// <returns>
        ///     A <see cref="Task" /> whose is result is an <see cref="IEnumerable{T}" /> of
        ///     <see cref="IDictionary{TKey,TValue}" />.
        /// </returns>
        public async Task<IEnumerable<IDictionary<string, object>>> ReadManyAsync(string fieldPath, object value)
        {
            return await this.ReadManyAsync(fieldPath, value, OrderType.Unsorted);
        }

        /// <summary>
        ///     Read all entries of a database collection.
        /// </summary>
        /// <param name="fieldPath">Defines the field path.</param>
        /// <param name="value">Defines the expected value of <paramref name="fieldPath" />.</param>
        /// <param name="orderType">Specifies the sorting order.</param>
        /// <returns>
        ///     A <see cref="Task" /> whose is result is an <see cref="IEnumerable{T}" /> of
        ///     <see cref="IDictionary{TKey,TValue}" />.
        /// </returns>
        public async Task<IEnumerable<IDictionary<string, object>>> ReadManyAsync(
            string fieldPath,
            object value,
            OrderType orderType
        )
        {
            var query = this.Collection().WhereEqualTo(fieldPath, value);
            switch (orderType)
            {
                case OrderType.Unsorted:
                    break;
                case OrderType.Asc:
                    query = query.OrderBy("created");
                    break;
                case OrderType.Desc:
                    query = query.OrderByDescending("created");
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(orderType), orderType, null);
            }

            var snapshot = await query.GetSnapshotAsync();
            return snapshot?.Any() == true
                ? snapshot.Documents.Where(doc => doc.Exists).Select(doc => doc.ToDictionary()).ToArray()
                : Enumerable.Empty<IDictionary<string, object>>();
        }

        /// <summary>
        ///     Read an entry of a database collection.
        /// </summary>
        /// <param name="fieldPath">Defines the field path.</param>
        /// <param name="value">Defines the expected value of <paramref name="fieldPath" />.</param>
        /// <returns>A <see cref="Task" /> whose is result is a <see cref="IDictionary{TKey,TValue}" />.</returns>
        public async Task<IDictionary<string, object>?> ReadOneAsync(string fieldPath, object value)
        {
            var snapshot = await this.Collection().WhereEqualTo(fieldPath, value).Limit(1).GetSnapshotAsync();
            if (snapshot?.Any() == true)
            {
                var documentSnapshot = snapshot.Documents.FirstOrDefault();
                if (documentSnapshot?.Exists == true)
                {
                    return documentSnapshot.ToDictionary();
                }
            }

            return null;
        }
    }
}
