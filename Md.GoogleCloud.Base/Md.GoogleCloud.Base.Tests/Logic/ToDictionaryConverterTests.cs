namespace Md.GoogleCloud.Base.Tests.Logic
{
    using System.Collections.Generic;
    using Md.GoogleCloud.Base.Contracts.Logic;
    using Md.GoogleCloud.Base.Logic;
    using Xunit;

    public class ToDictionaryConverterTests : ToDictionaryConverter
    {
        /// <summary>
        ///     Add the property values to a dictionary.
        /// </summary>
        /// <param name="dictionary">The values are added to the given dictionary.</param>
        /// <returns>The given <paramref name="dictionary" />.</returns>
        public override IDictionary<string, object> AddToDictionary(IDictionary<string, object> dictionary)
        {
            dictionary.Add("foo", "bar");
            return dictionary;
        }

        [Fact]
        public void AddToDictionaryTest()
        {
            var dictionary = new Dictionary<string, object>();
            var obj = new ToDictionaryConverterTests() as IToDictionary;
            var actual = obj.AddToDictionary(dictionary);
            Assert.StrictEqual(dictionary, actual);
            Assert.Single(actual);
            Assert.Equal("bar", actual["foo"] as string);
        }

        [Fact]
        public void ToDictionaryTest()
        {
            var obj = new ToDictionaryConverterTests() as IToDictionary;
            var actual = obj.ToDictionary();
            Assert.NotNull(actual);
            Assert.Single(actual);
            Assert.Equal("bar", actual["foo"] as string);
        }
    }
}
