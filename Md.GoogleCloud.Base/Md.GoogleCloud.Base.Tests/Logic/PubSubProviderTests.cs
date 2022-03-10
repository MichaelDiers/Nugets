namespace Md.GoogleCloud.Base.Tests.Logic
{
    using System;
    using Md.GoogleCloud.Base.Contracts.Logic;
    using Md.GoogleCloud.Base.Logic;
    using Md.GoogleCloud.Base.Tests.Mocks;
    using Xunit;

    /// <summary>
    ///     Tests for <see cref="PubSubProvider{TMessage,TCategoryName}" />.
    /// </summary>
    public class PubSubProviderTests
    {
        [Fact]
        public async void HandleAsync()
        {
            var id = Guid.NewGuid().ToString();
            const string data = "data";

            var logger = new LoggerMock<PubSubProviderImplementation>();
            var provider =
                new PubSubProviderImplementation(logger, new TestMessage(id, data)) as IPubSubProvider<TestMessage>;
            await provider.HandleAsync(new TestMessage(id, data));
            Assert.False(logger.HasErrors);
        }

        [Fact]
        public void ImplementsIPubSubProvider()
        {
            Assert.IsAssignableFrom<IPubSubProvider<TestMessage>>(
                new PubSubProviderImplementation(
                    new LoggerMock<PubSubProviderImplementation>(),
                    new TestMessage(Guid.NewGuid().ToString(), "data")));
        }

        [Fact]
        public async void LogErrorAsync()
        {
            var id = Guid.NewGuid().ToString();
            const string data = "data";

            var logger = new LoggerMock<PubSubProviderImplementation>();
            var provider =
                new PubSubProviderImplementation(logger, new TestMessage(id, data)) as IPubSubProvider<TestMessage>;
            await provider.LogErrorAsync(new Exception("foo"), "bar");
            Assert.True(logger.HasErrors);
        }
    }
}
