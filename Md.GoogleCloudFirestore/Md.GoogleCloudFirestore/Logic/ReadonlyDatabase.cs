namespace Md.GoogleCloudFirestore.Logic
{
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
    }
}
