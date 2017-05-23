namespace FunctionalDataStructures
{
    using System;

    /// <summary>
    /// Indicates an invalid operation on an empty collection.
    /// </summary>
    public class EmptyCollectionException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EmptyCollectionException"/> class.
        /// </summary>
        public EmptyCollectionException()
            : base("Empty")
        {
        }
    }
}
