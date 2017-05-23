namespace FunctionalDataStructures.Heap
{
    using System;

    /// <summary>
    /// Implementation of Splay Heaps from Chris Okasakis book, p. 46ff
    /// </summary>
    /// <typeparam name="T">The element type</typeparam>
    public abstract class SplayHeap<T> : IHeap<SplayHeap<T>, T>
        where T : IComparable<T>
    {
        /// <summary>
        /// The empty heap.
        /// </summary>
        public static readonly SplayHeap<T> Empty = new Leaf();

        /// <summary>
        /// Prevents a default instance of the <see cref="SplayHeap{T}"/> class from being created.
        /// </summary>
        private SplayHeap()
        {
        }

        /// <summary>
        /// Gets or sets the number of elements in the heap.
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
        /// Deletes the minimum element from the heap.
        /// </summary>
        /// <returns>
        /// The updated heap.
        /// </returns>
        public abstract SplayHeap<T> DeleteMin();

        /// <summary>
        /// Finds the minimum element in the heap.
        /// </summary>
        /// <returns>
        /// The minimu element.
        /// </returns>
        public abstract T FindMin();

        /// <summary>
        /// Merges the heap with a specified heap.
        /// </summary>
        /// <param name="heap">The heap to merge with the current instance.</param>
        /// <returns>
        /// The merged heap.
        /// </returns>
        public abstract SplayHeap<T> Merge(SplayHeap<T> heap);

        /// <summary>
        /// Inserts the specified element into the heap.
        /// </summary>
        /// <param name="elem">The element to insert.</param>
        /// <returns>
        /// The updated heap.
        /// </returns>
        public SplayHeap<T> Insert(T elem)
        {
            var p = Partition(elem);
            return new Node(p.Item1, elem, p.Item2);
        }

        /// <summary>
        /// Partitions heap using the specified pivot.
        /// </summary>
        /// <param name="pivot">The pivot.</param>
        /// <returns>
        ///   A pair of heaps, where the first heap contains elements smaller than
        ///   the pivot and the second heap contains elements larger than the pivot.
        /// </returns>
        protected abstract Tuple<SplayHeap<T>, SplayHeap<T>> Partition(T pivot);

        /// <summary>
        /// Represents a leaf node (empty heap)
        /// </summary>
        private class Leaf : SplayHeap<T>
        {
            public override T FindMin()
            {
                throw new EmptyCollectionException();
            }

            public override SplayHeap<T> DeleteMin()
            {
                throw new EmptyCollectionException();
            }

            public override SplayHeap<T> Merge(SplayHeap<T> heap)
            {
                return heap;
            }

            protected override Tuple<SplayHeap<T>, SplayHeap<T>> Partition(T pivot)
            {
                return Tuple.Create(Empty, Empty);
            }
        }

        /// <summary>
        /// Represents an inner node (non-empty sub heap)
        /// </summary>
        private class Node : SplayHeap<T>
        {
            private readonly T element;
            private readonly SplayHeap<T> left, right;

            public Node(SplayHeap<T> left, T element, SplayHeap<T> right)
            {
                this.element = element;
                this.left = left;
                this.right = right;

                Count = left.Count + 1 + right.Count;
            }

            public override SplayHeap<T> DeleteMin()
            {
                Node leftChild = this.left as Node;
                if (leftChild == null)
                {
                    return this.right;
                }
                else if (leftChild.left is Leaf)
                {
                    return new Node(leftChild.right, this.element, this.right);
                }
                else
                {
                    return new Node(
                        leftChild.left.DeleteMin(), 
                        leftChild.element, 
                        new Node(leftChild.right, this.element, this.right));
                }
            }

            public override T FindMin()
            {
                if (this.left is Leaf)
                {
                    return this.element;
                }
                else
                {
                    return this.left.FindMin();
                }
            }

            public override SplayHeap<T> Merge(SplayHeap<T> heap)
            {
                var p = heap.Partition(this.element);
                return new Node(this.left.Merge(p.Item1), this.element, this.right.Merge(p.Item2));
            }

            protected override Tuple<SplayHeap<T>, SplayHeap<T>> Partition(T pivot)
            {
                if (this.element.CompareTo(pivot) <= 0)
                {
                    Node rightChild = this.right as Node;
                    if (rightChild == null)
                    {
                        return Tuple.Create(this as SplayHeap<T>, Empty);
                    }
                    else if (rightChild.element.CompareTo(pivot) <= 0)
                    {
                        var p = rightChild.right.Partition(pivot);
                        SplayHeap<T> h1 = new Node(new Node(this.left, this.element, rightChild.left), rightChild.element, p.Item1);
                        SplayHeap<T> h2 = p.Item2;
                        return Tuple.Create(h1, h2);
                    }
                    else
                    {
                        var p = rightChild.left.Partition(pivot);
                        SplayHeap<T> h1 = new Node(this.left, this.element, p.Item1);
                        SplayHeap<T> h2 = new Node(p.Item2, rightChild.element, rightChild.right);
                        return Tuple.Create(h1, h2);
                    }
                }
                else
                {
                    // this.element > pivot
                    Node leftChild = this.left as Node;
                    if (leftChild == null)
                    {
                        return Tuple.Create(Empty, this as SplayHeap<T>);
                    }
                    else if (leftChild.element.CompareTo(pivot) <= 0)
                    {
                        var p = leftChild.right.Partition(pivot);
                        SplayHeap<T> h1 = new Node(leftChild.left, leftChild.element, p.Item1);
                        SplayHeap<T> h2 = new Node(p.Item2, this.element, this.right);
                        return Tuple.Create(h1, h2);
                    }
                    else
                    {
                        var p = leftChild.left.Partition(pivot);
                        SplayHeap<T> h1 = p.Item1;
                        SplayHeap<T> h2 = new Node(p.Item2, leftChild.element, new Node(leftChild.right, this.element, this.right));
                        return Tuple.Create(h1, h2);
                    }
                }
            }
        }
    }
}
