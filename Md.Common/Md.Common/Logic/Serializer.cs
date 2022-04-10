namespace Md.Common.Logic
{
    using System;
    using Md.Common.Contracts;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    using Newtonsoft.Json.Serialization;

    /// <summary>
    ///     Handle <see cref="JsonConvert.SerializeObject(object?)" /> and
    ///     <see cref="JsonConvert.DeserializeObject{T}(string)" /> with predefined parameters.
    /// </summary>
    public class Serializer : ISerializer
    {
        /// <summary>
        ///     The predefined parameters for <see cref="JsonConvert" />.
        /// </summary>
        private readonly JsonSerializerSettings settings;

        /// <summary>
        ///     Creates a new instance of <see cref="Serializer" />.
        /// </summary>
        public Serializer()
        {
            this.settings = new JsonSerializerSettings
            {
                Converters = {new StringEnumConverter(new DefaultNamingStrategy(), false)}
            };
        }

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
        public T DeserializeObject<T>(string value)
        {
            var deserialized = JsonConvert.DeserializeObject<T>(value, this.settings);
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
        public string SerializeObject(object value)
        {
            return JsonConvert.SerializeObject(value, this.settings);
        }
    }
}
