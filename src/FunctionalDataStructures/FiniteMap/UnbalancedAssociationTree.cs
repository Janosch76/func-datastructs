namespace FunctionalDataStructures.FiniteMap
{
    using System;
    using FunctionalDataStructures.Set;

    /// <summary>
    /// Implementation of functional finite maps using unbalanced binary search trees
    /// </summary>
    /// <typeparam name="TKey">The type of the key.</typeparam>
    /// <typeparam name="T">The element type</typeparam>
    public class UnbalancedAssociationTree<TKey, T> : IFiniteMap<TKey, T>
        where TKey : IComparable<TKey>
    { 
        /// <summary>
        /// The empty map.
        /// </summary>
        public static readonly UnbalancedAssociationTree<TKey, T> Empty = new UnbalancedAssociationTree<TKey, T>(UnbalancedSet<ComparableBinding>.Empty);

        private readonly UnbalancedSet<ComparableBinding> associations;

        /// <summary>
        /// Initializes a new instance of the <see cref="UnbalancedAssociationTree{TKey, T}"/> class.
        /// </summary>
        /// <param name="associations">The key-value bindings.</param>
        private UnbalancedAssociationTree(UnbalancedSet<ComparableBinding> associations)
        {
            this.associations = associations;
        }

        /// <summary>
        /// Gets the number of elements in the map.
        /// </summary>
        public int Count
        {
            get { return this.associations.Count; }
        }

        /// <summary>
        /// Determines whether this instance is empty.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if this instance is empty; otherwise, <c>false</c>.
        /// </returns>
        public bool IsEmpty()
        {
            return this.associations.IsEmpty();
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
        public UnbalancedAssociationTree<TKey, T> Bind(TKey key, T value)
        {
            var binding = new ComparableBinding(key, value);
            return new UnbalancedAssociationTree<TKey, T>(this.associations.Insert(binding));
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
            var binding = this.associations.Find(b => b.Key.CompareTo(key) == 0);
            return binding.Value;
        }

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through the collection.
        /// </returns>
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// An enumerator that can be used to iterate through the collection.
        /// </returns>
        public System.Collections.Generic.IEnumerator<Binding<TKey, T>> GetEnumerator()
        {
            return this.associations.GetEnumerator();
        }

        private class ComparableBinding : Binding<TKey, T>, IComparable<ComparableBinding>
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="ComparableBinding"/> class.
            /// </summary>
            /// <param name="key">The key.</param>
            /// <param name="value">The value.</param>
            public ComparableBinding(TKey key, T value)
                : base(key, value)
            {
            }

            /// <summary>
            /// Compares this instance to another instance.
            /// </summary>
            /// <param name="other">The other instance.</param>
            /// <returns>
            /// Returns a value indicating the relative order of this
            /// instance and the given other instance
            /// </returns>
            public int CompareTo(ComparableBinding other)
            {
                return Key.CompareTo(other.Key);
            }
        }
    }
}
