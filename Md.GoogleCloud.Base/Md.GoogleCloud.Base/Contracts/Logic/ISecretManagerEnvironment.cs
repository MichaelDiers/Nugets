namespace Md.GoogleCloud.Base.Contracts.Logic
{
    using Md.Common.Contracts;

    /// <summary>
    ///     Environment of the secret manager.
    /// </summary>
    public interface ISecretManagerEnvironment
    {
        /// <summary>
        ///     Gets the environment.
        /// </summary>
        public Environment Environment { get; }

        /// <summary>
        ///     Gets the id of the project.
        /// </summary>
        string ProjectId { get; }
    }
}
