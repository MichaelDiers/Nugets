namespace Md.GoogleCloudSecrets.Contracts.Logic
{
    using Md.Common.Contracts.Model;

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
