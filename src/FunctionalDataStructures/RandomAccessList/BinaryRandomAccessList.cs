namespace FunctionalDataStructures.RandomAccessList
{
    using System;
    using FunctionalDataStructures.List;

    /// <summary>
    /// Implements the random access lists from Chris Okasaki´s book, p. 145ff: Structural Decomposition
    /// </summary>
    /// <typeparam name="T">The element type</typeparam>
    /// <remarks>Relies on polymorphic (type-generic?) recursion</remarks>
    public abstract class BinaryRandomAccessList<T> : IRandomAccessList<T>
    {
        public static readonly BinaryRandomAccessList<T> Empty = new Nil();

        public int Count { get; protected set; }

        private BinaryRandomAccessList()
        {
        }

        public bool IsEmpty()
        {
            return this == BinaryRandomAccessList<T>.Empty;
        }

        public T Head()
        {
            var p = this.Uncons();
            return p.Item1;
        }

        IRandomAccessList<T> IRandomAccessList<T>.Tail()
        {
            return Tail();
        }

        public BinaryRandomAccessList<T> Tail() 
        {
            var p = this.Uncons();
            return p.Item2;
        }

        IList<T> IList<T>.Tail()
        {
            var p = this.Uncons();
            return p.Item2;
        }

        IRandomAccessList<T> IRandomAccessList<T>.Update(int i, T value)
        {
            return Update(i, value);
        }

        public BinaryRandomAccessList<T> Update(int i, T value)
        {
            return this.Update(i, elem => value);
        }

        IList<T> IList<T>.Cons(T elem)
        {
            return Cons(elem);
        }

        IRandomAccessList<T> IRandomAccessList<T>.Cons(T elem)
        {
            return Cons(elem);
        }

        public abstract BinaryRandomAccessList<T> Cons(T elem);
        public abstract T this[int i] { get; }
        protected abstract Tuple<T, BinaryRandomAccessList<T>> Uncons();
        protected abstract BinaryRandomAccessList<T> Update(int i, Func<T, T> f);

        private class Nil : BinaryRandomAccessList<T>
        {
            public override BinaryRandomAccessList<T> Cons(T elem)
            {
                return new One(elem, BinaryRandomAccessList<Tuple<T, T>>.Empty);
            }

            public override T this[int i]
            {
                get { throw new IndexOutOfRangeException(); }
            }

            protected override Tuple<T, BinaryRandomAccessList<T>> Uncons()
            {
                throw new Exception("Empty");
            }

            protected override BinaryRandomAccessList<T> Update(int i, Func<T, T> f)
            {
                throw new IndexOutOfRangeException();
            }
        }

        private class Zero : BinaryRandomAccessList<T>
        {
            private readonly BinaryRandomAccessList<Tuple<T, T>> next;

            public Zero(BinaryRandomAccessList<Tuple<T, T>> next)
            {
                this.next = next;
                Count = 2 * next.Count;
            }

            public override BinaryRandomAccessList<T> Cons(T elem)
            {
                return new One(elem, this.next);
            }

            public override T this[int i]
            {
                get
                {
                    var p = this.next[i / 2];
                    return IsEven(i) ? p.Item1 : p.Item2;
                }
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

        private class One : BinaryRandomAccessList<T>
        {
            private readonly BinaryRandomAccessList<Tuple<T, T>> next;
            private readonly T elem;

            public One(T elem, BinaryRandomAccessList<Tuple<T, T>> next)
            {
                this.elem = elem;
                this.next = next;
                Count = 2 * next.Count + 1;
            }

            public override BinaryRandomAccessList<T> Cons(T elem)
            {
                return new Zero(this.next.Cons(Tuple.Create(elem, this.elem)));
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
                    };
                }
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
