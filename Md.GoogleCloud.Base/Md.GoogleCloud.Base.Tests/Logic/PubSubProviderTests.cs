namespace Md.GoogleCloudPubSub.Base.Tests.Logic
{
    using System;
    using Md.GoogleCloudPubSub.Base.Contracts.Logic;
    using Md.GoogleCloudPubSub.Base.Logic;
    using Md.GoogleCloudPubSub.Base.Tests.Mocks;
    using Xunit;

    /// <summary>
    ///     Tests for <see cref="PubSubProvider{TMessage,TCategoryName}" />.
    /// </summary>
    public class PubSubProviderTests
    {
        [Fact]
        public void CtorThrowsArgumentNullExceptionIfLoggerIsNull()
        {
            Assert.Throws<ArgumentNullException>(
                () => new PubSubProviderImplementation(null, new TestMessage(Guid.NewGuid().ToString(), "data")));
        }

        [Fact]
        public async void HandleAsync()
        {
            var id = Guid.NewGuid().ToString();
            const string data = "data";

            var logger = new LoggerMock<PubSubProviderImplementation>();
            var provider = new PubSubProviderImplementation(logger, new TestMessage(id, data));
            await provider.HandleAsync(new TestMessage(id, data));
            Assert.False(logger.HasErrors);
        }

        [Fact]
        public async void HandleAsyncThrowsArgumentNullException()
        {
            var provider = new PubSubProviderImplementation(
                new LoggerMock<PubSubProviderImplementation>(),
                new TestMessage(Guid.NewGuid().ToString(), "data"));
            await Assert.ThrowsAnyAsync<ArgumentNullException>(() => provider.HandleAsync(null));
        }

        [Fact]
        public void ImplementsIPubSubProvider()
        {
            Assert.IsAssignableFrom<IPubSubProvider<TestMessage>>(
                new PubSubProviderImplementation(
                    new LoggerMock<PubSubProviderImplementation>(),
                    new TestMessage(Guid.NewGuid().ToString(), "data")));
        }
    }
}
