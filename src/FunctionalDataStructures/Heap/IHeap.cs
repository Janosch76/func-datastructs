namespace FunctionalDataStructures.Heap
{
    using System;

    public interface IHeap<THeap, T> 
        where THeap : IHeap<THeap, T> 
        where T : IComparable<T>
    {
        int Count { get; }

        bool IsEmpty();

        THeap Insert(T elem);
        THeap Merge(THeap heap);

        T FindMin();
        THeap DeleteMin();
    }
}
