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
        [InlineData("@bar.example")]
        [InlineData("foo-bar2345@")]
        [InlineData("foo-bar2345@bar")]
        public void EmailIsInvalid(string email)
        {
            Assert.Throws<ArgumentException>(email.ValidateIsAnEmail);
        }

        [Theory]
        [InlineData("foo@bar.de")]
        [InlineData("foo@bar.co.uk")]
        [InlineData("foo-bar@bar.example")]
        [InlineData("foo-bar2345@bar.example")]
        public void EmailIsValid(string email)
        {
            Assert.Equal(email, email.ValidateIsAnEmail());
        }

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

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void StringIsInvalid(string s)
        {
            Assert.Throws<ArgumentException>(s.ValidateIsNotNullOrWhitespace);
        }

        [Fact]
        public void StringIsValid()
        {
            const string s = "valid";
            Assert.Equal(s, s.ValidateIsNotNullOrWhitespace());
        }
    }
}
