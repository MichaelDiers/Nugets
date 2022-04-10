namespace Md.GoogleCloudFirestore.Contracts.Logic
{
    /// <summary>
    ///     Specifies the sorting order.
    /// </summary>
    public enum OrderType
    {
        /// <summary>
        ///     Value is not defined.
        /// </summary>
        Undefined = 0,

        /// <summary>
        ///     Do not sort the results.
        /// </summary>
        Unsorted = 1,

        /// <summary>
        ///     Sort in ascending order.
        /// </summary>
        Asc = 2,

        /// <summary>
        ///     Sort in descending order.
        /// </summary>
        Desc = 3
    }
}
