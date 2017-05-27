namespace FunctionalDataStructures.Queue
{
    using System;
    using FunctionalDataStructures.List;

    /// <summary>
    /// Purely functional queue implementation from Chris Okasaki´s book, p. 43ff
    /// </summary>
    /// <typeparam name="T">The element type</typeparam>
    public class BatchedQueue<T> : IQueue<T>
    {
        /// <summary>
        /// The empty set.
        /// </summary>
        public static readonly BatchedQueue<T> Empty = new BatchedQueue<T>(List<T>.Empty, List<T>.Empty);

        private List<T> front, rear;

        /// <summary>
        /// Initializes a new instance of the <see cref="BatchedQueue{T}"/> class.
        /// </summary>
        /// <param name="front">The first part of the queue</param>
        /// <param name="rear">The second part of the queue, reversed</param>
        private BatchedQueue(List<T> front, List<T> rear)
        {
            if (front.IsEmpty())
            {
                this.front = rear.Reverse();
                this.rear = List<T>.Empty;
            }
            else
            {
                this.front = front;
                this.rear = rear;
            }
        }

        /// <summary>
        /// Gets the number of elements in the queue.
        /// </summary>
        public int Count
        {
            get { return front.Count + rear.Count; }
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
        /// Returns the head element of the queue.
        /// </summary>
        /// <returns>
        /// The head element of the queue.
        /// </returns>
        public T Head()
        {
            if (this.front.IsEmpty())
            {
                throw new EmptyCollectionException();
            }

            return this.front.Head();
        }

        /// <summary>
        /// Returns the tail of the queue.
        /// </summary>
        /// <returns>
        /// The tail of the queue.
        /// </returns>
        public IQueue<T> Tail()
        {
            if (this.front.IsEmpty())
            {
                throw new EmptyCollectionException();
            }

            return new BatchedQueue<T>(this.front.Tail(), this.rear);
        }

        /// <summary>
        /// Appends the specified element to the queue.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <returns>
        /// The queue with the specified element at the end.
        /// </returns>
        public IQueue<T> Snoc(T element)
        {
            return new BatchedQueue<T>(this.front, this.rear.Cons(element));
        }
    }
}
