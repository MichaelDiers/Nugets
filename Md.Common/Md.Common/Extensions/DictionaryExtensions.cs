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
        ///     Parses the value to a string.
        ///     Throws <see cref="ArgumentException" /> if a check fails.
        /// </summary>
        /// <param name="dictionary">The dictionary to checked for the key.</param>
        /// <param name="key">The search key.</param>
        /// <returns>The requested string.</returns>
        public static string GetString(this IDictionary<string, object> dictionary, string key)
        {
            if (!dictionary.TryGetValue(key, out var value))
            {
                throw new ArgumentException($"Missing key '{key}' in dictionary", nameof(dictionary));
            }

            if (!(value is string s) || string.IsNullOrWhiteSpace(s))
            {
                throw new ArgumentException(
                    $"Value '{value}' is not a string for key '{key}' or empty in dictionary",
                    nameof(dictionary));
            }

            return s;
        }
    }
}
