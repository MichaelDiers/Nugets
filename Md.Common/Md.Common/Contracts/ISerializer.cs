namespace Md.Common.Contracts
{
    using System;
    using Newtonsoft.Json;

    /// <summary>
    ///     Handle <see cref="JsonConvert.SerializeObject(object?)" /> and
    ///     <see cref="JsonConvert.DeserializeObject{T}(string)" /> with predefined parameters.
    /// </summary>
    public interface ISerializer
    {
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
        T DeserializeObject<T>(string value);

        /// <summary>
        ///     Serialize an <see cref="object" /> to a <see cref="string" />.
        /// </summary>
        /// <param name="value">The value to be serialized.</param>
        /// <returns>An instance of <see cref="string" />.</returns>
        string SerializeObject(object value);
    }
}
