namespace Md.Common.Contracts
{
    using System.Runtime.Serialization;

    /// <summary>
    ///     Specifies the runtime environment.
    /// </summary>
    public enum Environment
    {
        /// <summary>
        ///     Undefined value.
        /// </summary>
        [EnumMember(Value = "None")]
        None = 0,

        /// <summary>
        ///     Test environment.
        /// </summary>
        [EnumMember(Value = "Test")]
        Test = 1,

        /// <summary>
        ///     Stage environment.
        /// </summary>
        [EnumMember(Value = "Stage")]
        Stage = 2,

        /// <summary>
        ///     Prod environment.
        /// </summary>
        [EnumMember(Value = "Prod")]
        Prod = 3
    }
}
