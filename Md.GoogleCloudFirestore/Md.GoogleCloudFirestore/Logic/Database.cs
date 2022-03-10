namespace Md.GoogleCloudFirestore.Logic
{
    using System.Threading.Tasks;
    using Google.Cloud.Firestore;
    using Md.GoogleCloud.Base.Contracts.Logic;

    /// <summary>
    ///     Access to the database.
    /// </summary>
    public class Database : ReadonlyDatabase, IDatabase
    {
        /// <summary>
        ///     Creates a new instance of <see cref="Database" />.
        /// </summary>
        /// <param name="databaseConfiguration">Configuration of the database.</param>
        public Database(IDatabaseConfiguration databaseConfiguration)
            : base(databaseConfiguration)
        {
        }

        /// <summary>
        ///     Insert a new object to the database.
        /// </summary>
        /// <param name="documentId">The id of the document.</param>
        /// <param name="data">The data to be saved.</param>
        /// <returns>A <see cref="Task" />.</returns>
        public async Task InsertAsync(string documentId, IToDictionary data)
        {
            var documentReference = this.Collection().Document(documentId);
            await InsertAsync(documentReference, data);
        }

        /// <summary>
        ///     Insert a new object to the database.
        /// </summary>
        /// <param name="data">The data to be saved.</param>
        /// <returns>A <see cref="Task" />.</returns>
        public async Task InsertAsync(IToDictionary data)
        {
            var documentReference = this.Collection().Document();
            await InsertAsync(documentReference, data);
        }

        /// <summary>
        ///     Insert a new object to the database.
        /// </summary>
        /// <param name="documentReference">The document reference used for inserting.</param>
        /// <param name="data">The data to be saved.</param>
        /// <returns>A <see cref="Task" />.</returns>
        private static async Task InsertAsync(DocumentReference documentReference, IToDictionary data)
        {
            var document = data.ToDictionary();
            document.Add("created", FieldValue.ServerTimestamp);
            await documentReference.CreateAsync(document);
        }
    }
}
