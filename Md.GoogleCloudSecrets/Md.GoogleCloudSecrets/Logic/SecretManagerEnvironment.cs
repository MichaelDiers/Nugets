namespace Md.GoogleCloudSecrets.Logic
{
    using Md.Common.Extensions;
    using Md.GoogleCloud.Base.Contracts.Logic;

    /// <summary>
    ///     Environment for <see cref="SecretManager" />.
    /// </summary>
    public class SecretManagerEnvironment : ISecretManagerEnvironment
    {
        /// <summary>
        ///     Creates a new instance of <see cref="SecretManagerEnvironment" />.
        /// </summary>
        /// <param name="projectId">The id of the project.</param>
        public SecretManagerEnvironment(string projectId)
        {
            this.ProjectId = projectId.ValidateIsNotNullOrWhitespace(nameof(projectId));
        }

        /// <summary>
        ///     Gets the id of the project.
        /// </summary>
        public string ProjectId { get; }
    }
}
