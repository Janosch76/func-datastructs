namespace FunctionalDataStructures.List
{
    using System;

    /// <summary>
    /// Implementation of functional lists.
    /// </summary>
    /// <typeparam name="T">The element type.</typeparam>
    public abstract class List<T> : IList<T>
    {
        /// <summary>
        /// The empty list.
        /// </summary>
        public static readonly List<T> Empty = new NilCell();

        /// <summary>
        /// Prevents a default instance of the <see cref="List{T}"/> class from being created.
        /// </summary>
        private List()
        {
        }

        /// <summary>
        /// Gets or sets the number of elements in the list.
        /// </summary>
        public int Count { get; protected set; }

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
        /// Prepends the specified elem to the list.
        /// </summary>
        /// <param name="elem">The element to add.</param>
        /// <returns>
        /// The updated list.
        /// </returns>
        IList<T> IList<T>.Cons(T elem)
        {
            return Cons(elem);
        }        

        /// <summary>
        /// Prepends the specified elem to the list.
        /// </summary>
        /// <param name="elem">The element to add.</param>
        /// <returns>
        /// The updated list.
        /// </returns>
        public List<T> Cons(T elem)
        {
            return new ConsCell(elem, this);
        }

        /// <summary>
        /// Returns the first element of this instance.
        /// </summary>
        /// <returns>
        /// The head element.
        /// </returns>
        public abstract T Head();

        /// <summary>
        /// Returns the list without its first element.
        /// </summary>
        /// <returns>
        /// The updated list.
        /// </returns>
        IList<T> IList<T>.Tail()
        {
            return Tail();
        }

        /// <summary>
        /// Returns the list without its first element.
        /// </summary>
        /// <returns>
        /// The updated list.
        /// </returns>
        public abstract List<T> Tail();

        /// <summary>
        /// Appends the specified list to this instance.
        /// </summary>
        /// <param name="other">The other list.</param>
        /// <returns>The concatenated lists.</returns>
        public abstract List<T> Append(List<T> other);

        /// <summary>
        /// Finds the first element satisfying the specified predicate.
        /// </summary>
        /// <param name="predicate">The predicate.</param>
        /// <returns>Returns the first element satisfying the given predicate.</returns>
        public abstract T Find(Func<T, bool> predicate);

        /// <summary>
        /// Reverses this list instance.
        /// </summary>
        /// <returns>The reversed list.</returns>
        public List<T> Reverse()
        {
            return Reverse(Empty);
        }

        /// <summary>
        /// Reverses this list instance.
        /// </summary>
        /// <param name="reversed">Accumulator argument.</param>
        /// <returns>The reversed list.</returns>
        protected abstract List<T> Reverse(List<T> reversed);

        /// <summary>
        /// Represents the empty list
        /// </summary>
        private class NilCell : List<T>
        {
            public NilCell()
            {
                Count = 0;
            }

            public override T Head()
            {
                throw new EmptyCollectionException();
            }

            public override List<T> Tail()
            {
                throw new EmptyCollectionException();
            }

            public override List<T> Append(List<T> other)
            {
                return other;
            }

            public override T Find(Func<T, bool> predicate)
            {
                throw new NotFoundException();
            }

            protected override List<T> Reverse(List<T> reversed)
            {
                return reversed;
            }
        }

        /// <summary>
        /// Represents a non-empty list
        /// </summary>
        private class ConsCell : List<T>
        {
            private T head;
            private List<T> tail;

            public ConsCell(T head, List<T> tail)
            {
                this.head = head;
                this.tail = tail;

                Count = tail.Count + 1;
            }

            public override T Head()
            {
                return this.head;
            }

            public override List<T> Tail()
            {
                return this.tail;
            }

            public override List<T> Append(List<T> other)
            {
                return new ConsCell(this.head, this.tail.Append(other));
            }

            public override T Find(Func<T, bool> predicate)
            {
                if (predicate(this.head))
                {
                    return this.head;
                }
                else
                {
                    return this.tail.Find(predicate);
                }
            }

            protected override List<T> Reverse(List<T> reversed)
            {
                return this.tail.Reverse(reversed.Cons(this.head));
            }
        }
    }
}
