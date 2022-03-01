namespace Md.GoogleCloud.Base.Contracts.Logic
{
    using System.Collections.Generic;

    /// <summary>
    ///     Provide methods for generating a dictionary from property values.
    /// </summary>
    public interface IToDictionary
    {
        /// <summary>
        ///     Add the property values to a dictionary.
        /// </summary>
        /// <param name="dictionary">The values are added to the given dictionary.</param>
        /// <returns>The given <paramref name="dictionary" />.</returns>
        IDictionary<string, object> AddToDictionary(IDictionary<string, object> dictionary);

        /// <summary>
        ///     Create a dictionary from the object properties.
        /// </summary>
        /// <returns>A <see cref="IDictionary{TKey,TValue}" />.</returns>
        IDictionary<string, object> ToDictionary();
    }
}
