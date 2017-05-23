namespace FunctionalDataStructures.Utils
{
    using System;

    /// <summary>
    /// Signature for delayed computation
    /// </summary>
    /// <typeparam name="T">The result type of the computation.</typeparam>
    public interface ISusp<out T>
    {
        /// <summary>
        /// Schedules a specified function to be applied to the result of the delayed computation.
        /// </summary>
        /// <typeparam name="S">the result type of the function.</typeparam>
        /// <param name="f">The function.</param>
        /// <returns>
        ///   A new delayed computation.
        /// </returns>
        ISusp<S> Select<S>(Func<T, S> f);

        /// <summary>
        /// Schedules a specified function to be applied to the result of the delayed computation.
        /// </summary>
        /// <typeparam name="S">the result type of the function.</typeparam>
        /// <param name="f">The function.</param>
        /// <returns>
        ///   A new delayed computation.
        /// </returns>
        ISusp<S> SelectMany<S>(Func<T, ISusp<S>> f);

        /// <summary>
        /// Forces this instance.
        /// </summary>
        /// <returns>
        ///   The result of the computation.
        /// </returns>
        T Force();
    }
}
