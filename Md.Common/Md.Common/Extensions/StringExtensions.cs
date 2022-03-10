namespace Md.Common.Extensions
{
    using System;
    using System.Text.RegularExpressions;

    /// <summary>
    ///     Extensions for <see cref="string" />.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        ///     Check if the given string is a guid.
        /// </summary>
        /// <param name="s">The value to be checked.</param>
        /// <param name="paramName">Name of the validated parameter.</param>
        /// <returns>The <paramref name="s" /> or throws an exception if not a valid guid.</returns>
        public static string ValidateIsAGuid(this string s, string paramName)
        {
            if (string.IsNullOrWhiteSpace(s))
            {
                throw new ArgumentException("Value cannot be null or whitespace.", paramName);
            }

            if (Guid.TryParse(s, out var guid) && guid != Guid.Empty)
            {
                return s;
            }

            throw new ArgumentException($"Value {s} is not a valid guid.", paramName);
        }

        /// <summary>
        ///     Check if the given string is an email.
        /// </summary>
        /// <param name="s">The value to be checked.</param>
        /// <param name="paramName">Name of the validated parameter.</param>
        /// <returns>The <paramref name="s" /> or throws an exception if not a valid email.</returns>
        public static string ValidateIsAnEmail(this string s, string paramName)
        {
            if (string.IsNullOrWhiteSpace(s))
            {
                throw new ArgumentException("Value cannot be null or whitespace.", paramName);
            }

            if (Regex.IsMatch(s, ".+@.+[.].+"))
            {
                return s;
            }

            throw new ArgumentException($"Value {s} is not a valid email.", paramName);
        }

        /// <summary>
        ///     Check if the given string is not null or whitespace.
        /// </summary>
        /// <param name="s">The value to be checked.</param>
        /// <param name="paramName">Name of the validated parameter.</param>
        /// <returns>The <paramref name="s" /> or throws an exception if not a valid string.</returns>
        public static string ValidateIsNotNullOrWhitespace(this string s, string paramName)
        {
            if (string.IsNullOrWhiteSpace(s))
            {
                throw new ArgumentException("Value cannot be null or whitespace.", paramName);
            }

            return s;
        }
    }
}
