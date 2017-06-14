namespace FunctionalDataStructures.SortableCollection
{
    using System;
    using FunctionalDataStructures.List;
    using FunctionalDataStructures.Utils;

    /// <summary>
    /// Implementation of sortable collections using bottom-up mergesort with sharing
    /// from Chris Okasakis book, page 74ff
    /// </summary>
    /// <typeparam name="T">The element type</typeparam>
    public class BottomUpMergesort<T> : ISortableCollection<T>
        where T : IComparable<T>
    {
        /// <summary>
        /// The empty collection.
        /// </summary>
        public static readonly BottomUpMergesort<T> Empty = new BottomUpMergesort<T>(0, new Susp<List<List<T>>>(() => List<List<T>>.Empty));

        private readonly int size;
        private readonly Susp<List<List<T>>> segments;

        private BottomUpMergesort(int size, Susp<List<List<T>>> segments)
        {
            this.size = size;
            this.segments = segments;
        }

        /// <summary>
        /// Gets the number of elements in the collection.
        /// </summary>
        public int Count
        {
            get { return this.size; }
        }

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
        /// Adds the specified element to the collection.
        /// </summary>
        /// <param name="element">The element to add.</param>
        /// <returns>
        /// A new collection where the given element is added.
        /// </returns>
        public BottomUpMergesort<T> Add(T element)
        {
            throw new NotImplementedException();
        }

        public System.Collections.Generic.IEnumerable<T> Sort()
        {
            var sorted = MergeAll(this.segments.Force());
            return sorted;
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// An enumerator that can be used to iterate through the collection.
        /// </returns>
        public System.Collections.Generic.IEnumerator<T> GetEnumerator()
        {
            foreach (var segment in segments.Force())
            {
                foreach (var element in segment)
                {
                    yield return element;
                }
            }
        }

        /// <summary>
        /// Adds the specified element to the collection.
        /// </summary>
        /// <param name="element">The element to add.</param>
        /// <returns>
        /// A new collection where the given element is added.
        /// </returns>
        ISortableCollection<T> ISortableCollection<T>.Add(T element)
        {
            return Add(element);
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

        private static List<T> MergeAll(List<List<T>> segments)
        {
            return MergeAll(List<T>.Empty, segments);
        }

        private static List<T> MergeAll(List<T> merged, List<List<T>> segments)
        {
            if (segments.IsEmpty())
            {
                return merged;
            }

            var segment = segments.Head();
            var remainingSegments = segments.Tail();
            return MergeAll(Merge(merged, segment), remainingSegments);
        }

        private static List<T> Merge(List<T> xs, List<T> ys)
        {
            if (xs.IsEmpty())
            {
                return ys;
            }

            if (ys.IsEmpty())
            {
                return xs;
            }

            if (xs.Head().CompareTo(ys.Head()) < 0)
            {
                var merged = Merge(xs.Tail(), ys);
                return merged.Cons(xs.Head());
            }
            else
            {
                var merged = Merge(xs, ys.Tail());
                return merged.Cons(ys.Head());
            }
        }
    }
}
