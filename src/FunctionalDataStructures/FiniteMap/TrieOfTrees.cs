namespace FunctionalDataStructures.FiniteMap
{
    using System;
    using FunctionalDataStructures.Utils;

    /// <summary>
    /// Trie implementation of functional finite maps over tree-structured keys
    /// </summary>
    /// <typeparam name="TKey">The key element type</typeparam>
    /// <typeparam name="T">The element type</typeparam>
    public class TrieOfTrees<TKey, T> : IFiniteMap<BinaryTree<TKey>, T>
        where TKey : IComparable<TKey>
    {
        /// <summary>
        /// The empty trie.
        /// </summary>
        public static readonly TrieOfTrees<TKey, T> Empty = new TrieOfTrees<TKey, T>(Option<T>.None, AssociationList<TKey, TrieOfTrees<TKey, TrieOfTrees<TKey, T>>>.Empty);

        private readonly Option<T> node;
        private readonly AssociationList<TKey, TrieOfTrees<TKey, TrieOfTrees<TKey, T>>> edges;

        /// <summary>
        /// Initializes a new instance of the <see cref="TrieOfTrees{TKey, T}"/> class.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <param name="edges">The edge map.</param>
        private TrieOfTrees(Option<T> node, AssociationList<TKey, TrieOfTrees<TKey, TrieOfTrees<TKey, T>>> edges)
        {
            this.node = node;
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
        public T Lookup(BinaryTree<TKey> key)
        {
            if (key == null)
            {
                throw new ArgumentNullException("key");
            }

            if (key.IsEmpty() && this.node.HasValue)
            {
                return this.node.Value;
            }
            else if (key.IsEmpty() && !this.node.HasValue)
            {
                throw new NotFoundException();
            }
            else
            {
                TKey k = key.Element;
                BinaryTree<TKey> left = key.Left;
                BinaryTree<TKey> right = key.Right;
                return edges.Lookup(k).Lookup(left).Lookup(right);
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
        IFiniteMap<BinaryTree<TKey>, T> IFiniteMap<BinaryTree<TKey>, T>.Bind(BinaryTree<TKey> key, T value)
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
        public TrieOfTrees<TKey, T> Bind(BinaryTree<TKey> key, T value)
        {
            if (key == null)
            {
                throw new ArgumentNullException("key");
            }

            if (key.IsEmpty())
            {
                return new TrieOfTrees<TKey, T>(Option<T>.Some(value), this.edges);
            }
            else
            {
                TKey k = key.Element;
                BinaryTree<TKey> left = key.Left;
                BinaryTree<TKey> right = key.Right;

                var subtrie1 = TryGetSubtrie(k);
                var subtrie2 = TryGetSubtrie(subtrie1, left);
                var updatedSubtrie1 = subtrie2.Bind(right, value);
                var updatedSubtrie2 = subtrie1.Bind(left, updatedSubtrie1);
                return new TrieOfTrees<TKey, T>(this.node, this.edges.Bind(k, updatedSubtrie2));
            }
        }

        private TrieOfTrees<TKey, T> TryGetSubtrie(TrieOfTrees<TKey, TrieOfTrees<TKey, T>> t, BinaryTree<TKey> left)
        {
            try
            {
                return t.Lookup(left);
            }
            catch (NotFoundException)
            {
                return TrieOfTrees<TKey, T>.Empty;
            }
        }

        private TrieOfTrees<TKey, TrieOfTrees<TKey, T>> TryGetSubtrie(TKey k)
        {
            try
            {
                return this.edges.Lookup(k);
            }
            catch (NotFoundException)
            {
                return TrieOfTrees<TKey, TrieOfTrees<TKey, T>>.Empty;
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
        public System.Collections.Generic.IEnumerator<Binding<BinaryTree<TKey>, T>> GetEnumerator()
        {
            if (this.node.HasValue)
            {
                yield return new Binding<BinaryTree<TKey>, T>(BinaryTree<TKey>.Empty, this.node.Value);
            }

            foreach (var edge in this.edges)
            {
                TKey k = edge.Key;
                foreach (var subtrie1 in edge.Value)
                {
                    foreach (var subtrie2 in subtrie1.Value)
                    {
                        yield return new Binding<BinaryTree<TKey>, T>(
                            BinaryTree<TKey>.MakeTree(k, subtrie1.Key, subtrie2.Key),
                            subtrie2.Value);
                    }
                }
            }
        }
    }
}
