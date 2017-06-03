namespace FunctionalDataStructures.FiniteMap
{
    using System;
    using FunctionalDataStructures.Utils;

    /// <summary>
    /// Trie implementation of functional finite maps over strings
    /// </summary>
    /// <typeparam name="T">The element type</typeparam>
    public class Trie<T> : IFiniteMap<string, T>
    {
        /// <summary>
        /// The empty trie.
        /// </summary>
        public static readonly Trie<T> Empty = new Trie<T>(Option<T>.None, AssociationList<char, Trie<T>>.Empty);

        private readonly Option<T> node;
        private readonly AssociationList<char, Trie<T>> edges;

        /// <summary>
        /// Initializes a new instance of the <see cref="Trie{T}"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="edges">The edge map.</param>
        private Trie(Option<T> value, AssociationList<char, Trie<T>> edges)
        {
            this.node = value;
            this.edges = edges;

            Count = (node.HasValue ? 1 : 0);
            foreach (var edge in edges)
            {
                var subtrie = edge.Value;
                Count += subtrie.Count;
            }
        }

        /// <summary>
        /// Gets the number of elements in the map.
        /// </summary>
        public int Count { get; private set; }

        /// <summary>
        /// Determines whether this instance is empty.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if this instance is empty; otherwise, <c>false</c>.
        /// </returns>
        public bool IsEmpty()
        {
            return Count == 0;
        }

        /// <summary>
        /// Lookup of the value for the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>
        /// The value associated with the given key
        /// </returns>
        public T Lookup(string key)
        {
            if (key == null)
            {
                throw new ArgumentNullException("key");
            }

            if (key == string.Empty && this.node.HasValue)
            {
                return this.node.Value;
            }
            else if (key == string.Empty && !this.node.HasValue)
            {
                throw new NotFoundException();
            }
            else
            {
                char k = Convert.ToChar(key.Substring(0, 1));
                string ks = key.Substring(1);
                return edges.Lookup(k).Lookup(ks);
            }
        }

        /// <summary>
        /// Adds a new key-value binding to the dictionary.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <returns>
        /// A dictionary extended with the given binding.
        /// </returns>
        IFiniteMap<string, T> IFiniteMap<string, T>.Bind(string key, T value)
        {
            return Bind(key, value);
        }

        /// <summary>
        /// Adds a new key-value binding to the trie.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <returns>
        /// A trie extended with the given binding.
        /// </returns>
        public Trie<T> Bind(string key, T value)
        {
            if (key == null)
            {
                throw new ArgumentNullException("key");
            }

            if (key == string.Empty)
            {
                return new Trie<T>(Option<T>.Some(value), this.edges);
            }
            else
            {
                char k = Convert.ToChar(key.Substring(0, 1));
                string ks = key.Substring(1);
                Trie<T> subtrie = TryGetSubtrie(k);
                Trie<T> updatedSubtrie = subtrie.Bind(ks, value);
                return new Trie<T>(this.node, this.edges.Bind(k, updatedSubtrie));
            }
        }

        private Trie<T> TryGetSubtrie(char k)
        {
            try
            {
                return this.edges.Lookup(k);
            }
            catch (NotFoundException)
            {
                return Empty;
            }
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
        public System.Collections.Generic.IEnumerator<Binding<string, T>> GetEnumerator()
        {
            if (this.node.HasValue)
            {
                yield return new Binding<string, T>(string.Empty, this.node.Value);
            }

            foreach (var edge in this.edges)
            {
                char k = edge.Key;
                var subtrie = edge.Value;
                foreach (var binding in subtrie)
                {
                    yield return new Binding<string, T>(k + binding.Key, binding.Value);
                }
            }
        }
    }
}