namespace FunctionalDataStructures.Queue
{
    using System;
    using FunctionalDataStructures.List;
    using FunctionalDataStructures.Utils;

    /// <summary>
    /// Amortized queue using the Bankers Method, from Chris Okasaki´s book, p. 64ff
    /// </summary>
    /// <typeparam name="T">The element type</typeparam>
    public class PhysicistsQueue<T> : IQueue<T>
    {
        /// <summary>
        /// The empty queue.
        /// </summary>
        public static readonly PhysicistsQueue<T> Empty = new PhysicistsQueue<T>(List<T>.Empty, 0, new Susp<List<T>>(() => List<T>.Empty), 0, List<T>.Empty);

        private List<T> frontWorkingPrefix;
        private Susp<List<T>> front;
        private List<T> rear;
        private int frontLength, rearLength;

        /// <summary>
        /// Initializes a new instance of the <see cref="PhysicistsQueue{T}"/> class.
        /// </summary>
        /// <param name="frontWorkingPrefix">The evaluated prefix of the suspended front list</param>
        /// <param name="frontLength">The length of the suspended front list</param>
        /// <param name="front">The suspended front list</param>
        /// <param name="rearLength">The length of the rear list</param>
        /// <param name="rear">The rear list</param>
        private PhysicistsQueue(List<T> frontWorkingPrefix, int frontLength, Susp<List<T>> front, int rearLength, List<T> rear)
        {
            if (frontLength < rearLength)
            {
                this.frontWorkingPrefix = front.Force();
                this.frontLength = frontLength + rearLength;
                this.front = new Susp<List<T>>(() => front.Force().Append(rear.Reverse()));
                this.rearLength = 0;
                this.rear = List<T>.Empty;
            }
            else
            {
                this.frontWorkingPrefix = frontWorkingPrefix;
                this.frontLength = frontLength;
                this.front = front;
                this.rearLength = rearLength;
                this.rear = rear;
            }

            if (this.frontWorkingPrefix.IsEmpty())
            {
                this.frontWorkingPrefix = this.front.Force();
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
            if (this.frontWorkingPrefix.IsEmpty())
            {
                throw new EmptyCollectionException();
            }

            return this.frontWorkingPrefix.Head();
        }

        /// <summary>
        /// Returns the tail of the queue.
        /// </summary>
        /// <returns>
        /// The tail of the queue.
        /// </returns>
        public IQueue<T> Tail()
        {
            if (this.frontWorkingPrefix.IsEmpty())
            {
                throw new EmptyCollectionException();
            }

            return new PhysicistsQueue<T>(
                this.frontWorkingPrefix.Tail(),
                this.frontLength - 1,
                new Susp<List<T>>(() => this.front.Force().Tail()),
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
            return new PhysicistsQueue<T>(
                this.frontWorkingPrefix,
                this.frontLength,
                this.front,
                this.rearLength + 1,
                this.rear.Cons(element));
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
            return new QueueEnumerator<T>(this);
        }
    }
}