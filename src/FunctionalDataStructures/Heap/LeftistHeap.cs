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
        public static readonly LeftistHeap<T> Empty = new Leaf();

        public int Count { get; protected set; }

        protected int Rank;

        private LeftistHeap()
        {
        }

        public abstract bool IsEmpty();

        public LeftistHeap<T> Insert(T elem)
        {
            return Merge(new Node(elem));
        }

        public abstract LeftistHeap<T> Merge(LeftistHeap<T> heap);

        public abstract T FindMin();

        public abstract LeftistHeap<T> DeleteMin();

        private class Leaf : LeftistHeap<T>
        {
            public override bool IsEmpty()
            {
                return true;
            }

            public override LeftistHeap<T> Merge(LeftistHeap<T> heap)
            {
                return heap;
            }

            public override T FindMin()
            {
                throw new Exception("Empty");
            }

            public override LeftistHeap<T> DeleteMin()
            {
                throw new Exception("Empty");
            }
        }

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
      
            public override bool IsEmpty()
            {
                return false;
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
