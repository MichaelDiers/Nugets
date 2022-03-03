namespace Md.GoogleCloudFirestore.Logic
{
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
        ///     Read all entries of a database collection.
        /// </summary>
        /// <param name="fieldPath">Defines the field path.</param>
        /// <param name="value">Defines the expected value of <paramref name="fieldPath" />.</param>
        /// <returns>
        ///     A <see cref="Task" /> whose is result is an <see cref="IEnumerable{T}" /> of
        ///     <see cref="IDictionary{TKey,TValue}" />.
        /// </returns>
        public async Task<IEnumerable<IDictionary<string, object>>> ReadMany(string fieldPath, object value)
        {
            var snapshot = await this.Collection().WhereEqualTo(fieldPath, value).GetSnapshotAsync();
            if (snapshot?.Any() == true)
            {
                return snapshot.Documents.Where(doc => doc.Exists).Select(doc => doc.ToDictionary()).ToArray();
            }

            return Enumerable.Empty<IDictionary<string, object>>();
        }

        /// <summary>
        ///     Read an entry of a database collection.
        /// </summary>
        /// <param name="fieldPath">Defines the field path.</param>
        /// <param name="value">Defines the expected value of <paramref name="fieldPath" />.</param>
        /// <returns>A <see cref="Task" /> whose is result is a <see cref="IDictionary{TKey,TValue}" />.</returns>
        public async Task<IDictionary<string, object>?> ReadOne(string fieldPath, object value)
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
