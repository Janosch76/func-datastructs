namespace FunctionalDataStructures.Utils
{
    using System;

    /// <summary>
    /// Represents a delayed computation.
    /// </summary>
    /// <typeparam name="T">The result type of the computation.</typeparam>
    public class Susp<T> : ISusp<T>
    {
        private readonly Lazy<T> susp;

        /// <summary>
        /// Initializes a new instance of the <see cref="Susp{T}"/> class.
        /// </summary>
        /// <param name="computation">The computation.</param>
        public Susp(Func<T> computation)
        {
            this.susp = new Lazy<T>(computation);
        }

        /// <summary>
        /// Creates a delayed computation that returns the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        ///   A delayed computation that returns the specified value.
        /// </returns>
        public static ISusp<T> Return(T value)
        {
            return new Susp<T>(() => value);
        }

        /// <summary>
        /// Turns the specified function into a function on delayed computations.
        /// </summary>
        /// <typeparam name="T1">The argument type of the function</typeparam>
        /// <typeparam name="S">The result type of the function.</typeparam>
        /// <param name="f">The function.</param>
        /// <returns>
        ///   A function that takes a delayed computation as argument.
        /// </returns>
        public static Func<ISusp<T1>, ISusp<S>> Lazy<T1, S>(Func<T1, S> f)
        {
            return (ISusp<T1> x) => x.Select(f);
        }

        /// <summary>
        /// Turns the specified function into a function on delayed computations.
        /// </summary>
        /// <typeparam name="T1">The type of the first argument of the function.</typeparam>
        /// <typeparam name="T2">The type of the second argument of the function.</typeparam>
        /// <typeparam name="S">The result type of the function.</typeparam>
        /// <param name="f">The function.</param>
        /// <returns>
        ///   A function that takes a delayed computation as argument.
        /// </returns>
        public static Func<ISusp<T1>, ISusp<T2>, ISusp<S>> Lazy<T1, T2, S>(Func<T1, T2, S> f)
        {
            return (x, y) => new Susp<S>(() => f(x.Force(), y.Force()));
        }

        /// <summary>
        /// Forces this instance.
        /// </summary>
        /// <returns>
        /// The result of the computation.
        /// </returns>
        public T Force()
        {
            return this.susp.Value;
        }

        /// <summary>
        /// Schedules a specified function to be applied to the result of the delayed computation.
        /// </summary>
        /// <typeparam name="S">the result type of the function.</typeparam>
        /// <param name="f">The function.</param>
        /// <returns>
        /// A new delayed computation.
        /// </returns>
        public ISusp<S> Select<S>(Func<T, S> f)
        {
            return SelectMany(x => new Susp<S>(() => f(x)));
        }

        /// <summary>
        /// Schedules a specified function to be applied to the result of the delayed computation.
        /// </summary>
        /// <typeparam name="S">the result type of the function.</typeparam>
        /// <param name="f">The function.</param>
        /// <returns>
        /// A new delayed computation.
        /// </returns>
        public ISusp<S> SelectMany<S>(Func<T, ISusp<S>> f)
        {
            return f(Force());
        }
    }
}
