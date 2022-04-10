namespace Md.GoogleCloudSecrets.Tests.Integration
{
    using Md.Common.Contracts.Model;
    using Md.GoogleCloudSecrets.Contracts.Logic;
    using Md.GoogleCloudSecrets.Logic;
    using Xunit;

    public class SecretManagerTests
    {
        [Theory(Skip = "IntegrationOnly")]
        [InlineData("projectId", "key")]
        public async void GetString(string projectId, string key)
        {
            var manager = new SecretManager(new SecretManagerEnvironment(projectId, Environment.Test));


            var value1 = await manager.GetStringAsync(key);
            var value2 = await ((ISecretManager) manager).GetStringAsync(key);
            Assert.Equal(value1, value2);
        }
    }
}
