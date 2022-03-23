namespace Md.Common.Tests.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using Md.Common.Extensions;
    using Xunit;

    /// <summary>
    ///     Tests for <see cref="DictionaryExtensions" />.
    /// </summary>
    public class DictionaryExtensionsTests
    {
        [Theory]
        [InlineData("key", true)]
        [InlineData("key", false)]
        public void GetBool(string key, bool value)
        {
            var dictionary = new Dictionary<string, object> {{key, value}};
            var actual = dictionary.GetBool(key);
            Assert.Equal(value, actual);
        }

        [Theory]
        [InlineData("key", "true")]
        [InlineData("key", 10)]
        [InlineData("key", 10.11)]
        public void GetBoolThrowsExceptionForInvalidType(string key, object value)
        {
            var dictionary = new Dictionary<string, object> {{key, value}};
            Assert.Throws<ArgumentException>(() => dictionary.GetBool(key));
        }

        [Fact]
        public void GetBoolThrowsExceptionForMissingKey()
        {
            var dictionary = new Dictionary<string, object>();
            Assert.Throws<ArgumentException>(() => dictionary.GetBool("key"));
        }

        [Theory]
        [InlineData("key", 5)]
        public void GetDictionaries(string key, int count)
        {
            var dictionaries = new List<Dictionary<string, object>>();
            for (var i = 0; i < count; ++i)
            {
                dictionaries.Add(
                    new Dictionary<string, object>
                    {
                        {$"{key}_bool", true}, {$"{key}_int", 10}, {$"{key}_string", "value"}
                    });
            }

            var dictionary = new Dictionary<string, object> {{key, dictionaries}};

            var actual = dictionary.GetDictionaries(key);
            Assert.Equal(dictionaries.Count, actual.Count());
        }

        [Theory]
        [InlineData(
            "key",
            true,
            10,
            "foo")]
        public void GetDictionary(
            string key,
            bool boolValue,
            int intValue,
            string stringValue
        )
        {
            var dictionary = new Dictionary<string, object>
            {
                {
                    key,
                    new Dictionary<string, object>
                    {
                        {nameof(boolValue), boolValue},
                        {nameof(intValue), intValue},
                        {nameof(stringValue), stringValue}
                    }
                }
            };

            var actual = dictionary.GetDictionary(key);
            Assert.Equal(boolValue, actual.GetBool(nameof(boolValue)));
            Assert.Equal(intValue, actual.GetInt(nameof(intValue)));
            Assert.Equal(stringValue, actual.GetString(nameof(stringValue)));
        }

        [Theory]
        [InlineData("key", RegexOptions.Compiled)]
        [InlineData("key", RegexOptions.None)]
        public void GetEnumValue(string key, RegexOptions value)
        {
            var dictionary = new Dictionary<string, object> {{key, value.ToString()}};
            var actual = dictionary.GetEnumValue<RegexOptions>(key);
            Assert.Equal(value, actual);
        }

        [Theory]
        [InlineData("key", "0")]
        [InlineData("key", "abd")]
        [InlineData("key", 10.11)]
        [InlineData("key", true)]
        public void GetEnumValueThrowsExceptionForInvalidType(string key, object value)
        {
            var dictionary = new Dictionary<string, object> {{key, value}};
            Assert.Throws<ArgumentException>(() => dictionary.GetEnumValue<RegexOptions>(key));
        }

        [Fact]
        public void GetEnumValueThrowsExceptionForMissingKey()
        {
            var dictionary = new Dictionary<string, object>();
            Assert.Throws<ArgumentException>(() => dictionary.GetEnumValue<RegexOptions>("key"));
        }

        [Theory]
        [InlineData("key", 1000)]
        public void GetEnumValueThrowsExceptionForUndefined(string key, int value)
        {
            var dictionary = new Dictionary<string, object> {{key, (RegexOptions) value}};
            Assert.Throws<ArgumentException>(() => dictionary.GetEnumValue<RegexOptions>(key));
        }

        [Theory]
        [InlineData("key", 0)]
        [InlineData("key", 1)]
        public void GetInt(string key, int value)
        {
            var dictionary = new Dictionary<string, object> {{key, value}};
            var actual = dictionary.GetInt(key);
            Assert.Equal(value, actual);
        }

        [Theory]
        [InlineData("key", 0)]
        [InlineData("key", 1)]
        public void GetIntForLong(string key, long value)
        {
            var dictionary = new Dictionary<string, object> {{key, value}};
            var actual = dictionary.GetInt(key);
            Assert.Equal(value, actual);
        }

        [Theory]
        [InlineData("key", "0")]
        [InlineData("key", "abd")]
        [InlineData("key", 10.11)]
        [InlineData("key", true)]
        public void GetIntThrowsExceptionForInvalidType(string key, object value)
        {
            var dictionary = new Dictionary<string, object> {{key, value}};
            Assert.Throws<ArgumentException>(() => dictionary.GetInt(key));
        }

        [Fact]
        public void GetIntThrowsExceptionForMissingKey()
        {
            var dictionary = new Dictionary<string, object>();
            Assert.Throws<ArgumentException>(() => dictionary.GetInt("key"));
        }

        [Theory]
        [InlineData("key", "value")]
        public void GetString(string key, string value)
        {
            var dictionary = new Dictionary<string, object> {{key, value}};
            var actual = dictionary.GetString(key);
            Assert.Equal(value, actual);
        }

        [Theory]
        [InlineData("key", true)]
        [InlineData("key", 0)]
        [InlineData("key", 10.11)]
        public void GetStringThrowsExceptionForInvalidType(string key, object value)
        {
            var dictionary = new Dictionary<string, object> {{key, value}};
            Assert.Throws<ArgumentException>(() => dictionary.GetString(key));
        }

        [Fact]
        public void GetStringThrowsExceptionForMissingKey()
        {
            var dictionary = new Dictionary<string, object>();
            Assert.Throws<ArgumentException>(() => dictionary.GetString("key"));
        }

        [Theory]
        [InlineData("key", null)]
        [InlineData("key", "")]
        public void GetStringThrowsExceptionForNullOrWhitespace(string key, object value)
        {
            var dictionary = new Dictionary<string, object> {{key, value}};
            Assert.Throws<ArgumentException>(() => dictionary.GetString(key));
        }
    }
}
