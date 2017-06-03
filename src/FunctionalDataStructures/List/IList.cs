namespace FunctionalDataStructures.List
{
    /// <summary>
    /// Signature for persistent lists
    /// </summary>
    /// <typeparam name="T">The element type</typeparam>
    public interface IList<T> : System.Collections.Generic.IEnumerable<T>
    {
        /// <summary>
        /// Gets the number of elements in the list.
        /// </summary>
        int Count { get; }

        /// <summary>
        /// Determines whether this instance is empty.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if this instance is empty; otherwise, <c>false</c>.
        /// </returns>
        bool IsEmpty();

        /// <summary>
        /// Prepends the specified elem to the list.
        /// </summary>
        /// <param name="elem">The elem to add.</param>
        /// <returns>
        ///   The updated list.
        /// </returns>
        IList<T> Cons(T elem);

        /// <summary>
        /// Returns the first element of this instance.
        /// </summary>
        /// <returns>
        ///   The head element.
        /// </returns>
        T Head();

        /// <summary>
        /// Returns the list without its first element.
        /// </summary>
        /// <returns>
        ///   The updated list.
        /// </returns>
        IList<T> Tail();
    }
}
