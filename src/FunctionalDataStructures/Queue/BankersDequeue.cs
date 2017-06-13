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
        public static readonly BankersDequeue<T> Empty = new BankersDequeue<T>(0, 0);

        private int frontLength, rearLength;

        public BankersDequeue(int frontLength, int rearLength)
        {
            this.frontLength = frontLength;
            this.rearLength = rearLength;
        }

        public int Count
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public IQueue<T> Cons(T element)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<T> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public T Head()
        {
            throw new NotImplementedException();
        }

        public IQueue<T> Init()
        {
            throw new NotImplementedException();
        }

        public bool IsEmpty()
        {
            throw new NotImplementedException();
        }

        public T Last()
        {
            throw new NotImplementedException();
        }

        public IQueue<T> Snoc(T element)
        {
            throw new NotImplementedException();
        }

        public IQueue<T> Tail()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
