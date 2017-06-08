namespace FunctionalDataStructures.Heap
{
    using System;
    using FunctionalDataStructures.List;
    using FunctionalDataStructures.RandomAccessList;
    using FunctionalDataStructures.Utils;

    /// <summary>
    /// Binomial heaps with lazy evaluation of heap list
    /// from Chris Okasaki's book, p.70 ff
    /// </summary>
    /// <typeparam name="T">The element type</typeparam>
    public class LazyBinomialHeap<T> : IHeap<LazyBinomialHeap<T>, T>
        where T : IComparable<T>
    {
        /// <summary>
        /// The empty heap.
        /// </summary>
        public static readonly LazyBinomialHeap<T> Empty = new LazyBinomialHeap<T>(
            new Susp<IList<Tree>>(() => BinaryRandomAccessList<Tree>.Empty),
            0);

        private ISusp<IList<Tree>> heap;
        private int count;

        /// <summary>
        /// Initializes a new instance of the <see cref="LazyBinomialHeap{T}"/> class.
        /// </summary>
        /// <param name="heap">The heap.</param>
        /// <param name="count">The number of elements in the heap.</param>
        private LazyBinomialHeap(ISusp<IList<Tree>> heap, int count)
        {
            this.heap = heap;
            this.count = count;
        }

        /// <summary>
        /// Gets the number of elements in the heap.
        /// </summary>
        public int Count
        {
            get { return this.count; }
        }

        /// <summary>
        /// Determines whether this instance is empty.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if this instance is empty; otherwise, <c>false</c>.
        /// </returns>
        public bool IsEmpty()
        {
            return this.count == 0;
        }

        /// <summary>
        /// Inserts the specified element into the heap.
        /// </summary>
        /// <param name="elem">The element to insert.</param>
        /// <returns>
        /// The updated heap.
        /// </returns>
        public LazyBinomialHeap<T> Insert(T elem)
        {
            return new LazyBinomialHeap<T>(
                this.heap.Select(trees => Insert(new Tree(0, elem, BinaryRandomAccessList<Tree>.Empty), trees)),
                this.count + 1);
        }

        /// <summary>
        /// Merges the specified other heap with this instance.
        /// </summary>
        /// <param name="other">The other heap.</param>
        /// <returns>
        /// The updated heap.
        /// </returns>
        public LazyBinomialHeap<T> Merge(LazyBinomialHeap<T> other)
        {
            return new LazyBinomialHeap<T>(
                new Susp<IList<Tree>>(() => Merge(this.heap.Force(), other.heap.Force())),
                this.count + other.count);
        }

        /// <summary>
        /// Finds the minimum element in the heap.
        /// </summary>
        /// <returns>
        /// The minimum element.
        /// </returns>
        public T FindMin()
        {
            var minTree = UnmergeMinTree(this.heap.Force()).Item1;
            return minTree.Root;
        }

        /// <summary>
        /// Deletes the minimum element from the heap.
        /// </summary>
        /// <returns>
        /// The updated heap.
        /// </returns>
        public LazyBinomialHeap<T> DeleteMin()
        {
            var p = UnmergeMinTree(this.heap.Force());
            return new LazyBinomialHeap<T>(
                new Susp<IList<Tree>>(() => Merge(Reverse(p.Item1.Subtrees), p.Item2)),
                this.count - 1);
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
        public System.Collections.Generic.IEnumerator<T> GetEnumerator()
        {
            return new HeapEnumerator<LazyBinomialHeap<T>, T>(this);
        }

        private static IList<S> Reverse<S>(IList<S> list)
        {
            return Reverse(list, BinaryRandomAccessList<S>.Empty);
        }

        private static IList<S> Reverse<S>(IList<S> list, IList<S> acc)
        {
            if (list.IsEmpty())
            {
                return acc;
            }
            else
            {
                return Reverse(list.Tail(), acc.Cons(list.Head()));
            }
        }

        private static IList<Tree> Insert(Tree tree, IList<Tree> trees)
        {
            if (trees.Count == 0)
            {
                return trees.Cons(tree);
            }
            else if (tree.Rank < trees.Head().Rank)
            {
                return trees.Cons(tree);
            }
            else
            {
                return Insert(Link(tree, trees.Head()), trees.Tail());
            }
        }

        private static Tree Link(Tree t1, Tree t2)
        {
            var rank = t1.Rank + 1;
            if (t1.Root.CompareTo(t2.Root) < 0)
            {
                return new Tree(rank, t1.Root, t1.Subtrees.Cons(t2));
            }
            else
            {
                return new Tree(rank, t2.Root, t2.Subtrees.Cons(t1));
            }
        }

        private static IList<Tree> Merge(IList<Tree> trees1, IList<Tree> trees2)
        {
            if (trees1.IsEmpty())
            {
                return trees2;
            }
            else if (trees2.IsEmpty())
            {
                return trees1;
            }
            else if (trees1.Head().Rank < trees2.Head().Rank)
            {
                return Merge(trees1.Tail(), trees2).Cons(trees1.Head());
            }
            else if (trees1.Head().Rank > trees2.Head().Rank)
            {
                return Merge(trees1, trees2.Tail()).Cons(trees2.Head());
            }
            else
            {
                return Insert(Link(trees1.Head(), trees2.Head()), Merge(trees1.Tail(), trees2.Tail()));
            }
        }

        private static Tuple<Tree, IList<Tree>> UnmergeMinTree(IList<Tree> trees)
        {
            if (trees.IsEmpty())
            {
                throw new EmptyCollectionException();
            }
            else if (trees.Count == 1)
            {
                IList<Tree> empty = BinaryRandomAccessList<Tree>.Empty;
                return Tuple.Create(trees.Head(), empty);
            }
            else
            {
                var headTree = trees.Head();
                var candidate = UnmergeMinTree(trees.Tail());
                if (headTree.Root.CompareTo(candidate.Item1.Root) < 0)
                {
                    return Tuple.Create(headTree, trees.Tail());
                }
                else
                {
                    return Tuple.Create(candidate.Item1, candidate.Item2.Cons(headTree));
                }
            }
        }

        private class Tree
        {
            public readonly int Rank;

            public readonly T Root;

            public readonly IList<Tree> Subtrees;

            public Tree(int rank, T root, IList<Tree> subtrees)
            {
                this.Rank = rank;
                this.Root = root;
                this.Subtrees = subtrees;
            }
        }
    }
}
