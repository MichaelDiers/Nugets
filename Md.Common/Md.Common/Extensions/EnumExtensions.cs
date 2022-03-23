namespace Md.Common.Extensions
{
    using System;

    /// <summary>
    ///     Extensions for <see cref="Enum" />.
    /// </summary>
    public static class EnumExtensions
    {
        /// <summary>
        ///     Check if the value is defined.
        /// </summary>
        /// <typeparam name="T">The type of the enum.</typeparam>
        /// <param name="value">The value of the enum.</param>
        /// <param name="paramName">The name of the parameter.</param>
        /// <returns>The given <paramref name="value" /> if the member is defined. Otherwise an exception is thrown.</returns>
        public static T IsDefined<T>(this T value, string paramName) where T : Enum
        {
            if (Enum.IsDefined(typeof(T), value))
            {
                return value;
            }

            throw new ArgumentException($"Unknown enum member {value}", paramName);
        }

        /// <summary>
        ///     Convert an enum to its database value.
        /// </summary>
        /// <typeparam name="T">The type of the enum.</typeparam>
        /// <param name="value">The value to be converted.</param>
        /// <returns>The database representation of the value.</returns>
        /// <see cref="ObjectExtensions.FromDatabaseToEnumToEnum{T}" />
        public static object ToDatabase<T>(this T value) where T : Enum
        {
            return value.IsDefined(nameof(value)).ToString();
        }
    }
}
