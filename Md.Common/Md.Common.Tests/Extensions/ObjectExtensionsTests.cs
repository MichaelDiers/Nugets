namespace Md.Common.Tests.Extensions
{
    using System;
    using System.Text.RegularExpressions;
    using Md.Common.Extensions;
    using Xunit;

    public class ObjectExtensionsTests
    {
        [Theory]
        [InlineData(RegexOptions.Compiled)]
        public void FromDatabase(RegexOptions options)
        {
            var database = options.ToDatabase();
            Assert.Equal(options, database.FromDatabaseToEnum<RegexOptions>());
        }

        [Fact]
        public void FromDatabaseThrowsException()
        {
            object database = int.MaxValue.ToString();
            Assert.Throws<ArgumentException>(() => database.FromDatabaseToEnum<RegexOptions>());
        }
    }
}
