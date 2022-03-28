namespace Md.GoogleCloudFirestore.Logic
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Md.Common.Contracts;
    using Md.GoogleCloud.Base.Contracts.Logic;

    /// <summary>
    ///     Access database in readonly mode.
    /// </summary>
    public class ReadonlyDatabase<T> : DatabaseBase, IReadOnlyDatabase<T> where T : class
    {
        /// <summary>
        ///     Factory method for creating objects.
        /// </summary>
        private readonly Func<IDictionary<string, object>, T> factory;

        /// <summary>
        ///     Creates a new instance of <see cref="ReadonlyDatabase{T}" />.
        /// </summary>
        /// <param name="runtimeEnvironment">The runtime environment.</param>
        /// <param name="collectionNameBase">The base name of the database collection.</param>
        /// <param name="factory">Factory method for creating objects.</param>
        public ReadonlyDatabase(
            IRuntimeEnvironment runtimeEnvironment,
            string collectionNameBase,
            Func<IDictionary<string, object>, T> factory
        )
            : base(runtimeEnvironment, collectionNameBase)
        {
            this.factory = factory;
        }

        /// <summary>
        ///     Creates a new instance of <see cref="ReadonlyDatabase{T}" />.
        /// </summary>
        /// <param name="databaseConfiguration">Configuration of the database.</param>
        /// <param name="factory">Factory method for creating objects.</param>
        public ReadonlyDatabase(
            IDatabaseConfiguration databaseConfiguration,
            Func<IDictionary<string, object>, T> factory
        )
            : base(databaseConfiguration)
        {
            this.factory = factory;
        }

        /// <summary>
        ///     Read a document of a database collection by its id.
        /// </summary>
        /// <param name="documentId">The id of the document.</param>
        /// <returns>A <see cref="Task" /> whose is result is a <see cref="IDictionary{TKey,TValue}" />.</returns>
        public async Task<T?> ReadByDocumentIdAsync(string documentId)
        {
            var snapshot = await this.Collection().Document(documentId).GetSnapshotAsync();
            if (snapshot.Exists)
            {
                return this.factory(snapshot.ToDictionary());
            }

            return null;
        }

        /// <summary>
        ///     Read all entries of a database collection.
        /// </summary>
        /// <returns>
        ///     A <see cref="Task" /> whose is result is an <see cref="IEnumerable{T}" /> of
        ///     <see cref="IDictionary{TKey,TValue}" />.
        /// </returns>
        public async Task<IEnumerable<T>> ReadManyAsync()
        {
            var snapshot = await this.Collection().GetSnapshotAsync();
            if (snapshot.Count > 0)
            {
                return snapshot.Where(doc => doc.Exists).Select(doc => this.factory(doc.ToDictionary())).ToArray();
            }

            return Enumerable.Empty<T>();
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
        public async Task<IEnumerable<T>> ReadManyAsync(string fieldPath, object value)
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
        public async Task<IEnumerable<T>> ReadManyAsync(string fieldPath, object value, OrderType orderType)
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
                ? snapshot.Documents.Where(doc => doc.Exists).Select(doc => this.factory(doc.ToDictionary())).ToArray()
                : Enumerable.Empty<T>();
        }

        /// <summary>
        ///     Read an entry of a database collection.
        /// </summary>
        /// <param name="fieldPath">Defines the field path.</param>
        /// <param name="value">Defines the expected value of <paramref name="fieldPath" />.</param>
        /// <returns>A <see cref="Task" /> whose is result is a <see cref="IDictionary{TKey,TValue}" />.</returns>
        public async Task<T?> ReadOneAsync(string fieldPath, object value)
        {
            var snapshot = await this.Collection().WhereEqualTo(fieldPath, value).Limit(1).GetSnapshotAsync();
            if (snapshot?.Any() == true)
            {
                var documentSnapshot = snapshot.Documents.FirstOrDefault();
                if (documentSnapshot?.Exists == true)
                {
                    return this.factory(documentSnapshot.ToDictionary());
                }
            }

            return null;
        }
    }
}
