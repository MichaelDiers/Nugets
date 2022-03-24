namespace Md.Common.Contracts
{
    /// <summary>
    ///     Specifies the runtime environment.
    /// </summary>
    public enum Environment
    {
        /// <summary>
        ///     Undefined value.
        /// </summary>
        None = 0,

        /// <summary>
        ///     Test environment.
        /// </summary>
        Test = 1,

        /// <summary>
        ///     Stage environment.
        /// </summary>
        Stage = 2,

        /// <summary>
        ///     Prod environment.
        /// </summary>
        Prod = 3
    }
}
