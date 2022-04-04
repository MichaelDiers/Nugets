namespace Md.GoogleCloudSecrets.Logic
{
    using Md.Common.Contracts;
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
        /// <param name="environment">The environment.</param>
        public SecretManagerEnvironment(string projectId, Environment environment)
        {
            this.Environment = environment.IsDefined(nameof(environment));
            this.ProjectId = projectId.ValidateIsNotNullOrWhitespace(nameof(projectId));
        }

        /// <summary>
        ///     Gets the environment.
        /// </summary>
        public Environment Environment { get; }

        /// <summary>
        ///     Gets the id of the project.
        /// </summary>
        public string ProjectId { get; }
    }
}
