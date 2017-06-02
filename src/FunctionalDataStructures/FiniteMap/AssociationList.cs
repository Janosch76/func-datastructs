namespace FunctionalDataStructures.FiniteMap
{
    using System;
    using FunctionalDataStructures.List;

    /// <summary>
    /// Implementation of functional finite maps using association lists
    /// </summary>
    /// <typeparam name="TKey">The type of the key.</typeparam>
    /// <typeparam name="T">The element type</typeparam>
    public class AssociationList<TKey, T> : IFiniteMap<TKey, T>
        where TKey : IComparable<TKey>
    {
        /// <summary>
        /// The empty map.
        /// </summary>
        public static readonly AssociationList<TKey, T> Empty = new AssociationList<TKey, T>(List<Binding<TKey, T>>.Empty);

        private List<Binding<TKey, T>> associations;

        /// <summary>
        /// Initializes a new instance of the <see cref="AssociationList{TKey, T}"/> class.
        /// </summary>
        /// <param name="associations">The associations.</param>
        private AssociationList(List<Binding<TKey, T>> associations)
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
        /// Adds a new key-value binding to the association list.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <returns>
        /// A new association list extended with the given binding.
        /// </returns>
        public AssociationList<TKey, T> Bind(TKey key, T value)
        {
            var binding = new Binding<TKey, T>(key, value);
            return new AssociationList<TKey, T>(this.associations.Cons(binding));
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
    }
}
