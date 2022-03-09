namespace Md.Common.Tests.Extensions
{
    using System;
    using Md.Common.Extensions;
    using Xunit;

    /// <summary>
    ///     Tests for <see cref="StringExtensions" />.
    /// </summary>
    public class StringExtensionsTests
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("invalid")]
        [InlineData("00000000-0000-0000-0000-000000000000")]
        public void GuidIsInvalid(string guid)
        {
            Assert.Throws<ArgumentException>(guid.ValidateIsAGuid);
        }

        [Fact]
        public void GuidIsValid()
        {
            var guid = Guid.NewGuid().ToString();
            Assert.Equal(guid, guid.ValidateIsAGuid());
        }
    }
}
