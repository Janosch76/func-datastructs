namespace FunctionalDataStructures.FiniteMap
{
    using System;
    using FunctionalDataStructures.Set;

    /// <summary>
    /// Implementation of functional finite maps using unbalanced binary search trees
    /// </summary>
    /// <typeparam name="TKey">The type of the key.</typeparam>
    /// <typeparam name="T">The element type</typeparam>
    public class UnbalancedSearchTreeFiniteMap<TKey, T> : IFiniteMap<TKey, T>
        where TKey : IComparable<TKey>
    {
        /// <summary>
        /// The empty map.
        /// </summary>
        public static readonly UnbalancedSearchTreeFiniteMap<TKey, T> Empty = new UnbalancedSearchTreeFiniteMap<TKey, T>(UnbalancedSet<Binding>.Empty);

        private readonly UnbalancedSet<Binding> bindings;

        /// <summary>
        /// Initializes a new instance of the <see cref="UnbalancedSearchTreeFiniteMap{TKey, T}"/> class.
        /// </summary>
        /// <param name="bindings">The key-value bindings.</param>
        private UnbalancedSearchTreeFiniteMap(UnbalancedSet<Binding> bindings)
        {
            this.bindings = bindings;
        }

        /// <summary>
        /// Adds a new key-value binding to the dictionary.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <returns>
        /// A dictionary extended with the given binding.
        /// </returns>
        IFiniteMap<TKey, T> IFiniteMap<TKey, T>.Bind(TKey key, T value)
        {
            return Bind(key, value);
        }

        /// <summary>
        /// Adds a new key-value binding to the dictionary.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <returns>
        /// A new instance extended with the given binding.
        /// </returns>
        public UnbalancedSearchTreeFiniteMap<TKey, T> Bind(TKey key, T value)
        {
            var updatedBindings = this.bindings.Insert(new Binding(key, value));
            return new UnbalancedSearchTreeFiniteMap<TKey, T>(updatedBindings);
        }

        /// <summary>
        /// Lookup of the value for the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>
        /// The value associated with the given key
        /// </returns>
        public T Lookup(TKey key)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Represents a key-value binding
        /// </summary>
        private class Binding : IComparable<Binding>
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="Binding"/> class.
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
            public int CompareTo(Binding other)
            {
                return Key.CompareTo(other.Key);
            }
        }
    }
}
