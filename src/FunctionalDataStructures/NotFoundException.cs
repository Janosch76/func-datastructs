namespace FunctionalDataStructures
{
    using System;

    /// <summary>
    /// Indicates that a value was not found.
    /// </summary>
    public class NotFoundException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NotFoundException"/> class.
        /// </summary>
        public NotFoundException()
            : base("Key not found.")
        {
        }
    }
}
