namespace FunctionalDataStructures.Queue
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using FunctionalDataStructures.Utils;

    /// <summary>
    /// Amortized double-ended queue implementation using the Bankers Method, 
    /// from Chris Okasaki´s book, p. 64ff
    /// </summary>
    /// <typeparam name="T">The element type</typeparam>
    public class BankersDequeue<T> : IDequeue<T>
    {
        /// <summary>
        /// The empty dequeue.
        /// </summary>
        public static readonly BankersDequeue<T> Empty = new BankersDequeue<T>(0, Stream<T>.Nil, 0, Stream<T>.Nil);

        private const int c = 2;

        private Stream<T> front, rear;
        private int frontLength, rearLength;

        private BankersDequeue(int frontLength, Stream<T> front, int rearLength, Stream<T> rear)
        {
            if (frontLength > (c * rearLength) + 1)
            {
                var i = (frontLength + rearLength) / 2;
                var j = frontLength + rearLength - i;
                var rebalancedFront = front.Take(i);
                var rebalancedRear = rear.Append(front.Drop(i).Reverse());

                this.frontLength = i;
                this.front = rebalancedFront;
                this.rearLength = j;
                this.rear = rebalancedRear;
            }
            else if (rearLength > (c * frontLength) + 1)
            {
                var j = (frontLength + rearLength) / 2;
                var i = frontLength + rearLength - j;
                var rebalancedFront = front.Append(rear.Drop(j).Reverse());
                var rebalancedRear = rear.Take(j);

                this.frontLength = i;
                this.front = rebalancedFront;
                this.rearLength = j;
                this.rear = rebalancedRear;
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
        /// Gets the number of elements in the dequeue.
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
        /// Returns the head element of the dequeue.
        /// </summary>
        /// <returns>
        /// The head element of the dequeue.
        /// </returns>
        public T Head()
        {
            if (this.front.IsEmpty() && this.rear.IsEmpty())
            {
                throw new EmptyCollectionException();
            }

            if (this.front.IsEmpty())
            {
                var p = this.rear.Uncons();
                return p.Item1;
            }
            else
            {
                var p = this.front.Uncons();
                return p.Item1;
            }
        }

        /// <summary>
        /// Returns the last element of the queue.
        /// </summary>
        /// <returns>
        /// The last element of the queue.
        /// </returns>
        public T Last()
        {
            if (this.front.IsEmpty() && this.rear.IsEmpty())
            {
                throw new EmptyCollectionException();
            }

            if (this.rear.IsEmpty())
            {
                var p = this.front.Uncons();
                return p.Item1;
            }
            else
            {
                var p = this.rear.Uncons();
                return p.Item1;
            }
        }

        /// <summary>
        /// Returns the tail of the dequeue.
        /// </summary>
        /// <returns>
        ///   The tail of the dequeue.
        /// </returns>
        public BankersDequeue<T> Tail()
        {
            if (this.front.IsEmpty() && this.rear.IsEmpty())
            {
                throw new EmptyCollectionException();
            }

            if (this.front.IsEmpty())
            {
                return BankersDequeue<T>.Empty;
            }
            else
            {
                var p = this.front.Uncons();
                return new BankersDequeue<T>(
                    this.frontLength - 1,
                    p.Item2,
                    this.rearLength,
                    this.rear);
            }
        }

        /// <summary>
        /// Returns the front of the dequeue, without the last element.
        /// </summary>
        /// <returns>
        ///   The front of the dequeue.
        /// </returns>
        public BankersDequeue<T> Init()
        {
            if (this.front.IsEmpty() && this.rear.IsEmpty())
            {
                throw new EmptyCollectionException();
            }

            if (this.rear.IsEmpty())
            {
                return BankersDequeue<T>.Empty;
            }
            else
            {
                var p = this.rear.Uncons();
                return new BankersDequeue<T>(
                    this.frontLength,
                    this.front,
                    this.rearLength - 1,
                    p.Item2);
            }
        }

        /// <summary>
        /// Prepends the specified element to the queue.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <returns>
        /// The queue with the specified element at the front.
        /// </returns>
        public BankersDequeue<T> Cons(T element)
        {
            return new BankersDequeue<T>(
                this.frontLength + 1,
                this.front.Cons(element),
                this.rearLength,
                this.rear);
        }

        /// <summary>
        /// Appends the specified element to the dequeue.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <returns>
        /// The queue with the specified element at the end.
        /// </returns>
        public BankersDequeue<T> Snoc(T element)
        {
            return new BankersDequeue<T>(
                this.frontLength,
                this.front,
                this.rearLength + 1,
                this.rear.Cons(element));
        }

        /// <summary>
        /// Reverses this instance.
        /// </summary>
        /// <returns>The reversed dequeue</returns>
        public BankersDequeue<T> Reverse()
        {
            return new BankersDequeue<T>(
                this.rearLength,
                this.rear,
                this.frontLength,
                this.front);
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// An enumerator that can be used to iterate through the collection.
        /// </returns>
        public IEnumerator<T> GetEnumerator()
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
        /// Returns the tail of the dequeue.
        /// </summary>
        /// <returns>
        /// The tail of the dequeue.
        /// </returns>
        IDequeue<T> IDequeue<T>.Tail()
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
        /// Appends the specified element to the dequeue.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <returns>
        /// The queue with the specified element at the end.
        /// </returns>
        IDequeue<T> IDequeue<T>.Snoc(T element)
        {
            return Snoc(element);
        }

        /// <summary>
        /// Prepends the specified element to the dequeue.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <returns>
        /// The dequeue with the specified element at the front.
        /// </returns>
        IDequeue<T> IDequeue<T>.Cons(T element)
        {
            return Cons(element);
        }

        /// <summary>
        /// Returns the front of the dequeue, without the last element.
        /// </summary>
        /// <returns>
        /// The front of the dequeue.
        /// </returns>
        IDequeue<T> IDequeue<T>.Init()
        {
            return Init();
        }

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through the collection.
        /// </returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
