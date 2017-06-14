namespace FunctionalDataStructures.Queue
{
    /// <summary>
    /// Signature for double-ended queues
    /// </summary>
    /// <typeparam name="T">The element type.</typeparam>
    public interface IDequeue<T> : IQueue<T>
    {

        /// <summary>
        /// Appends the specified element to the queue.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <returns>
        ///   The queue with the specified element at the end.
        /// </returns>
        new IDequeue<T> Snoc(T element);

        /// <summary>
        /// Prepends the specified element to the dequeue.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <returns>
        ///   The dequeue with the specified element at the front.
        /// </returns>
        IDequeue<T> Cons(T element);

        /// <summary>
        /// Returns the last element of the dequeue.
        /// </summary>
        /// <returns>
        ///   The last element of the dequeue. 
        /// </returns>
        T Last();

        /// <summary>
        /// Returns the tail of the dequeue.
        /// </summary>
        /// <returns>
        ///   The tail of the dequeue.
        /// </returns>
        new IDequeue<T> Tail();

        /// <summary>
        /// Returns the front of the dequeue, without the last element.
        /// </summary>
        /// <returns>
        ///   The front of the dequeue.
        /// </returns>
        IDequeue<T> Init();
    }
}
