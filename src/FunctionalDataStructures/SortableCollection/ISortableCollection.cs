namespace FunctionalDataStructures.SortableCollection
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Signature for sortable collections
    /// </summary>
    /// <typeparam name="T">The element type</typeparam>
    public interface ISortableCollection<T> : IEnumerable<T>
        where T : IComparable<T>
    {
        /// <summary>
        /// Gets the number of elements in the collection.
        /// </summary>
        int Count { get; }

        /// <summary>
        /// Determines whether this instance is empty.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if this instance is empty; otherwise, <c>false</c>.
        /// </returns>
        bool IsEmpty();

        /// <summary>
        /// Adds the specified element to the collection.
        /// </summary>
        /// <param name="element">The element to add.</param>
        /// <returns>A new collection where the given element is added.</returns>
        ISortableCollection<T> Add(T element);

        /// <summary>
        /// Sorts this collection.
        /// </summary>
        /// <returns>An enumeration in ascending order</returns>
        IEnumerable<T> Sort();
    }
}
