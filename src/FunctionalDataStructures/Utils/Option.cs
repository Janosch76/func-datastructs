namespace FunctionalDataStructures.Utils
{
    using System;

    /// <summary>
    /// Option type implementation.
    /// </summary>
    /// <typeparam name="T">The element type.</typeparam>
    public abstract class Option<T>
    {
        /// <summary>
        /// Represents none.
        /// </summary>
        public static readonly Option<T> None = new NoValue();

        /// <summary>
        /// Represents some specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns> new instance containing the given value.</returns>
        public static Option<T> Some(T value)
        {
            return new SomeValue(value);
        }

        /// <summary>
        /// Gets a value indicating whether this instance has value.
        /// </summary>
        public abstract bool HasValue { get; }

        /// <summary>
        /// Gets the value.
        /// </summary>
        public abstract T Value { get; }

        private class NoValue : Option<T>
        {
            public override bool HasValue
            {
                get { return false; }
            }

            public override T Value
            {
                get { throw new InvalidOperationException(); }
            }
        }

        private class SomeValue : Option<T>
        {
            private readonly T value;

            public SomeValue(T value)
            {
                this.value = value;
            }

            public override bool HasValue
            {
                get { return true; }
            }

            public override T Value
            {
                get { return this.value; }
            }
        }
    }
}
