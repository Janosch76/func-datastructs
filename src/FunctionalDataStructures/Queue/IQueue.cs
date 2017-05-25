namespace FunctionalDataStructures.Queue
{
    /// <summary>
    /// Signature for queues
    /// </summary>
    /// <typeparam name="T">The element type.</typeparam>
    public interface IQueue<T>
    {
        /// <summary>
        /// Gets the number of elements in the queue.
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
        /// Appends the specified element to the queue.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <returns>
        ///   The queue with the specified element at the end.
        /// </returns>
        IQueue<T> Snoc(T element);

        /// <summary>
        /// Returns the head element of the queue.
        /// </summary>
        /// <returns>
        ///   The head element of the queue. 
        /// </returns>
        T Head();

        /// <summary>
        /// Returns the tail of the queue.
        /// </summary>
        /// <returns>
        ///   The tail of the queue.
        /// </returns>
        IQueue<T> Tail();
    }
}
