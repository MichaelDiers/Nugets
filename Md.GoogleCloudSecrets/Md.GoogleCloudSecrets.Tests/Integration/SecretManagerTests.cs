namespace Md.GoogleCloudSecrets.Tests.Integration
{
    using Md.GoogleCloudSecrets.Logic;

    public class SecretManagerTests
    {
        public async void GetString(string projectId, string key)
        {
            var value = await new SecretManager(new SecretManagerEnvironment(projectId)).GetStringAsync(key);
        }
    }
}
