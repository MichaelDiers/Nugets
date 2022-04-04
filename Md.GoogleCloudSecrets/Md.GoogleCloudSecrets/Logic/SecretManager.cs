namespace Md.GoogleCloudSecrets.Logic
{
    using System.Threading.Tasks;
    using Google.Cloud.SecretManager.V1;
    using Md.Common.Contracts;
    using Md.Common.Extensions;
    using Md.GoogleCloud.Base.Contracts.Logic;

    /// <summary>
    ///     Access google cloud secret manager.
    /// </summary>
    public class SecretManager : ISecretManager
    {
        /// <summary>
        ///     The runtime environment.
        /// </summary>
        private readonly Environment environment;

        /// <summary>
        ///     The id of the project.
        /// </summary>
        private readonly string projectId;

        /// <summary>
        ///     Creates a new instance of <see cref="SecretManager" />.
        /// </summary>
        /// <param name="environment">The environment of the secret manager.</param>
        public SecretManager(ISecretManagerEnvironment environment)
        {
            this.environment = environment.Environment.IsDefined(nameof(environment));
            this.projectId = environment.ProjectId.ValidateIsNotNullOrWhitespace(nameof(SecretManager.projectId));
        }

        /// <summary>
        ///     Read a string for a given key from the secret manager.
        /// </summary>
        /// <param name="key">The requested key.</param>
        /// <returns>The value of the key.</returns>
        public async Task<string> GetStringAsync(string key)
        {
            // Create the client.
            var client = await SecretManagerServiceClient.CreateAsync();
            var secret = await client.AccessSecretVersionAsync(
                new SecretVersionName(this.projectId, $"{key}_{this.environment.ToString().ToUpper()}", "latest"));
            return secret.Payload.Data.ToStringUtf8();
        }
    }
}
