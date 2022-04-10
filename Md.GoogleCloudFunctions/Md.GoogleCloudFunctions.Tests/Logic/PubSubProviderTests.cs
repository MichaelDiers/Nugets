namespace Md.GoogleCloudFunctions.Tests.Logic
{
    using System;
    using Google.Cloud.Functions.Testing;
    using Md.GoogleCloudFunctions.Contracts.Logic;
    using Md.GoogleCloudFunctions.Logic;
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

            var logger = new MemoryLogger<PubSubFunctionImplementation>();
            var provider = new PubSubProviderImplementation(logger, new TestMessage(id, data));
            await provider.HandleAsync(new TestMessage(id, data));
            Assert.Empty(logger.ListLogEntries());
        }

        [Fact]
        public async void HandleAsyncThrowsArgumentNullException()
        {
            var provider = new PubSubProviderImplementation(
                new MemoryLogger<PubSubFunctionImplementation>(),
                new TestMessage(Guid.NewGuid().ToString(), "data"));
            await Assert.ThrowsAnyAsync<ArgumentNullException>(() => provider.HandleAsync(null));
        }

        [Fact]
        public void ImplementsIPubSubProvider()
        {
            Assert.IsAssignableFrom<IPubSubProvider<TestMessage>>(
                new PubSubProviderImplementation(
                    new MemoryLogger<PubSubFunctionImplementation>(),
                    new TestMessage(Guid.NewGuid().ToString(), "data")));
        }
    }
}
