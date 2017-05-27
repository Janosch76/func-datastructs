namespace FunctionalDataStructures.Test.UnitTests.Queue
{
    using System;
    using FunctionalDataStructures.Queue;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class BankersQueueTests : TestBase
    {
        [TestMethod]
        [UnitTest]
        public void CountOfEmptyQueue()
        {
            var empty = BankersQueue<int>.Empty;

            Assert.AreEqual(
                0,
                empty.Count);
        }

        [TestMethod]
        [UnitTest]
        public void CountOfSingletonQueue()
        {
            var empty = BankersQueue<int>.Empty;

            Assert.AreEqual(
                1,
                empty.Snoc(1).Count);
        }

        [TestMethod]
        [UnitTest]
        public void CountOfNonemptyQueue()
        {
            var empty = BankersQueue<int>.Empty;

            Assert.AreEqual(
                2,
                empty.Snoc(1).Snoc(2).Count);
        }

        [TestMethod]
        [UnitTest]
        public void IsEmptyOnEmptyQueue()
        {
            var empty = BankersQueue<int>.Empty;

            Assert.AreEqual(
                true,
                empty.IsEmpty());
        }

        [TestMethod]
        [UnitTest]
        public void IsEmptyOnSingletonQueue()
        {
            var empty = BankersQueue<int>.Empty;

            Assert.AreEqual(
                false,
                empty.Snoc(1).IsEmpty());
        }

        [TestMethod]
        [UnitTest]
        public void HeadOnEmptyQueue()
        {
            var empty = BankersQueue<int>.Empty;

            AssertThrows<EmptyCollectionException>(() =>
                empty.Head());
        }

        [TestMethod]
        [UnitTest]
        public void HeadOnSingletonQueue()
        {
            var empty = BankersQueue<int>.Empty;

            Assert.AreEqual(
                1,
                empty.Snoc(1).Head());
        }

        [TestMethod]
        [UnitTest]
        public void TailOnEmptyQueue()
        {
            var empty = BankersQueue<int>.Empty;

            AssertThrows<EmptyCollectionException>(() =>
                empty.Tail());
        }

        [TestMethod]
        [UnitTest]
        public void TailOnSingletonQueue()
        {
            var empty = BankersQueue<int>.Empty;

            Assert.AreEqual(
                true,
                empty.Snoc(1).Tail().IsEmpty());
        }

        [TestMethod]
        [UnitTest]
        public void FifoBehaviorOfQueue()
        {
            var queue = BankersQueue<int>.Empty.Snoc(1).Snoc(2);  // [1,2]

            Assert.AreEqual(
                1,
                queue.Head());

            Assert.AreEqual(
                2,
                queue.Tail().Head());
        }
    }
}
