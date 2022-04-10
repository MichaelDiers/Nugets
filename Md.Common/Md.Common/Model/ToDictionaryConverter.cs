namespace Md.Common.Model
{
    using System.Collections.Generic;
    using Md.Common.Contracts.Model;

    /// <summary>
    ///     Implementation of <see cref="IToDictionary" />.
    /// </summary>
    public abstract class ToDictionaryConverter : IToDictionary
    {
        /// <summary>
        ///     Add the property values to a dictionary.
        /// </summary>
        /// <param name="dictionary">The values are added to the given dictionary.</param>
        /// <returns>The given <paramref name="dictionary" />.</returns>
        public abstract IDictionary<string, object> AddToDictionary(IDictionary<string, object> dictionary);

        /// <summary>
        ///     Create a dictionary from the object properties.
        /// </summary>
        /// <returns>A <see cref="IDictionary{TKey,TValue}" />.</returns>
        public IDictionary<string, object> ToDictionary()
        {
            return this.AddToDictionary(new Dictionary<string, object>());
        }
    }
}
