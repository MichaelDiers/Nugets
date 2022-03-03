namespace Md.GoogleCloud.Base.Contracts.Logic
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    ///     Readonly operations for databases.
    /// </summary>
    public interface IReadOnlyDatabase
    {
        /// <summary>
        ///     Read a document of a database collection by its id.
        /// </summary>
        /// <param name="documentId">The id of the document.</param>
        /// <returns>A <see cref="Task" /> whose is result is a <see cref="IDictionary{TKey,TValue}" />.</returns>
        Task<IDictionary<string, object>?> ReadByDocumentId(string documentId);

        /// <summary>
        ///     Read all entries of a database collection.
        /// </summary>
        /// <param name="fieldPath">Defines the field path.</param>
        /// <param name="value">Defines the expected value of <paramref name="fieldPath" />.</param>
        /// <returns>
        ///     A <see cref="Task" /> whose is result is an <see cref="IEnumerable{T}" /> of
        ///     <see cref="IDictionary{TKey,TValue}" />.
        /// </returns>
        Task<IEnumerable<IDictionary<string, object>>> ReadMany(string fieldPath, object value);

        /// <summary>
        ///     Read an entry of a database collection.
        /// </summary>
        /// <param name="fieldPath">Defines the field path.</param>
        /// <param name="value">Defines the expected value of <paramref name="fieldPath" />.</param>
        /// <returns>A <see cref="Task" /> whose is result is a <see cref="IDictionary{TKey,TValue}" />.</returns>
        Task<IDictionary<string, object>?> ReadOne(string fieldPath, object value);
    }
}
