namespace Md.Common.Model
{
    using Md.Common.Contracts.Model;

    /// <summary>
    ///     Describes the runtime environment.
    /// </summary>
    public class RuntimeEnvironment : IRuntimeEnvironment
    {
        /// <summary>
        ///     Gets the environment.
        /// </summary>
        public Environment Environment { get; set; } = Environment.None;

        /// <summary>
        ///     Gets the id of the project.
        /// </summary>
        public string ProjectId { get; set; } = string.Empty;
    }
}
