namespace FunctionalDataStructures.Queue
{
    using System;
    using FunctionalDataStructures.Utils;

    /// <summary>
    /// Amortized queue using the Bankers Method, from Chris Okasaki´s book, p. 64ff
    /// </summary>
    /// <typeparam name="T">The element type</typeparam>
    public class BankersQueue<T> : IQueue<T>
    {
        /// <summary>
        /// The empty queue.
        /// </summary>
        public static readonly BankersQueue<T> Empty = new BankersQueue<T>(0, Stream<T>.Nil, 0, Stream<T>.Nil);

        private Stream<T> front, rear;
        private int frontLength, rearLength;

        private BankersQueue(int frontLength, Stream<T> front, int rearLength, Stream<T> rear)
        {
            if (frontLength < rearLength)
            {
                this.frontLength = frontLength + rearLength;
                this.front = front.Append(rear.Reverse());
                this.rearLength = 0;
                this.rear = Stream<T>.Nil;
            }
            else
            {
                this.frontLength = frontLength;
                this.front = front;
                this.rearLength = rearLength;
                this.rear = rear;
            }
        }

        /// <summary>
        /// Gets the number of elements in the queue.
        /// </summary>
        public int Count
        {
            get { return frontLength + rearLength; }
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

            var p = this.front.Uncons();
            return p.Item1;
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

            var p = this.front.Uncons();
            return new BankersQueue<T>(
                this.frontLength - 1,
                p.Item2, 
                this.rearLength,
                this.rear);
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
            return new BankersQueue<T>(
                this.frontLength, 
                this.front, 
                this.rearLength + 1, 
                this.rear.Cons(element));
        }
    }
}
