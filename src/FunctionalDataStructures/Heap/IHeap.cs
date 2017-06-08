namespace FunctionalDataStructures.Heap
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Signature for heaps (priority queues)
    /// </summary>
    /// <typeparam name="THeap">The type of the heap.</typeparam>
    /// <typeparam name="T">The element type.</typeparam>
    public interface IHeap<THeap, T> : IEnumerable<T>
        where THeap : IHeap<THeap, T> 
        where T : IComparable<T>
    {
        /// <summary>
        /// Gets the number of elements in the heap.
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
        /// Inserts the specified element into the heap.
        /// </summary>
        /// <param name="elem">The element to insert.</param>
        /// <returns>
        ///   The updated heap. 
        /// </returns>
        THeap Insert(T elem);

        /// <summary>
        /// Merges the heap with a specified heap.
        /// </summary>
        /// <param name="heap">The heap to merge with the current instance.</param>
        /// <returns>
        ///   The merged heap.
        /// </returns>
        THeap Merge(THeap heap);

        /// <summary>
        /// Finds the minimum element in the heap.
        /// </summary>
        /// <returns>
        ///   The minimu element.
        /// </returns>
        T FindMin();

        /// <summary>
        /// Deletes the minimum element from the heap.
        /// </summary>
        /// <returns>
        ///   The updated heap.
        /// </returns>
        THeap DeleteMin();
    }
}
