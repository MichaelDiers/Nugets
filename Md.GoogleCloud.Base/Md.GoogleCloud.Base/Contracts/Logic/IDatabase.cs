namespace Md.GoogleCloud.Base.Contracts.Logic
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    ///     Access to the database.
    /// </summary>
    public interface IDatabase : IReadOnlyDatabase
    {
        /// <summary>
        ///     Insert a new object to the database.
        /// </summary>
        /// <param name="documentId">The id of the document.</param>
        /// <param name="data">The data to be saved.</param>
        /// <returns>A <see cref="Task{T}" /> whose result is the document id.</returns>
        Task<string> InsertAsync(string documentId, IToDictionary data);

        /// <summary>
        ///     Insert a new object to the database.
        /// </summary>
        /// <param name="data">The data to be saved.</param>
        /// <returns>A <see cref="Task{T}" /> whose result is the document id.</returns>
        Task<string> InsertAsync(IToDictionary data);

        /// <summary>
        ///     Update a document by its document id.
        /// </summary>
        /// <param name="documentId">The id of the document.</param>
        /// <param name="updates">The fields to be updated.</param>
        /// <returns>A <see cref="Task" />.</returns>
        Task UpdateByDocumentIdAsync(string documentId, IDictionary<string, object> updates);


        /// <summary>
        ///     Update a document that matches <paramref name="fieldPath" /> and <paramref name="value" />.
        /// </summary>
        /// <param name="fieldPath">The document is selected by the field path.</param>
        /// <param name="value">The field path should be this value.</param>
        /// <param name="updates">The updates for the document.</param>
        /// <returns>A <see cref="Task" />.</returns>
        Task UpdateOneAsync(string fieldPath, object value, IDictionary<string, object> updates);
    }
}
