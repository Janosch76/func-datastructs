namespace FunctionalDataStructures.FiniteMap
{
    using System;

    /// <summary>
    /// Represents a key-value binding
    /// </summary>
    /// <typeparam name="TKey">The type of the key.</typeparam>
    /// <typeparam name="T">The element type</typeparam>
    public class Binding<TKey, T> 
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Binding{TKey, T}"/> class.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        public Binding(TKey key, T value)
        {
            this.Key = key;
            this.Value = value;
        }

        /// <summary>
        /// Gets the key.
        /// </summary>
        public TKey Key { get; private set; }

        /// <summary>
        /// Gets the value.
        /// </summary>
        public T Value { get; private set; }
    }
}
