namespace Md.Common.Logic
{
    using System;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    using Newtonsoft.Json.Serialization;

    /// <summary>
    ///     Handle <see cref="JsonConvert.SerializeObject(object?)" /> and
    ///     <see cref="JsonConvert.DeserializeObject{T}(string)" /> with predefined parameters.
    /// </summary>
    public static class Serializer
    {
        /// <summary>
        ///     The predefined parameters for <see cref="JsonConvert" />.
        /// </summary>
        private static readonly JsonSerializerSettings settings = new JsonSerializerSettings
        {
            Converters = {new StringEnumConverter(new DefaultNamingStrategy(), false)}
        };


        /// <summary>
        ///     Deserialize a string to an object of type <typeparamref name="T" />.
        /// </summary>
        /// <typeparam name="T">The type of the resulting type.</typeparam>
        /// <param name="value">The value to be deserialized.</param>
        /// <returns>An instance of type <typeparamref name="T" />.</returns>
        /// <exception cref="ArgumentException">
        ///     Is thrown if <see cref="JsonConvert.DeserializeObject{T}(string)" /> results in a
        ///     null value.
        /// </exception>
        public static T DeserializeObject<T>(string value)
        {
            var deserialized = JsonConvert.DeserializeObject<T>(value, Serializer.settings);
            if (deserialized == null)
            {
                throw new ArgumentException($"Cannot deserialize {value}");
            }

            return deserialized;
        }

        /// <summary>
        ///     Serialize an <see cref="object" /> to a <see cref="string" />.
        /// </summary>
        /// <param name="value">The value to be serialized.</param>
        /// <returns>An instance of <see cref="string" />.</returns>
        public static string SerializeObject(object value)
        {
            return JsonConvert.SerializeObject(value, Serializer.settings);
        }
    }
}
