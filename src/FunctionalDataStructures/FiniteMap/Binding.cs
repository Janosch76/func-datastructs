namespace FunctionalDataStructures.FiniteMap
{
    using System;

    /// <summary>
    /// Represents a key-value binding
    /// </summary>
    /// <typeparam name="TKey">The type of the key.</typeparam>
    /// <typeparam name="T">The element type</typeparam>
    internal class Binding<TKey, T> : IComparable<Binding<TKey, T>>
        where TKey : IComparable<TKey>
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

        /// <summary>
        /// Compares this instance to another instance.
        /// </summary>
        /// <param name="other">The other instance.</param>
        /// <returns>
        /// Returns a value indicating the relative order of this
        /// instance and the given other instance
        /// </returns>
        public int CompareTo(Binding<TKey, T> other)
        {
            return Key.CompareTo(other.Key);
        }
    }
}
