namespace Md.Common.Contracts.Model
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
        Test,

        /// <summary>
        ///     Stage environment.
        /// </summary>
        Stage,

        /// <summary>
        ///     Prod environment.
        /// </summary>
        Prod
    }
}
