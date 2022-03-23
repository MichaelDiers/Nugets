namespace Md.Common.Tests.Extensions
{
    using System;
    using System.Text.RegularExpressions;
    using Md.Common.Extensions;
    using Xunit;

    public class EnumExtensionsTests
    {
        [Theory]
        [InlineData(RegexOptions.Compiled)]
        public void IsDefined(RegexOptions options)
        {
            Assert.Equal(options, EnumExtensions.IsDefined(options, "foo"));
        }

        [Theory]
        [InlineData((RegexOptions) 100)]
        public void IsDefinedThrowsException(RegexOptions options)
        {
            Assert.Throws<ArgumentException>(() => options.IsDefined("foo"));
        }
    }
}
