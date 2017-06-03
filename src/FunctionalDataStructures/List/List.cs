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
        /// Checks if any element in the list satisfies the specified predicate.
        /// </summary>
        /// <param name="predicate">The predicate.</param>
        /// <returns>True if one or more elements satisfy the given predicate.</returns>
        public abstract bool Any(Func<T, bool> predicate);

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
            var current = this;
            while (current is ConsCell)
            {
                var cell = current as ConsCell;
                yield return cell.Head();
                current = cell.Tail();
            }
        }

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

            public override bool Any(Func<T, bool> predicate)
            {
                return false;
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

            public override bool Any(Func<T, bool> predicate)
            {
                return predicate(this.head) || this.tail.Any(predicate);
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
