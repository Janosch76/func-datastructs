namespace FunctionalDataStructures.Test.Application
{
    using System;
    using System.Linq;
    using FunctionalDataStructures.RandomAccessList;

    public class Program
    {
        public static void Main(string[] args)
        {
            var elems = Enumerable.Range(1, 10);
            var a = elems.Aggregate<int, BinaryRandomAccessList<int>>(BinaryRandomAccessList<int>.Empty, (xs, x) => xs.Cons(x));

            for (int i = 0; i < 10; i++)
            {
                Console.Out.WriteLine("a[{0}] = {1}", i, a[i]);
            }

            for (int i = 0; i < 10; i++)
            {
                a = a.Update(i, 2 * i);
            }

            var b = a;
            while (!b.IsEmpty())
            {
                Console.Out.WriteLine("Element at head is {0}. {1} elements in total.", b.Head(), b.Count);
                b = b.Tail();
            }

            Console.ReadKey(true);
        }
    }
}
