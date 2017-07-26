namespace FunctionalDataStructures.Utils
{
    using System;

    /// <summary>
    /// Minimalist binary tree implementation.
    /// </summary>
    /// <typeparam name="T">The element type</typeparam>
    public abstract class BinaryTree<T> : IEquatable<BinaryTree<T>>
        where T : IEquatable<T>
    {
        /// <summary>
        /// The empty binary tree.
        /// </summary>
        public static readonly BinaryTree<T> Empty = new EmptyTree();

        /// <summary>
        /// Gets the element stored at an inner node.
        /// </summary>
        public abstract T Element { get; }

        /// <summary>
        /// Gets the left subtree.
        /// </summary>
        public abstract BinaryTree<T> Left { get; }

        /// <summary>
        /// Gets the right subtree.
        /// </summary>
        public abstract BinaryTree<T> Right { get; }

        /// <summary>
        /// Creates a non-empty tree.
        /// </summary>
        /// <param name="element">The root element.</param>
        /// <param name="left">The left subtree.</param>
        /// <param name="right">The right subtree.</param>
        /// <returns>A non-empty tree</returns>
        public static BinaryTree<T> MakeTree(T element, BinaryTree<T> left, BinaryTree<T> right)
        {
            return new Node(element, left, right);
        }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other" /> parameter; otherwise, false.
        /// </returns>
        public abstract bool Equals(BinaryTree<T> other);

        /// <summary>
        /// Determines whether this instance is empty.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if this instance is empty; otherwise, <c>false</c>.
        /// </returns>
        public abstract bool IsEmpty();

        private class EmptyTree : BinaryTree<T>
        {
            public override T Element
            {
                get { throw new EmptyCollectionException(); }
            }

            public override BinaryTree<T> Left
            {
                get { throw new EmptyCollectionException(); }
            }

            public override BinaryTree<T> Right
            {
                get { throw new EmptyCollectionException(); }
            }

            public override bool IsEmpty()
            {
                return true;
            }

            public override bool Equals(BinaryTree<T> other)
            {
                return other is EmptyTree;
            }
        }

        private class Node : BinaryTree<T>
        {
            private readonly T element;
            private readonly BinaryTree<T> left;
            private readonly BinaryTree<T> right;

            public Node(T element, BinaryTree<T> left, BinaryTree<T> right)
            {
                this.element = element;
                this.left = left;
                this.right = right;
            }

            public override T Element
            {
                get { return this.element; }
            }

            public override BinaryTree<T> Left
            {
                get { return this.left; }
            }

            public override BinaryTree<T> Right
            {
                get { return this.right; }
            }

            public override bool IsEmpty()
            {
                return false;
            }

            public override bool Equals(BinaryTree<T> other)
            {
                var otherNode = other as Node;
                if (otherNode == null)
                {
                    return false;
                }

                return this.element.Equals(otherNode.Element)
                    && this.left.Equals(otherNode.Left)
                    && this.right.Equals(otherNode.right);
            }
        }
    }
}
