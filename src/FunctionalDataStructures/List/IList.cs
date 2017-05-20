namespace FunctionalDataStructures.List
{
    using System;

    public interface IList<T>
    {
        int Count { get; }

        bool IsEmpty();

        IList<T> Cons(T elem);
        T Head();
        IList<T> Tail();
    }
}
