namespace FunctionalDataStructures.Set
{
    using System;

    /// <summary>
    /// Red-Black tree implementation of <see cref="FunctionalDataStructures.Set.ISet{T}"/>.
    /// </summary>
    /// <typeparam name="T">The element type.</typeparam>
    public abstract class RedBlackSet<T> : ISet<T>
        where T : IComparable<T>
    {
        /// <summary>
        /// The empty set.
        /// </summary>
        public static readonly RedBlackSet<T> Empty = new Leaf();

        /// <summary>
        /// Prevents a default instance of the <see cref="RedBlackSet{T}"/> class from being created.
        /// </summary>
        private RedBlackSet()
        {
        }

        private enum Color
        {
            Red,
            Black
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
        public abstract RedBlackSet<T> Insert(T elem);

        /// <summary>
        /// Determines whether the specified element is contained in this instance.
        /// </summary>
        /// <param name="elem">The element.</param>
        /// <returns>
        ///   <c>true</c> if the specified element is a member of this instance; otherwise, <c>false</c>.
        /// </returns>
        public abstract bool IsMember(T elem);

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
        public abstract System.Collections.Generic.IEnumerator<T> GetEnumerator();

        /// <summary>
        /// Represents an empty Red-Black tree
        /// </summary>
        private class Leaf : RedBlackSet<T>
        {
            public override bool IsMember(T elem)
            {
                return false;
            }

            public override RedBlackSet<T> Insert(T elem)
            {
                return Node.Red(Empty, elem, Empty);
            }

            public override System.Collections.Generic.IEnumerator<T> GetEnumerator()
            {
                yield break;
            }
        }

        /// <summary>
        /// Represents an inner node of a Red-Black tree
        /// </summary>
        private class Node : RedBlackSet<T>
        {
            private readonly Color color;
            private readonly T element;
            private readonly RedBlackSet<T> left, right;

            private Node(Color color, RedBlackSet<T> left, T element, RedBlackSet<T> right)
            {
                this.color = color;
                this.left = left;
                this.element = element;
                this.right = right;

                Count = left.Count + 1 + right.Count;
            }

            public static Node Red(RedBlackSet<T> left, T element, RedBlackSet<T> right)
            {
                return new Node(Color.Red, left, element, right);
            }

            public static Node Black(RedBlackSet<T> left, T element, RedBlackSet<T> right)
            {
                return new Node(Color.Black, left, element, right);
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

            public override RedBlackSet<T> Insert(T elem)
            {
                if (elem.CompareTo(this.element) < 0)
                {
                    return LBalance(new Node(this.color, this.left.Insert(elem), this.element, this.right));
                }
                else if (elem.CompareTo(this.element) > 0)
                {
                    return RBalance(new Node(this.color, this.left, this.element, this.right.Insert(elem)));
                }
                else
                {
                    return new Node(this.color, this.left, this.element, this.right);
                }
            }

            public override System.Collections.Generic.IEnumerator<T> GetEnumerator()
            {
                // in-order enumeration: 
                // enumerate left subtree
                foreach (T element in this.left)
                {
                    yield return element;
                }

                // root element
                yield return this.element;

                // enumerate right subtree
                foreach (T element in this.right)
                {
                    yield return element;
                }
            }

            private static Node LBalance(Node node)
            {
                Node leftChild = node.left as Node;
                if (node.color == Color.Black && leftChild != null)
                {
                    Node leftLeftChild = leftChild.left as Node;
                    if (leftChild.color == Color.Red && leftLeftChild != null && leftLeftChild.color == Color.Red)
                    {
                        return Node.Red(
                            Node.Black(leftLeftChild.left, leftLeftChild.element, leftLeftChild.right),
                            leftChild.element,
                            Node.Black(leftChild.right, node.element, node.right));
                    }

                    Node leftRightChild = leftChild.right as Node;
                    if (leftChild.color == Color.Red && leftRightChild != null && leftRightChild.color == Color.Red)
                    {
                        return Node.Red(
                            Node.Black(leftChild.left, leftChild.element, leftRightChild.left),
                            leftRightChild.element,
                            Node.Black(leftRightChild.right, node.element, node.right));
                    }
                }

                return node;
            }

            private static Node RBalance(Node node)
            {
                Node rightChild = node.right as Node;
                if (node.color == Color.Black && rightChild != null)
                {
                    Node rightRightChild = rightChild.right as Node;
                    if (rightChild.color == Color.Red && rightRightChild != null && rightRightChild.color == Color.Red)
                    {
                        return Node.Red(
                            Node.Black(node.left, node.element, rightChild.left),
                            rightChild.element,
                            Node.Black(rightRightChild.left, rightRightChild.element, rightRightChild.right));
                    }

                    Node rightLeftChild = rightChild.left as Node;
                    if (rightChild.color == Color.Red && rightLeftChild != null && rightLeftChild.color == Color.Red)
                    {
                        return Node.Red(
                            Node.Black(node.left, node.element, rightLeftChild.left),
                            rightLeftChild.element,
                            Node.Black(rightLeftChild.right, rightChild.element, rightChild.right));
                    }
                }

                return node;
            }
        }
    }
}
