namespace FunctionalDataStructures.List
{
    using System;
    using System.Collections;

    /// <summary>
    /// Generic enumerator for <see cref="IList{T}"/> implementations
    /// </summary>
    /// <typeparam name="T">The element type</typeparam>
    /// <seealso cref="System.Collections.Generic.IEnumerator{T}" />
    public class ListEnumerator<T> : System.Collections.Generic.IEnumerator<T>
    {
        private readonly IList<T> list;
        private IList<T> state;
        private bool disposedValue = false; // To detect redundant calls

        /// <summary>
        /// Initializes a new instance of the <see cref="ListEnumerator{T}"/> class.
        /// </summary>
        /// <param name="list">The list.</param>
        internal ListEnumerator(IList<T> list)
        {
            if (list == null)
            {
                throw new ArgumentNullException("list");
            }

            this.list = list;
            this.state = null;
        }

        /// <summary>
        /// Gets the element in the collection at the current position of the enumerator.
        /// </summary>
        public T Current
        {
            get { return this.state.Head(); }
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
        public bool MoveNext()
        {
            if (this.state == null)
            {
                this.state = this.list;
            }
            else
            {
                this.state = this.state.Tail();
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
