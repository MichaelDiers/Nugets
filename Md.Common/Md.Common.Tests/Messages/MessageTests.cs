namespace Md.Common.Tests.Messages
{
    using System;
    using Md.Common.Contracts.Messages;
    using Md.Common.Messages;
    using Xunit;

    /// <summary>
    ///     Tests for <see cref="Message" />
    /// </summary>
    public class MessageTests
    {
        [Fact]
        public void Ctor()
        {
            const string processId = "f7f7ea9d-96dd-4b26-8c30-91ebb3059eb4";
            var message = new Message(processId);
            Assert.Equal(processId, message.ProcessId);

            var iMessage = message as IMessage;
            Assert.Equal(processId, iMessage.ProcessId);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("00000000-0000-0000-0000-000000000000")]
        [InlineData("not a guid")]
        public void CtorThrowsArgumentException(string processId)
        {
            Assert.Throws<ArgumentException>(() => new Message(processId));
        }

        [Fact]
        public void MessageImplementsIMessage()
        {
            Assert.IsAssignableFrom<IMessage>(new Message(Guid.NewGuid().ToString()));
        }
    }
}
