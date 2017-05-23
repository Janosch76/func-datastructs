namespace FunctionalDataStructures.Test.UnitTests.Heap
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using FunctionalDataStructures.Heap;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class LazyBinomialHeapTests : TestBase
    {
        [TestMethod]
        [UnitTest]
        public void CountOfEmptyHeap()
        {
            var empty = LazyBinomialHeap<int>.Empty;

            Assert.AreEqual(
                0,
                empty.Count);
        }

        [TestMethod]
        [UnitTest]
        public void CountOfSingletonHeap()
        {
            var empty = LazyBinomialHeap<int>.Empty;

            Assert.AreEqual(
                1,
                empty.Insert(1).Count);
        }

        [TestMethod]
        [UnitTest]
        public void CountOfNonemptyHeap()
        {
            var empty = LazyBinomialHeap<int>.Empty;

            Assert.AreEqual(
                2,
                empty.Insert(1).Insert(2).Count);
        }

        [TestMethod]
        [UnitTest]
        public void IsEmptyOnEmptyHeap()
        {
            var empty = LazyBinomialHeap<int>.Empty;

            Assert.AreEqual(
                true,
                empty.IsEmpty());
        }

        [TestMethod]
        [UnitTest]
        public void IsEmptyOnSingletonHeap()
        {
            var empty = LazyBinomialHeap<int>.Empty;

            Assert.AreEqual(
                false,
                empty.Insert(1).IsEmpty());
        }

        [TestMethod]
        [UnitTest]
        public void FindMinOnEmptyHeap()
        {
            var empty = LazyBinomialHeap<int>.Empty;

            AssertThrows<EmptyCollectionException>(() =>
                empty.FindMin());
        }

        [TestMethod]
        [UnitTest]
        public void FindMinOnNonemptyHeap()
        {
            var empty = LazyBinomialHeap<int>.Empty;

            Assert.AreEqual(
                1,
                empty.Insert(1).Insert(2).FindMin());
        }

        [TestMethod]
        [UnitTest]
        public void FindMinOnNonemptyReversedHeap()
        {
            var empty = LazyBinomialHeap<int>.Empty;

            Assert.AreEqual(
                1,
                empty.Insert(2).Insert(1).FindMin());
        }

        [TestMethod]
        [UnitTest]
        public void CountOnMergedHeapsReturnsSum()
        {
            var empty = LazyBinomialHeap<int>.Empty;
            var heap1 = empty.Insert(1).Insert(3);
            var heap2 = empty.Insert(2).Insert(4);

            Assert.AreEqual(
                4,
                heap1.Merge(heap2).Count);
        }

        [TestMethod]
        [UnitTest]
        public void FindMinMergedHeapsReturnsGlobalMin()
        {
            var empty = LazyBinomialHeap<int>.Empty;
            var heap1 = empty.Insert(1).Insert(3);
            var heap2 = empty.Insert(2).Insert(4);

            Assert.AreEqual(
                1,
                heap1.Merge(heap2).FindMin());
        }

        [TestMethod]
        [UnitTest]
        public void DeleteMinOnEmptyHeap()
        {
            var empty = LazyBinomialHeap<int>.Empty;

            AssertThrows<EmptyCollectionException> (() =>
                empty.DeleteMin());
        }

        [TestMethod]
        [UnitTest]
        public void DeleteMinOnNonemptyHeapRemovesElement()
        {
            var empty = LazyBinomialHeap<int>.Empty;

            Assert.AreEqual(
                2,
                empty.Insert(1).Insert(3).Insert(2).DeleteMin().Count);
        }

        [TestMethod]
        [UnitTest]
        public void DeleteMinOnNonemptyHeapRemovesMinimalElement()
        {
            var empty = LazyBinomialHeap<int>.Empty;

            Assert.AreEqual(
                2,
                empty.Insert(1).Insert(3).Insert(2).DeleteMin().FindMin());
        }

        [TestMethod]
        [UnitTest]
        public void RepeatedFindMinReturnsAscendingSequence()
        {
            var seq = RandomSequence(1000).ToList();

            var heap = LazyBinomialHeap<int>.Empty;
            foreach(var n in seq)
            {
                heap = heap.Insert(n);
            }

            List<int> result = new List<int>();
            while (!heap.IsEmpty())
            {
                result.Add(heap.FindMin());
                heap = heap.DeleteMin();
            }

            Assert.IsTrue(IsSorted(result));
            CollectionAssert.AreEquivalent(seq.ToList(), result);
        }

        private bool IsSorted(IEnumerable<int> elements)
        {
            int prev = int.MinValue;
            foreach (var elem in elements)
            {
                if (elem < prev)
                {
                    return false;
                }

                prev = elem;
            }

            return true;
        }

        private IEnumerable<int> RandomSequence(int count)
        {
            var generator = new Random();
            for (int i=0; i< count; i++)
            {
                yield return generator.Next();
            }
        }
    }
}
