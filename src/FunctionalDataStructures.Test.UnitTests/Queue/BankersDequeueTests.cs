namespace FunctionalDataStructures.Test.UnitTests.Queue
{
    using System;
    using System.Linq;
    using FunctionalDataStructures.Queue;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class BankersDequeueTests : TestBase
    {
        [TestMethod]
        [UnitTest]
        public void CountOfEmptyDequeue()
        {
            var empty = BankersDequeue<int>.Empty;

            Assert.AreEqual(
                0,
                empty.Count);
        }

        [TestMethod]
        [UnitTest]
        public void CountOfSingletonDequeue()
        {
            var empty = BankersDequeue<int>.Empty;

            Assert.AreEqual(
                1,
                empty.Snoc(1).Count);
        }

        [TestMethod]
        [UnitTest]
        public void CountOfNonemptyDequeue()
        {
            var empty = BankersDequeue<int>.Empty;

            Assert.AreEqual(
                2,
                empty.Snoc(1).Snoc(2).Count);
        }

        [TestMethod]
        [UnitTest]
        public void IsEmptyOnEmptyDequeue()
        {
            var empty = BankersDequeue<int>.Empty;

            Assert.AreEqual(
                true,
                empty.IsEmpty());
        }

        [TestMethod]
        [UnitTest]
        public void IsEmptyOnSingletonDequeue()
        {
            var empty = BankersDequeue<int>.Empty;

            Assert.AreEqual(
                false,
                empty.Snoc(1).IsEmpty());
        }

        [TestMethod]
        [UnitTest]
        public void HeadOnEmptyDequeue()
        {
            var empty = BankersDequeue<int>.Empty;

            AssertThrows<EmptyCollectionException>(() =>
                empty.Head());
        }

        [TestMethod]
        [UnitTest]
        public void HeadOnSingletonDequeue()
        {
            var empty = BankersDequeue<int>.Empty;

            Assert.AreEqual(
                1,
                empty.Snoc(1).Head());
        }

        [TestMethod]
        [UnitTest]
        public void TailOnEmptyDequeue()
        {
            var empty = BankersDequeue<int>.Empty;

            AssertThrows<EmptyCollectionException>(() =>
                empty.Tail());
        }

        [TestMethod]
        [UnitTest]
        public void TailOnSingletonDequeue()
        {
            var empty = BankersDequeue<int>.Empty;

            Assert.AreEqual(
                true,
                empty.Snoc(1).Tail().IsEmpty());
        }

        [TestMethod]
        [UnitTest]
        public void FifoBehaviorOfDequeue()
        {
            var queue = BankersDequeue<int>.Empty.Snoc(1).Snoc(2);  // [1,2]

            Assert.AreEqual(
                1,
                queue.Head());

            Assert.AreEqual(
                2,
                queue.Tail().Head());
        }

        [TestMethod]
        [UnitTest]
        public void FifoEnumerationOfDequeue()
        {
            var queue = BankersDequeue<int>.Empty.Snoc(1).Snoc(2);  // [1,2]
            var elements = queue.ToArray();

            Assert.AreEqual(2, elements.Length);
            Assert.AreEqual(1, elements[0]);
            Assert.AreEqual(2, elements[1]);
        }
    }
}
