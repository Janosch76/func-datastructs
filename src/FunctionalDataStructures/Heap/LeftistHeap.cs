namespace FunctionalDataStructures.Heap
{
    using System;

    /// <summary>
    /// Implementation of Leftist Heaps from Chris Okasakis book, p. 17ff
    /// </summary>
    /// <typeparam name="T">The element type</typeparam>
    public abstract class LeftistHeap<T> : IHeap<LeftistHeap<T>, T>
        where T : IComparable<T>
    {
        /// <summary>
        /// The empty heap.
        /// </summary>
        public static readonly LeftistHeap<T> Empty = new Leaf();

        /// <summary>
        /// Prevents a default instance of the <see cref="LeftistHeap{T}"/> class from being created.
        /// </summary>
        private LeftistHeap()
        {
        }

        /// <summary>
        /// Gets or sets the number of elements in the heap.
        /// </summary>
        public int Count { get; protected set; }

        /// <summary>
        /// Gets or sets the rank of this instance.
        /// </summary>
        protected int Rank { get; set; }

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
        /// Inserts the specified element into the heap.
        /// </summary>
        /// <param name="elem">The element to insert.</param>
        /// <returns>
        /// The updated heap.
        /// </returns>
        public LeftistHeap<T> Insert(T elem)
        {
            return Merge(new Node(elem));
        }

        /// <summary>
        /// Merges the heap with a specified heap.
        /// </summary>
        /// <param name="heap">The heap to merge with the current instance.</param>
        /// <returns>
        /// The merged heap.
        /// </returns>
        public abstract LeftistHeap<T> Merge(LeftistHeap<T> heap);

        /// <summary>
        /// Finds the minimum element in the heap.
        /// </summary>
        /// <returns>
        /// The minimu element.
        /// </returns>
        public abstract T FindMin();

        /// <summary>
        /// Deletes the minimum element from the heap.
        /// </summary>
        /// <returns>
        /// The updated heap.
        /// </returns>
        public abstract LeftistHeap<T> DeleteMin();

        /// <summary>
        /// Represents a leaf node (empty heap)
        /// </summary>
        private class Leaf : LeftistHeap<T>
        {
            public override LeftistHeap<T> Merge(LeftistHeap<T> heap)
            {
                return heap;
            }

            public override T FindMin()
            {
                throw new EmptyCollectionException();
            }

            public override LeftistHeap<T> DeleteMin()
            {
                throw new EmptyCollectionException();
            }
        }

        /// <summary>
        /// Represents an inner node (non-empty sub heap)
        /// </summary>
        private class Node : LeftistHeap<T>
        {
            private readonly T elem;
            private readonly LeftistHeap<T> left;
            private readonly LeftistHeap<T> right;

            public Node(T elem)
            {
                this.elem = elem;
                Count = 1;
                Rank = 1;
                this.left = LeftistHeap<T>.Empty;
                this.right = LeftistHeap<T>.Empty;
            }

            private Node(T elem, LeftistHeap<T> heap1, LeftistHeap<T> heap2)
            {
                this.elem = elem;
                Count = heap1.Count + heap2.Count + 1;

                if (heap1.Rank >= heap2.Rank)
                {
                    Rank = heap2.Rank + 1;
                    this.left = heap1;
                    this.right = heap2;
                }
                else
                {
                    Rank = heap1.Rank + 1;
                    this.left = heap2;
                    this.right = heap1;
                }                   
            }
      
            public override LeftistHeap<T> Merge(LeftistHeap<T> heap)
            {
                if (heap == LeftistHeap<T>.Empty)
                {
                    return this;
                }
                else
                {
                    return Link(this, heap as Node);
                }
            }

            public override T FindMin()
            {
                return this.elem;
            }

            public override LeftistHeap<T> DeleteMin()
            {
                return this.left.Merge(this.right);
            }

            private static LeftistHeap<T> Link(Node t1, Node t2)
            {
                if (t1.elem.CompareTo(t2.elem) <= 0)
                {
                    return new Node(t1.elem, t1.left, t2.Merge(t1.right));
                }
                else
                {
                    return new Node(t2.elem, t2.left, t1.Merge(t2.right));
                }
            }
        }
    }
}
