namespace Md.Common.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    ///     Extensions for dictionaries.
    /// </summary>
    public static class DictionaryExtensions
    {
        /// <summary>
        ///     Checks if <paramref name="key" /> is in <paramref name="dictionary" />.
        ///     Parses the value to an bool.
        ///     Throws <see cref="ArgumentException" /> if a check fails.
        /// </summary>
        /// <param name="dictionary">The dictionary to checked for the key.</param>
        /// <param name="key">The search key.</param>
        /// <returns>The requested bool.</returns>
        public static bool GetBool(this IDictionary<string, object> dictionary, string key)
        {
            return GetValue<bool>(dictionary, key);
        }

        /// <summary>
        ///     Checks if <paramref name="key" /> is in <paramref name="dictionary" />.
        ///     Parses the value to an IDictionary{string, object}.
        ///     Throws <see cref="ArgumentException" /> if a check fails.
        /// </summary>
        /// <param name="dictionary">The dictionary to checked for the key.</param>
        /// <param name="key">The search key.</param>
        /// <returns>The requested dictionary.</returns>
        public static IEnumerable<IDictionary<string, object>> GetDictionaries(
            this IDictionary<string, object> dictionary,
            string key
        )
        {
            if (!dictionary.TryGetValue(key, out var valueObject))
            {
                throw new ArgumentException($"Missing key '{key}' in dictionary", nameof(dictionary));
            }

            if (!(valueObject is IEnumerable<object> valueEnumerable))
            {
                throw new ArgumentException(
                    $"Value '{valueObject}' is not an IEnumerable<object> for key '{key}' or empty in dictionary",
                    nameof(dictionary));
            }

            foreach (var entry in valueEnumerable)
            {
                if (!(entry is IDictionary<string, object> parsedEntry))
                {
                    throw new ArgumentException(
                        "For key '{key}' there exists an entry in the enumerable that is not a IDictionary{string, object}.",
                        nameof(dictionary));
                }

                yield return parsedEntry;
            }
        }


        /// <summary>
        ///     Checks if <paramref name="key" /> is in <paramref name="dictionary" />.
        ///     Parses the value to an IDictionary{string, object}.
        ///     Throws <see cref="ArgumentException" /> if a check fails.
        /// </summary>
        /// <param name="dictionary">The dictionary to checked for the key.</param>
        /// <param name="key">The search key.</param>
        /// <returns>The requested dictionary.</returns>
        public static IDictionary<string, object> GetDictionary(this IDictionary<string, object> dictionary, string key)
        {
            if (!dictionary.TryGetValue(key, out var value))
            {
                throw new ArgumentException($"Missing key '{key}' in dictionary", nameof(dictionary));
            }

            if (!(value is IDictionary<string, object> d) || !d.Any())
            {
                throw new ArgumentException(
                    $"Value '{value}' is not a dictionary for key '{key}' or empty in dictionary",
                    nameof(dictionary));
            }

            return d;
        }

        /// <summary>
        ///     Checks if <paramref name="key" /> is in <paramref name="dictionary" />.
        ///     Parses the value to an enum value.
        ///     Throws <see cref="ArgumentException" /> if a check fails.
        /// </summary>
        /// <param name="dictionary">The dictionary to checked for the key.</param>
        /// <param name="key">The search key.</param>
        /// <returns>The requested enum value.</returns>
        public static T GetEnumValue<T>(this IDictionary<string, object> dictionary, string key) where T : Enum
        {
            var value = GetString(dictionary, key);
            var enumValue = (T) Enum.Parse(typeof(T), value, true);

            if (value == null || !Enum.IsDefined(typeof(T), value))
            {
                throw new ArgumentException($"Value for key {key} is not defined or null");
            }

            return enumValue;
        }

        /// <summary>
        ///     Checks if <paramref name="key" /> is in <paramref name="dictionary" />.
        ///     Parses the value to an int.
        ///     Throws <see cref="ArgumentException" /> if a check fails.
        /// </summary>
        /// <param name="dictionary">The dictionary to checked for the key.</param>
        /// <param name="key">The search key.</param>
        /// <returns>The requested int.</returns>
        public static int GetInt(this IDictionary<string, object> dictionary, string key)
        {
            var value = GetValue<object>(dictionary, key);

            if (value is int i)
            {
                return i;
            }

            if (value is long)
            {
                return Convert.ToInt32(value);
            }

            throw new ArgumentException($"Invalid value {value} for key {key}");
        }

        /// <summary>
        ///     Checks if <paramref name="key" /> is in <paramref name="dictionary" />.
        ///     Parses the value to a string.
        ///     Throws <see cref="ArgumentException" /> if a check fails.
        /// </summary>
        /// <param name="dictionary">The dictionary to checked for the key.</param>
        /// <param name="key">The search key.</param>
        /// <returns>The requested string.</returns>
        public static string GetString(this IDictionary<string, object> dictionary, string key)
        {
            var value = GetValue<string>(dictionary, key);
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException(
                    $"Value '{value}' is not a string for key '{key}' or empty in dictionary",
                    nameof(dictionary));
            }

            return value;
        }

        /// <summary>
        ///     Checks if <paramref name="key" /> is in <paramref name="dictionary" />.
        ///     Parses the value to type T.
        ///     Throws <see cref="ArgumentException" /> if a check fails.
        /// </summary>
        /// <param name="dictionary">The dictionary to checked for the key.</param>
        /// <param name="key">The search key.</param>
        /// <returns>The requested value.</returns>
        public static T GetValue<T>(this IDictionary<string, object> dictionary, string key)
        {
            if (!dictionary.TryGetValue(key, out var value))
            {
                throw new ArgumentException($"Missing key '{key}' in dictionary", nameof(dictionary));
            }

            if (!(value is T s))
            {
                throw new ArgumentException($"Value '{value}' is not an T for key '{key}'", nameof(dictionary));
            }

            return s;
        }
    }
}
