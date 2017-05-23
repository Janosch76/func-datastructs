namespace FunctionalDataStructures.Set
{
    using System;

    /// <summary>
    /// Signature for sets.
    /// </summary>
    /// <typeparam name="T">The element type.</typeparam>
    public interface ISet<T>
    {
        /// <summary>
        /// Gets the number of elements in the set.
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
        /// Inserts the specified element.
        /// </summary>
        /// <param name="elem">The element.</param>
        /// <returns>
        ///   The updated set.
        /// </returns>
        ISet<T> Insert(T elem);

        /// <summary>
        /// Determines whether the specified element is contained in this instance.
        /// </summary>
        /// <param name="elem">The element.</param>
        /// <returns>
        ///   <c>true</c> if the specified element is a member of this instance; otherwise, <c>false</c>.
        /// </returns>
        bool IsMember(T elem);
    }
}
