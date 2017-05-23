namespace FunctionalDataStructures.RandomAccessList
{
    using FunctionalDataStructures.List;

    /// <summary>
    /// Signature for random access lists
    /// </summary>
    /// <typeparam name="T">The element type</typeparam>
    /// <seealso cref="FunctionalDataStructures.List.IList{T}" />
    public interface IRandomAccessList<T> : IList<T>
    {
        /// <summary>
        /// Gets the element at the specified index.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns>
        ///   Returns the element at the specified index
        /// </returns>
        T this[int index] { get; }

        /// <summary>
        /// Updates the element at the specified index.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="value">The value.</param>
        /// <returns>
        ///   The updated list.
        /// </returns>
        IRandomAccessList<T> Update(int index, T value);

        /// <summary>
        /// Prepends the specified elem to the list.
        /// </summary>
        /// <param name="elem">The elem to add.</param>
        /// <returns>
        ///   The updated list.
        /// </returns>
        new IRandomAccessList<T> Cons(T elem);

        /// <summary>
        /// Returns the list without its first element.
        /// </summary>
        /// <returns>
        ///   The updated list.
        /// </returns>
        new IRandomAccessList<T> Tail();
    }
}
