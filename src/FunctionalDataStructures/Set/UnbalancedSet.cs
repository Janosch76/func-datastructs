namespace FunctionalDataStructures.Set
{
    using System;

    /// <summary>
    /// Unbalanced binary search tree implementation of sets
    /// </summary>
    /// <typeparam name="T">The element type</typeparam>
    public abstract class UnbalancedSet<T> : ISet<T>
        where T : IComparable<T>
    {
        /// <summary>
        /// The empty set.
        /// </summary>
        public static readonly UnbalancedSet<T> Empty = new Leaf();

        /// <summary>
        /// Prevents a default instance of the <see cref="UnbalancedSet{T}"/> class from being created.
        /// </summary>
        private UnbalancedSet()
        {
        }

        /// <summary>
        /// Gets or sets the number of elements in the set.
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
        public abstract UnbalancedSet<T> Insert(T elem);

        /// <summary>
        /// Determines whether the specified element is contained in this instance.
        /// </summary>
        /// <param name="elem">The element.</param>
        /// <returns>
        ///   <c>true</c> if the specified element is a member of this instance; otherwise, <c>false</c>.
        /// </returns>
        public abstract bool IsMember(T elem);

        /// <summary>
        /// Finds the first element satisfying the specified predicate.
        /// </summary>
        /// <param name="predicate">The predicate.</param>
        /// <returns>Returns the first element satisfying the given predicate.</returns>
        public abstract T Find(Func<T, bool> predicate);

        /// <summary>
        /// Represents an empty binary tree
        /// </summary>
        private class Leaf : UnbalancedSet<T>
        {
            public override bool IsMember(T elem)
            {
                return false;
            }

            public override UnbalancedSet<T> Insert(T elem)
            {
                return new Node(Empty, elem, Empty);
            }

            public override T Find(Func<T, bool> predicate)
            {
                throw new NotFoundException();
            }
        }

        /// <summary>
        /// Represents a non-empty binary tree
        /// </summary>
        private class Node : UnbalancedSet<T>
        {
            private readonly T element;
            private readonly UnbalancedSet<T> left, right;

            public Node(UnbalancedSet<T> left, T element, UnbalancedSet<T> right)
            {
                this.left = left;
                this.element = element;
                this.right = right;

                Count = left.Count + 1 + right.Count;
            }

            public override bool IsMember(T elem)
            {
                if (elem.CompareTo(this.element) < 0)
                {
                    return this.left.IsMember(elem);
                }
                else if (elem.CompareTo(this.element) > 0)
                {
                    return this.right.IsMember(elem);
                }
                else
                {
                    return true;
                }
            }

            public override UnbalancedSet<T> Insert(T elem)
            {
                if (elem.CompareTo(this.element) < 0)
                {
                    return new Node(this.left.Insert(elem), this.element, this.right);
                }
                else if (elem.CompareTo(this.element) > 0)
                {
                    return new Node(this.left, this.element, this.right.Insert(elem));
                }
                else
                {
                    return this;
                }
            }

            public override T Find(Func<T, bool> predicate)
            {
                if (predicate(this.element))
                {
                    return this.element;
                }
                else
                {
                    try
                    {
                        return this.left.Find(predicate);
                    }
                    catch (NotFoundException)
                    {
                        return this.right.Find(predicate);
                    }
                }
            }
        }
    }
}