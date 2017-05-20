namespace FunctionalDataStructures.Utils
{
    using System;

    public interface ISusp<out T>
    {
        ISusp<S> Select<S>(Func<T, S> f);
        ISusp<S> SelectMany<S>(Func<T, ISusp<S>> f);
        T Force();
    }
}
