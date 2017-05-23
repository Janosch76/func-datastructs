namespace FunctionalDataStructures.Utils
{
    using System;

    /// <summary>
    /// Stream library.
    /// </summary>
    /// <typeparam name="T">The element type.</typeparam>
    public class Stream<T>
    {
        /// <summary>
        /// The empty stream.
        /// </summary>
        public static readonly Stream<T> Nil = new Stream<T>(new Susp<StreamCell>(() => new NilCell()));

        private readonly Susp<StreamCell> cell;

        private Stream(StreamCell cell)
            : this(() => cell)
        {
        }

        private Stream(Func<StreamCell> comp)
            : this(new Susp<StreamCell>(comp))
        {
        }

        private Stream(Susp<StreamCell> cell)
        {
            this.cell = cell;
        }

        /// <summary>
        /// Lazy constructor for streams.
        /// </summary>
        /// <param name="head">The head element.</param>
        /// <param name="tail">The tail stream.</param>
        /// <returns>The given tail stream with the specified new head element.</returns>
        public static Stream<T> LCons(T head, Susp<Stream<T>> tail)
        {
            return new Stream<T>(() => new ConsCell(head, tail.Force()));
        }

        /// <summary>
        /// Determines whether this instance is empty.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if this instance is empty; otherwise, <c>false</c>.
        /// </returns>
        public bool IsEmpty()
        {
            return this.cell.Force() is NilCell;
        }

        /// <summary>
        /// Prepends the specified head element.
        /// </summary>
        /// <param name="head">The new head element.</param>
        /// <returns>The updated stream.</returns>
        public Stream<T> Cons(T head)
        {
            return new Stream<T>(() => new ConsCell(head, this));
        }

        /// <summary>
        /// Decomposes this instance into head and tail stream.
        /// </summary>
        /// <returns>A tuple, consisting of head element and tail stream</returns>
        /// <exception cref="FunctionalDataStructures.EmptyCollectionException">Throws exception when the stream is empty.</exception>
        public Tuple<T, Stream<T>> Uncons()
        {
            if (IsEmpty())
            {
                throw new EmptyCollectionException();
            }

            var consCell = this.cell.Force() as ConsCell;
            return Tuple.Create(consCell.Head, consCell.Tail);
        }

        /// <summary>
        /// Appends the specified stream to this instance.
        /// </summary>
        /// <param name="other">The stream to append.</param>
        /// <returns>The concatenated streams</returns>
        public Stream<T> Append(Stream<T> other)
        {
            if (IsEmpty())
            {
                return other;
            }
            else
            {
                var p = Uncons();
                var head = p.Item1;
                var tail = p.Item2;
                return LCons(head, new Susp<Stream<T>>(() => tail.Append(other)));
            }
        }

        /// <summary>
        /// Takes the specified number of elements.
        /// </summary>
        /// <param name="n">The number of elements to take.</param>
        /// <returns>The initial stream elements</returns>
        public Stream<T> Take(int n)
        {
            if (n <= 0)
            {
                return Nil;
            }
            else if (IsEmpty())
            {
                return Nil;
            }
            else
            {
                var p = Uncons();
                var head = p.Item1;
                var tail = p.Item2;
                return LCons(head, new Susp<Stream<T>>(() => tail.Take(n - 1)));
            }
        }

        /// <summary>
        /// Drops the specified number of elements.
        /// </summary>
        /// <param name="n">The number of elements to drop.</param>
        /// <returns>The stream without the initial elements</returns>
        public Stream<T> Drop(int n)
        {
            if (n <= 0)
            {
                return this;
            }
            else if (IsEmpty())
            {
                return Nil;
            }
            else
            {
                var p = Uncons();
                var head = p.Item1;
                var tail = p.Item2;
                return tail.Drop(n - 1);
            }
        }

        /// <summary>
        /// Reverses this instance.
        /// </summary>
        /// <returns>The reversed stream.</returns>
        public Stream<T> Reverse()
        {
            return Reverse(Nil);
        }

        private Stream<T> Reverse(Stream<T> reversed)
        {
            if (IsEmpty())
            {
                return reversed;
            }
            else
            {
                var p = Uncons();
                var head = p.Item1;
                var tail = p.Item2;
                return tail.Reverse(new Stream<T>(() => new ConsCell(head, tail)));
            }
        }

        /// <summary>
        /// Represents a stream cell
        /// </summary>
        private abstract class StreamCell
        {
        }

        /// <summary>
        /// Represents the empty stream cell
        /// </summary>
        private class NilCell : StreamCell
        {
        }

        /// <summary>
        /// Represents a non-empty stream cell (with head element and link to the tail stream)
        /// </summary>
        private class ConsCell : StreamCell
        {
            public ConsCell(T head, Stream<T> tail)
            {
                this.Head = head;
                this.Tail = tail;
            }

            public T Head { get; private set; }

            public Stream<T> Tail { get; private set; }
        }
    }
}
