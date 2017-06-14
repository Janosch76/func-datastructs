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
        /// The empty queue.
        /// </summary>
        public static readonly BatchedQueue<T> Empty = new BatchedQueue<T>(List<T>.Empty, List<T>.Empty);

        private readonly List<T> front, rear;

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
        public BatchedQueue<T> Tail()
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
        public BatchedQueue<T> Snoc(T element)
        {
            return new BatchedQueue<T>(this.front, this.rear.Cons(element));
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// An enumerator that can be used to iterate through the collection.
        /// </returns>
        public System.Collections.Generic.IEnumerator<T> GetEnumerator()
        {
            return new QueueEnumerator<T>(this);
        }

        /// <summary>
        /// Returns the tail of the queue.
        /// </summary>
        /// <returns>
        /// The tail of the queue.
        /// </returns>
        IQueue<T> IQueue<T>.Tail()
        {
            return Tail();
        }

        /// <summary>
        /// Appends the specified element to the queue.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <returns>
        /// The queue with the specified element at the end.
        /// </returns>
        IQueue<T> IQueue<T>.Snoc(T element)
        {
            return Snoc(element);
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
    }
}
