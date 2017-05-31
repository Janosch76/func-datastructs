namespace FunctionalDataStructures.FiniteMap
{
    /// <summary>
    /// Signature for persistent dictionaries
    /// </summary>
    /// <typeparam name="TKey">The key type</typeparam>
    /// <typeparam name="T">The element type</typeparam>
    public interface IFiniteMap<TKey, T>
    {
        /// <summary>
        /// Adds a new key-value binding to the dictionary.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <returns>A dictionary extended with the given binding.</returns>
        IFiniteMap<TKey, T> Bind(TKey key, T value);

        /// <summary>
        /// Lookup of the value for the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>The value associated with the given key</returns>
        /// <exception cref="NotFoundException">Thrown when the given key is not present in the dictionary.</exception>
        T Lookup(TKey key);
    }
}
