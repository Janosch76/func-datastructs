namespace FunctionalDataStructures.Utils
{
    using System;

    public class Susp<T> : ISusp<T>
    {
        private readonly Lazy<T> susp;

        public Susp(Func<T> computation)
        {
            this.susp = new Lazy<T>(computation);
        }

        public static ISusp<T> Return(T value)
        {
            return new Susp<T>(() => value);
        }

        public static Func<ISusp<T>, ISusp<S>> Lazy<S>(Func<T, S> f)
        {
            return x => x.Select(f);
        }

        public static Func<ISusp<T1>, ISusp<T2>, ISusp<S>> Lazy<T1,T2,S>(Func<T1,T2, S> f)
        {
            return (x,y) => new Susp<S>(() => f(x.Force(),y.Force()));
        }

        public T Force()
        {
            return this.susp.Value;
        }

        public ISusp<S> Select<S>(Func<T, S> f)
        {
            return SelectMany(x => new Susp<S>(() => f(x)));
        }

        public ISusp<S> SelectMany<S>(Func<T, ISusp<S>> f)
        {
            return f(Force());
        }
    }
}
