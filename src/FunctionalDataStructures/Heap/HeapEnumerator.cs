namespace FunctionalDataStructures.Heap
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    /// <summary>
    /// Generic enumerator for <see cref="IHeap{THeap, T}"/> implementations
    /// </summary>
    /// <typeparam name="THeap">The heap type</typeparam>
    /// <typeparam name="T">The element type</typeparam>
    /// <seealso cref="System.Collections.Generic.IEnumerator{T}" />
    public class HeapEnumerator<THeap,T> : IEnumerator<T>
        where THeap : IHeap<THeap, T>
        where T : IComparable<T>
    {
        private readonly IHeap<THeap,T> heap;
        private IHeap<THeap,T> state;
        private bool disposedValue = false; // To detect redundant calls

        /// <summary>
        /// Initializes a new instance of the <see cref="HeapEnumerator{THeap, T}"/> class.
        /// </summary>
        /// <param name="heap">The heap.</param>
        internal HeapEnumerator(IHeap<THeap, T> heap)
        {
            if (heap == null)
            {
                throw new ArgumentNullException("heap");
            }

            this.heap = heap;
            this.state = null;
        }

        /// <summary>
        /// Gets the element in the collection at the current position of the enumerator.
        /// </summary>
        public T Current
        {
            get { return this.state.FindMin(); }
        }

        /// <summary>
        /// Gets the element in the collection at the current position of the enumerator.
        /// </summary>
        object IEnumerator.Current
        {
            get { return Current; }
        }

        /// <summary>
        /// Advances the enumerator to the next element of the collection.
        /// </summary>
        /// <returns>
        /// true if the enumerator was successfully advanced to the next element; false if the enumerator has passed the end of the collection.
        /// </returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public bool MoveNext()
        {
            if (this.state == null)
            {
                this.state = this.heap;
            }
            else
            {
                this.state = this.state.DeleteMin();
            }

            return !this.state.IsEmpty();
        }


        /// <summary>
        /// Sets the enumerator to its initial position, which is before the first element in the collection.
        /// </summary>
        public void Reset()
        {
            this.state = null;
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // no managed state (managed objects) needs disposing here.
                }

                disposedValue = true;
            }
        }
    }
}
