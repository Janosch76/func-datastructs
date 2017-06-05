namespace FunctionalDataStructures.Queue
{
    /// <summary>
    /// Signature for double-ended queues
    /// </summary>
    /// <typeparam name="T">The element type.</typeparam>
    public interface IDequeue<T> : IQueue<T>
    {
        /// <summary>
        /// Prepends the specified element to the queue.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <returns>
        ///   The queue with the specified element at the front.
        /// </returns>
        IQueue<T> Cons(T element);

        /// <summary>
        /// Returns the last element of the queue.
        /// </summary>
        /// <returns>
        ///   The last element of the queue. 
        /// </returns>
        T Last();

        /// <summary>
        /// Returns the front of the queue, without the last element.
        /// </summary>
        /// <returns>
        ///   The front of the queue.
        /// </returns>
        IQueue<T> Init();
    }
}
