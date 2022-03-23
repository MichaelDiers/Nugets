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
        /// <returns>The given <paramref name="value" /> if the member is defined. Otherwise an exception is thrown.</returns>
        public static T IsDefined<T>(this T value) where T : Enum
        {
            if (Enum.IsDefined(typeof(T), value))
            {
                return value;
            }

            throw new ArgumentException($"Unknown enum member {value}", nameof(value));
        }
    }
}
