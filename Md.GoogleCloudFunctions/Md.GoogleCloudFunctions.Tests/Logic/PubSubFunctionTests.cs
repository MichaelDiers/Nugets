namespace Md.GoogleCloudFunctions.Tests.Logic
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using CloudNative.CloudEvents;
    using Google.Cloud.Functions.Testing;
    using Google.Events.Protobuf.Cloud.PubSub.V1;
    using Md.GoogleCloudFunctions.Logic;
    using Microsoft.Extensions.Logging;
    using Newtonsoft.Json;
    using Xunit;

    /// <summary>
    ///     Tests for <see cref="PubSubFunction{TMessage,TIMessage,TCategoryName}" />.
    /// </summary>
    public class PubSubFunctionTests
    {
        [Fact]
        public void CtorThrowsArgumentNullExceptionIfLoggerIsNull()
        {
            Assert.Throws<ArgumentNullException>(
                () => new PubSubFunctionImplementation(
                    null,
                    new PubSubProviderImplementation(
                        new MemoryLogger<PubSubFunctionImplementation>(),
                        new TestMessage(Guid.NewGuid().ToString(), "data"))));
        }

        [Fact]
        public void CtorThrowsArgumentNullExceptionIfProviderIsNull()
        {
            Assert.Throws<ArgumentNullException>(
                () => new PubSubFunctionImplementation(new MemoryLogger<PubSubFunctionImplementation>(), null));
        }

        [Fact]
        public async void HandleAsync()
        {
            var id = Guid.NewGuid().ToString();
            const string data = "data";

            var logger = new MemoryLogger<PubSubFunctionImplementation>();
            var json = JsonConvert.SerializeObject(new TestMessage(id, data));

            await RunHandleAsync(
                logger,
                json,
                id,
                data);

            Assert.Empty(logger.ListLogEntries());
        }


        [Theory]
        [InlineData("", "Empty incoming json message (Parameter 'TextData')")]
        [InlineData(" ", "Empty incoming json message (Parameter 'TextData')")]
        [InlineData("{}", "Value cannot be null or whitespace. (Parameter 'processId')")]
        [InlineData("{processId:\"\"}", "Value cannot be null or whitespace. (Parameter 'processId')")]
        [InlineData("{processId:\"a\"}", "Value a is not a valid guid. (Parameter 'processId')")]
        [InlineData(
            "{processId:\"00000000-0000-0000-0000-000000000000\"}",
            "Value 00000000-0000-0000-0000-000000000000 is not a valid guid. (Parameter 'processId')")]
        [InlineData(
            "{processId:\"f7f7ea9d-96dd-4b26-8c30-91ebb3059eb4\"}",
            "Required property 'data' not found in JSON. Path '', line 1, position 50.")]
        [InlineData("{data:\"\"}", "Value cannot be null or whitespace. (Parameter 'processId')")]
        [InlineData("{data:\"data\"}", "Value cannot be null or whitespace. (Parameter 'processId')")]
        [InlineData("a", "Unexpected character encountered while parsing value: a. Path '', line 0, position 0.")]
        [InlineData("{foo:1}", "Value cannot be null or whitespace. (Parameter 'processId')")]
        public async void HandleAsyncLogsError(string json, string error)
        {
            var logger = new MemoryLogger<PubSubFunctionImplementation>();
            await RunHandleAsync(
                logger,
                json,
                Guid.NewGuid().ToString(),
                "data");

            Assert.Single(logger.ListLogEntries());
            Assert.Equal("Unexpected error!", logger.ListLogEntries().Single().Message);
            Assert.Equal(error, logger.ListLogEntries().Single().Exception?.Message);
        }

        private static async Task RunHandleAsync(
            ILogger<PubSubFunctionImplementation> logger,
            string json,
            string id,
            string data
        )
        {
            var provider = new PubSubProviderImplementation(logger, new TestMessage(id, data));

            var function = new PubSubFunctionImplementation(logger, provider);

            var raw = new MessagePublishedData {Message = new PubsubMessage {TextData = json}};

            var cloudEvent = new CloudEvent
            {
                Type = MessagePublishedData.MessagePublishedCloudEventType,
                Source = new Uri("//pubsub.googleapis.com", UriKind.RelativeOrAbsolute),
                Id = Guid.NewGuid().ToString(),
                Time = DateTimeOffset.UtcNow,
                Data = raw
            };

            await function.HandleAsync(cloudEvent, raw, CancellationToken.None);
        }
    }
}
