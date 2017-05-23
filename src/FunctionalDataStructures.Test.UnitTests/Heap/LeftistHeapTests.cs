namespace FunctionalDataStructures.Test.UnitTests.Heap
{
    using System;
    using FunctionalDataStructures.Heap;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class LeftistHeapTests : TestBase
    {
        [TestMethod]
        [UnitTest]
        public void CountOfEmptyHeap()
        {
            var empty = LeftistHeap<int>.Empty;

            Assert.AreEqual(
                0,
                empty.Count);
        }

        [TestMethod]
        [UnitTest]
        public void CountOfSingletonHeap()
        {
            var empty = LeftistHeap<int>.Empty;

            Assert.AreEqual(
                1,
                empty.Insert(1).Count);
        }

        [TestMethod]
        [UnitTest]
        public void CountOfNonemptyHeap()
        {
            var empty = LeftistHeap<int>.Empty;

            Assert.AreEqual(
                2,
                empty.Insert(1).Insert(2).Count);
        }

        [TestMethod]
        [UnitTest]
        public void IsEmptyOnEmptyHeap()
        {
            var empty = LeftistHeap<int>.Empty;

            Assert.AreEqual(
                true,
                empty.IsEmpty());
        }

        [TestMethod]
        [UnitTest]
        public void IsEmptyOnSingletonHeap()
        {
            var empty = LeftistHeap<int>.Empty;

            Assert.AreEqual(
                false,
                empty.Insert(1).IsEmpty());
        }

        [TestMethod]
        [UnitTest]
        public void FindMinOnEmptyHeap()
        {
            var empty = LeftistHeap<int>.Empty;

            AssertThrows<EmptyCollectionException>(() =>
                empty.FindMin());
        }

        [TestMethod]
        [UnitTest]
        public void FindMinOnNonemptyHeap()
        {
            var empty = LeftistHeap<int>.Empty;

            Assert.AreEqual(
                1,
                empty.Insert(1).Insert(2).FindMin());
        }

        [TestMethod]
        [UnitTest]
        public void FindMinOnNonemptyReversedHeap()
        {
            var empty = LeftistHeap<int>.Empty;

            Assert.AreEqual(
                1,
                empty.Insert(2).Insert(1).FindMin());
        }

        [TestMethod]
        [UnitTest]
        public void CountOnMergedHeapsReturnsSum()
        {
            var empty = LeftistHeap<int>.Empty;
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
            var empty = LeftistHeap<int>.Empty;
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
            var empty = LeftistHeap<int>.Empty;

            AssertThrows<EmptyCollectionException>(() =>
                empty.DeleteMin());
        }

        [TestMethod]
        [UnitTest]
        public void DeleteMinOnNonemptyHeapRemovesElement()
        {
            var empty = LeftistHeap<int>.Empty;

            Assert.AreEqual(
                2,
                empty.Insert(1).Insert(3).Insert(2).DeleteMin().Count);
        }

        [TestMethod]
        [UnitTest]
        public void DeleteMinOnNonemptyHeapRemovesMinimalElement()
        {
            var empty = LeftistHeap<int>.Empty;

            Assert.AreEqual(
                2,
                empty.Insert(1).Insert(3).Insert(2).DeleteMin().FindMin());
        }
    }
}
