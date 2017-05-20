namespace FunctionalDataStructures.RandomAccessList
{
    using FunctionalDataStructures.List;

    public interface IRandomAccessList<T>: IList<T>
    {
        new IRandomAccessList<T> Cons(T elem);
        new IRandomAccessList<T> Tail();

        T this[int index] { get; }
        IRandomAccessList<T> Update(int index, T value);
    }
}
