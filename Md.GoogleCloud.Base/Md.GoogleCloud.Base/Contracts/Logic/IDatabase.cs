namespace Md.GoogleCloud.Base.Contracts.Logic
{
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
    }
}
