namespace Md.GoogleCloud.Base.Contracts.Logic
{
    /// <summary>
    ///     Environment of the secret manager.
    /// </summary>
    public interface ISecretManagerEnvironment
    {
        /// <summary>
        ///     Gets the id of the project.
        /// </summary>
        string ProjectId { get; }
    }
}
