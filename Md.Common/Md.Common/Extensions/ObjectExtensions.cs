namespace Md.Common.Extensions
{
    using System;
    using System.Linq;

    /// <summary>
    ///     Extensions for <see cref="object" />.
    /// </summary>
    public static class ObjectExtensions
    {
        /// <summary>
        ///     Parse a database object to an enum.
        /// </summary>
        /// <typeparam name="T">The type of the enum.</typeparam>
        /// <param name="value">The database value.</param>
        /// <returns>The value as an enum.</returns>
        /// <seealso cref="EnumExtensions.ToDatabase{T}" />
        public static T FromDatabaseToEnum<T>(this object value) where T : Enum
        {
            var stringValue = value is string s ? s : value.ToString();
            if (!string.IsNullOrWhiteSpace(stringValue) &&
                Enum.GetNames(typeof(T))
                    .Any(x => string.Equals(stringValue, x, StringComparison.InvariantCultureIgnoreCase)))
            {
                return (T) Enum.Parse(typeof(T), stringValue);
            }

            throw new ArgumentException("Cannot parse object to enum.", nameof(value));
        }
    }
}
