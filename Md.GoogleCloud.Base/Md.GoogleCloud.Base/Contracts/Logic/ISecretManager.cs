namespace Md.GoogleCloud.Base.Contracts.Logic
{
    /// <summary>
    ///     Access the google cloud secrets manager.
    /// </summary>
    public interface ISecretManager
    {
        /// <summary>
        ///     Read a secret by the given key.
        /// </summary>
        /// <param name="key">The key of the secret.</param>
        /// <returns>The value for the given key.</returns>
        string GetString(string key);
    }
}
