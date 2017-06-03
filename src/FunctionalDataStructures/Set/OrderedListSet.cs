namespace FunctionalDataStructures.Set
{
    using System;

    public abstract class OrderedListSet<T> : ISet<T>
        where T : IComparable<T>
    {
        /// <summary>
        /// The empty list.
        /// </summary>
        public static readonly OrderedListSet<T> Empty = new NilCell();

        /// <summary>
        /// Prevents a default instance of the <see cref="OrderedListSet{T}"/> class from being created.
        /// </summary>
        private OrderedListSet()
        {
        }

        /// <summary>
        /// Gets the number of elements in the set.
        /// </summary>
        public int Count { get; protected set; }

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
        public abstract OrderedListSet<T> Tail();

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
        /// Inserts the specified element.
        /// </summary>
        /// <param name="elem">The element.</param>
        /// <returns>
        /// The updated set.
        /// </returns>
        ISet<T> ISet<T>.Insert(T elem)
        {
            return Insert(elem);
        }

        /// <summary>
        /// Inserts the specified element.
        /// </summary>
        /// <param name="elem">The element.</param>
        /// <returns>
        /// The updated set.
        /// </returns>
        public abstract OrderedListSet<T> Insert(T elem);

        /// <summary>
        /// Determines whether the specified element is contained in this instance.
        /// </summary>
        /// <param name="elem">The element.</param>
        /// <returns>
        ///   <c>true</c> if the specified element is a member of this instance; otherwise, <c>false</c>.
        /// </returns>
        public abstract bool IsMember(T elem);

        /// <summary>
        /// Finds the element satisfying the specified predicate.
        /// </summary>
        /// <param name="pred">The predicate.</param>
        /// <returns>The first element satisfying the specified predicate.</returns>
        public abstract T Find(Func<T, bool> pred);

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

        private class NilCell : OrderedListSet<T>
        {
            public override T Head()
            {
                throw new EmptyCollectionException();
            }

            public override OrderedListSet<T> Tail()
            {
                throw new EmptyCollectionException();
            }

            public override OrderedListSet<T> Insert(T elem)
            {
                return new ConsCell(elem, this);
            }

            public override bool IsMember(T elem)
            {
                return false;
            }

            public override T Find(Func<T, bool> pred)
            {
                throw new NotFoundException();
            }
        }

        private class ConsCell : OrderedListSet<T>
        {
            private T head;
            private OrderedListSet<T> tail;

            public ConsCell(T head, OrderedListSet<T> tail)
            {
                this.head = head;
                this.tail = tail;

                Count = 1 + tail.Count;
            }

            public override T Head()
            {
                return this.head;
            }

            public override OrderedListSet<T> Tail()
            {
                return this.tail;
            }

            public override OrderedListSet<T> Insert(T elem)
            {
                if (elem.CompareTo(this.head) < 0)
                {
                    return new ConsCell(elem, this);
                }
                else if (elem.CompareTo(this.head) == 0)
                {
                    return new ConsCell(elem, this.tail);
                }
                else
                {
                    return new ConsCell(this.head, this.tail.Insert(elem));
                }
            }

            public override bool IsMember(T elem)
            {
                if (elem.CompareTo(this.head) < 0)
                {
                    return false;
                }
                else if (elem.CompareTo(this.head) == 0)
                {
                    return true;
                }
                else
                {
                    return this.tail.IsMember(elem);
                }
            }

            public override T Find(Func<T, bool> pred)
            {
                if (pred(this.head))
                {
                    return this.head;
                }
                else
                {
                    return this.tail.Find(pred);
                }
            }
        }
    }
}