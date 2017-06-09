namespace FunctionalDataStructures.RandomAccessList
{
    using System;
    using FunctionalDataStructures.List;

    /// <summary>
    /// Implements the random access lists from Chris Okasaki´s book, p. 145ff: Structural Decomposition
    /// </summary>
    /// <typeparam name="T">The element type</typeparam>
    /// <remarks>Relies on polymorphic recursion</remarks>
    public abstract class BinaryRandomAccessList<T> : IRandomAccessList<T>
    {
        /// <summary>
        /// The empty list.
        /// </summary>
        public static readonly BinaryRandomAccessList<T> Empty = new Nil();

        /// <summary>
        /// Prevents a default instance of the <see cref="BinaryRandomAccessList{T}"/> class from being created.
        /// </summary>
        private BinaryRandomAccessList()
        {
        }

        /// <summary>
        /// Gets or sets the number of elements in the list.
        /// </summary>
        public int Count { get; protected set; }

        /// <summary>
        /// Gets the element with the specified index.
        /// </summary>
        /// <param name="i">The index.</param>
        /// <returns>
        /// The element with the specified index.
        /// </returns>
        public abstract T this[int i] { get; }

        /// <summary>
        /// Determines whether this instance is empty.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if this instance is empty; otherwise, <c>false</c>.
        /// </returns>
        public bool IsEmpty()
        {
            return this == BinaryRandomAccessList<T>.Empty;
        }

        /// <summary>
        /// Returns the first element of this instance.
        /// </summary>
        /// <returns>
        /// The head element.
        /// </returns>
        public T Head()
        {
            var p = this.Uncons();
            return p.Item1;
        }

        /// <summary>
        /// Returns the list without its first element.
        /// </summary>
        /// <returns>
        /// The updated list.
        /// </returns>
        IRandomAccessList<T> IRandomAccessList<T>.Tail()
        {
            return Tail();
        }

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
        public BinaryRandomAccessList<T> Tail() 
        {
            var p = this.Uncons();
            return p.Item2;
        }

        /// <summary>
        /// Updates the element at the specified index.
        /// </summary>
        /// <param name="i">The index.</param>
        /// <param name="value">The value.</param>
        /// <returns>
        /// The updated list.
        /// </returns>
        IRandomAccessList<T> IRandomAccessList<T>.Update(int i, T value)
        {
            return Update(i, value);
        }

        /// <summary>
        /// Updates the element at the specified index.
        /// </summary>
        /// <param name="i">The index.</param>
        /// <param name="value">The value.</param>
        /// <returns>
        /// The updated list.
        /// </returns>
        public BinaryRandomAccessList<T> Update(int i, T value)
        {
            return this.Update(i, elem => value);
        }

        /// <summary>
        /// Prepends the specified elem to the list.
        /// </summary>
        /// <param name="elem">The elem to add.</param>
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
        /// <param name="elem">The elem to add.</param>
        /// <returns>
        /// The updated list.
        /// </returns>
        IRandomAccessList<T> IRandomAccessList<T>.Cons(T elem)
        {
            return Cons(elem);
        }

        /// <summary>
        /// Prepends the specified elem to the list.
        /// </summary>
        /// <param name="elem">The elem to add.</param>
        /// <returns>
        /// The updated list.
        /// </returns>
        public abstract BinaryRandomAccessList<T> Cons(T elem);

        /// <summary>
        /// Decomposes this instance into head and tail.
        /// </summary>
        /// <returns>
        /// A pair consisting of the head and tail of the list.
        /// </returns>
        protected abstract Tuple<T, BinaryRandomAccessList<T>> Uncons();

        /// <summary>
        /// Updates the element at the specified index.
        /// </summary>
        /// <param name="i">The index.</param>
        /// <param name="f">The subtree to replace.</param>
        /// <returns>
        /// The updated list.
        /// </returns>
        protected abstract BinaryRandomAccessList<T> Update(int i, Func<T, T> f);

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
            while (!current.IsEmpty())
            {
                yield return current.Head();
                current = current.Tail();
            }
        }

        /// <summary>
        /// Represents an empty collection
        /// </summary>
        private class Nil : BinaryRandomAccessList<T>
        {
            public override T this[int i]
            {
                get { throw new IndexOutOfRangeException(); }
            }

            public override BinaryRandomAccessList<T> Cons(T elem)
            {
                return new One(elem, BinaryRandomAccessList<Tuple<T, T>>.Empty);
            }

            protected override Tuple<T, BinaryRandomAccessList<T>> Uncons()
            {
                throw new EmptyCollectionException();
            }

            protected override BinaryRandomAccessList<T> Update(int i, Func<T, T> f)
            {
                throw new IndexOutOfRangeException();
            }
        }

        /// <summary>
        /// Represents a collection with even number of elements
        /// </summary>
        private class Zero : BinaryRandomAccessList<T>
        {
            private readonly BinaryRandomAccessList<Tuple<T, T>> next;

            public Zero(BinaryRandomAccessList<Tuple<T, T>> next)
            {
                this.next = next;
                Count = 2 * next.Count;
            }

            public override T this[int i]
            {
                get
                {
                    var p = this.next[i / 2];
                    return IsEven(i) ? p.Item1 : p.Item2;
                }
            }

            public override BinaryRandomAccessList<T> Cons(T elem)
            {
                return new One(elem, this.next);
            }

            protected override Tuple<T, BinaryRandomAccessList<T>> Uncons()
            {
                var p = this.next.Uncons();
                var heads = p.Item1;
                var tail = p.Item2;
                BinaryRandomAccessList<T> list = new One(heads.Item2, tail);
                return Tuple.Create(heads.Item1, list);
            }

            protected override BinaryRandomAccessList<T> Update(int i, Func<T, T> f)
            {
                return new Zero(this.next.Update(
                    i / 2,
                    p => IsEven(i) ? Tuple.Create(f(p.Item1), p.Item2) : Tuple.Create(p.Item1, f(p.Item2))));
            }

            private static bool IsEven(int i)
            {
                return i % 2 == 0;
            }
        }

        /// <summary>
        /// Represents a collection with odd number of elements
        /// </summary>
        private class One : BinaryRandomAccessList<T>
        {
            private readonly BinaryRandomAccessList<Tuple<T, T>> next;
            private readonly T elem;

            public One(T elem, BinaryRandomAccessList<Tuple<T, T>> next)
            {
                this.elem = elem;
                this.next = next;
                Count = (2 * next.Count) + 1;
            }

            public override T this[int i]
            {
                get
                {
                    if (i == 0)
                    {
                        return this.elem;
                    }
                    else
                    {
                        return new Zero(this.next)[i - 1];
                    }
                }
            }

            public override BinaryRandomAccessList<T> Cons(T elem)
            {
                return new Zero(this.next.Cons(Tuple.Create(elem, this.elem)));
            }

            protected override Tuple<T, BinaryRandomAccessList<T>> Uncons()
            {
                if (this.next == BinaryRandomAccessList<Tuple<T, T>>.Empty)
                {
                    return Tuple.Create(this.elem, BinaryRandomAccessList<T>.Empty);
                }
                else
                {
                    BinaryRandomAccessList<T> list = new Zero(this.next);
                    return Tuple.Create(this.elem, list);
                }
            }

            protected override BinaryRandomAccessList<T> Update(int i, Func<T, T> f)
            {
                if (i == 0)
                {
                    return new One(f(this.elem), this.next);
                }
                else
                {
                    return new Zero(this.next).Update(i - 1, f).Cons(this.elem);
                }
            }
        }
    }
}
