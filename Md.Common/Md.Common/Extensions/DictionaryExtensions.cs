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
            return dictionary.GetValue<bool>(key);
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
        ///     Parses the value to an <see cref="IEnumerable{T}" /> of <see cref="string" />.
        ///     Throws <see cref="ArgumentException" /> if a check fails.
        /// </summary>
        /// <param name="dictionary">The dictionary to checked for the key.</param>
        /// <param name="key">The search key.</param>
        /// <returns>The requested value.</returns>
        public static IEnumerable<string> GetEnumerableOfString(this IDictionary<string, object> dictionary, string key)
        {
            var value = dictionary.GetValue<object>(key);
            if (value is IEnumerable<object> enumerable)
            {
                return enumerable.Select(
                        x => x is string s
                            ? s
                            : throw new ArgumentException(
                                $"Value '{value}' is not an IEnumerable<string> for key '{key}'",
                                nameof(dictionary)))
                    .ToArray();
            }

            throw new ArgumentException(
                $"Value '{value}' is not an IEnumerable<string> for key '{key}'",
                nameof(dictionary));
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
            var value = dictionary.GetValue<object>(key);
            try
            {
                return value.FromDatabaseToEnum<T>();
            }
            catch (Exception e)
            {
                throw new ArgumentException($"Value for key {key} is not defined or null", e);
            }
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
            var value = dictionary.GetValue<object>(key);

            return value switch
            {
                int i => i,
                long _ => Convert.ToInt32(value),
                _ => throw new ArgumentException($"Invalid value {value} for key {key}")
            };
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
            var value = dictionary.GetValue<string>(key);
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
        ///     Parses the value to a string.
        ///     Throws <see cref="ArgumentException" /> if a check fails.
        /// </summary>
        /// <param name="dictionary">The dictionary to checked for the key.</param>
        /// <param name="key">The search key.</param>
        /// <param name="defaultValue">Return value if key is not in dictionary.</param>
        /// <returns>The requested string.</returns>
        public static string GetString(this IDictionary<string, object> dictionary, string key, string defaultValue)
        {
            return dictionary.GetValue(key, defaultValue);
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

        /// <summary>
        ///     Checks if <paramref name="key" /> is in <paramref name="dictionary" />.
        ///     Parses the value to type T.
        ///     Throws <see cref="ArgumentException" /> if a check fails.
        /// </summary>
        /// <param name="dictionary">The dictionary to checked for the key.</param>
        /// <param name="key">The search key.</param>
        /// <param name="defaultValue">Value is returned if key is not in dictionary.</param>
        /// <returns>The requested value.</returns>
        private static T GetValue<T>(this IDictionary<string, object> dictionary, string key, T defaultValue)
        {
            if (!dictionary.TryGetValue(key, out var value))
            {
                return defaultValue;
            }

            if (!(value is T s))
            {
                throw new ArgumentException($"Value '{value}' is not an T for key '{key}'", nameof(dictionary));
            }

            return s;
        }
    }
}
