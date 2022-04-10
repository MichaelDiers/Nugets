namespace Md.Common.Contracts.Model
{
    /// <summary>
    ///     Describes the runtime environment.
    /// </summary>
    public interface IRuntimeEnvironment
    {
        /// <summary>
        ///     Gets the environment.
        /// </summary>
        Environment Environment { get; }

        /// <summary>
        ///     Gets the id of the project.
        /// </summary>
        string ProjectId { get; }
    }
}
