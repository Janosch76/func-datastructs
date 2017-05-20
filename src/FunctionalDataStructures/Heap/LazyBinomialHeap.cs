namespace FunctionalDataStructures.Heap
{
    using System;
    using FunctionalDataStructures.Utils;
    using FunctionalDataStructures.List;
    using FunctionalDataStructures.RandomAccessList;

    /// <summary>
    /// Binomial heaps with lazy evaluation of heap list
    /// from Chris Okasaki's book, p.70 ff
    /// </summary>
    /// <typeparam name="T">element type</typeparam>
    public class LazyBinomialHeap<T> : IHeap<LazyBinomialHeap<T>, T>
        where T : IComparable<T>
    {
        private ISusp<IList<Tree>> heap;
        private int size;

        private LazyBinomialHeap(ISusp<IList<Tree>> heap, int size)
        {
            this.heap = heap;
            this.size = size;
        }

        public static LazyBinomialHeap<T> Empty = new LazyBinomialHeap<T>(
            new Susp<IList<Tree>>(() => BinaryRandomAccessList<Tree>.Empty),
            0);

        public int Count
        {
            get { return this.size; }
        }

        public bool IsEmpty()
        {
            return this.size == 0;
        }

        public LazyBinomialHeap<T> Insert(T elem)
        {
            return new LazyBinomialHeap<T>(
                this.heap.Select(trees => Insert(new Tree(0, elem, BinaryRandomAccessList<Tree>.Empty), trees)),
                this.size + 1);
        }

        public LazyBinomialHeap<T> Merge(LazyBinomialHeap<T> other)
        {
            return new LazyBinomialHeap<T>(
                new Susp<IList<Tree>>(() => Merge(this.heap.Force(), other.heap.Force())),
                this.size + other.size);
        }

        public T FindMin()
        {
            var minTree = UnmergeMinTree(this.heap.Force()).Item1;
            return minTree.root;
        }

        public LazyBinomialHeap<T> DeleteMin()
        {
            var p = UnmergeMinTree(this.heap.Force());
            return new LazyBinomialHeap<T>(
                new Susp<IList<Tree>>(() => Merge(Reverse(p.Item1.subtrees), p.Item2)),
                this.size - 1);
        }

        private static IList<S> Reverse<S>(IList<S> list)
        {
            return Reverse(list, BinaryRandomAccessList<S>.Empty);
        }

        private static IList<S> Reverse<S>(IList<S> list, IList<S> acc)
        {
            if(list.IsEmpty())
            {
                return acc;
            }
            else
            {
                return Reverse(list.Tail(), acc.Cons(list.Head()));
            }
        }

        private static IList<Tree> Insert(Tree tree, IList<Tree> trees)
        {
            if (trees.Count == 0)
            {
                return trees.Cons(tree);
            }
            else if (tree.rank < trees.Head().rank)
            {
                return trees.Cons(tree);
            }
            else
            {
                return Insert(Link(tree, trees.Head()), trees.Tail());
            }
        }
        
        private static Tree Link(Tree t1, Tree t2)
        {
            var rank = t1.rank + 1;
            if(t1.root.CompareTo(t2.root) < 0)
            {
                return new Tree(rank, t1.root, t1.subtrees.Cons(t2));
            }
            else
            {
                return new Tree(rank, t2.root, t2.subtrees.Cons(t1));
            }
        }

        private static IList<Tree> Merge(IList<Tree> trees1, IList<Tree> trees2)
        {
            if(trees1.IsEmpty())
            {
                return trees2;
            }
            else if (trees2.IsEmpty())
            {
                return trees1;
            }
            else if (trees1.Head().rank < trees2.Head().rank)
            {
                return Merge(trees1.Tail(), trees2).Cons(trees1.Head());
            }
            else if (trees1.Head().rank > trees2.Head().rank)
            {
                return Merge(trees1, trees2.Tail()).Cons(trees2.Head());
            }
            else
            {
                return Insert(Link(trees1.Head(), trees2.Head()), Merge(trees1.Tail(), trees2.Tail()));
            }
        }

        private static Tuple<Tree, IList<Tree>> UnmergeMinTree(IList<Tree> trees)
        {
            if(trees.IsEmpty())
            {
                throw new Exception("Empty");
            }
            else if(trees.Count == 1)
            {
                IList<Tree> empty = BinaryRandomAccessList<Tree>.Empty;
                return Tuple.Create(trees.Head(), empty);
            }
            else
            {
                var tHead = trees.Head();
                var candidate = UnmergeMinTree(trees.Tail());
                if(tHead.root.CompareTo(candidate.Item1.root) < 0)
                {
                    return Tuple.Create(tHead, trees.Tail());
                }
                else
                {
                    return Tuple.Create(candidate.Item1, candidate.Item2.Cons(tHead));
                }
            }
        }

        private class Tree
        {
            public readonly int rank;
            public readonly T root;
            public readonly IList<Tree> subtrees;
   
            public Tree(int rank, T root, IList<Tree> subtrees)
            {
                this.rank = rank;
                this.root = root;
                this.subtrees = subtrees;
            }

        }
    }
}
