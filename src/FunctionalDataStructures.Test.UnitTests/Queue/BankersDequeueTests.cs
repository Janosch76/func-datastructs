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
        public void CountOfSingletonConsedDequeue()
        {
            var empty = BankersDequeue<int>.Empty;

            Assert.AreEqual(
                1,
                empty.Cons(1).Count);
        }

        [TestMethod]
        [UnitTest]
        public void CountOfSingletonSnocedDequeue()
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
                empty.Snoc(1).Cons(2).Count);
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
        public void IsEmptyOnSingletonSnocedDequeue()
        {
            var empty = BankersDequeue<int>.Empty;

            Assert.AreEqual(
                false,
                empty.Snoc(1).IsEmpty());
        }

        [TestMethod]
        [UnitTest]
        public void IsEmptyOnSingletonConsedDequeue()
        {
            var empty = BankersDequeue<int>.Empty;

            Assert.AreEqual(
                false,
                empty.Cons(1).IsEmpty());
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
        public void LastOnEmptyDequeue()
        {
            var empty = BankersDequeue<int>.Empty;

            AssertThrows<EmptyCollectionException>(() =>
                empty.Last());
        }

        [TestMethod]
        [UnitTest]
        public void HeadOnSingletonSnocedDequeue()
        {
            var empty = BankersDequeue<int>.Empty;

            Assert.AreEqual(
                1,
                empty.Snoc(1).Head());
        }

        [TestMethod]
        [UnitTest]
        public void HeadOnSingletonConsedDequeue()
        {
            var empty = BankersDequeue<int>.Empty;

            Assert.AreEqual(
                1,
                empty.Cons(1).Head());
        }

        [TestMethod]
        [UnitTest]
        public void LastOnSingletonSnocedDequeue()
        {
            var empty = BankersDequeue<int>.Empty;

            Assert.AreEqual(
                1,
                empty.Snoc(1).Last());
        }

        [TestMethod]
        [UnitTest]
        public void LastOnSingletonConsedDequeue()
        {
            var empty = BankersDequeue<int>.Empty;

            Assert.AreEqual(
                1,
                empty.Cons(1).Last());
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
        public void InitOnEmptyDequeue()
        {
            var empty = BankersDequeue<int>.Empty;

            AssertThrows<EmptyCollectionException>(() =>
                empty.Init());
        }

        [TestMethod]
        [UnitTest]
        public void TailOnSingletonSnocedDequeue()
        {
            var empty = BankersDequeue<int>.Empty;

            Assert.AreEqual(
                true,
                empty.Snoc(1).Tail().IsEmpty());
        }

        [TestMethod]
        [UnitTest]
        public void TailOnSingletonConsedDequeue()
        {
            var empty = BankersDequeue<int>.Empty;

            Assert.AreEqual(
                true,
                empty.Cons(1).Tail().IsEmpty());
        }

        [TestMethod]
        [UnitTest]
        public void InitOnSingletonSnocedDequeue()
        {
            var empty = BankersDequeue<int>.Empty;

            Assert.AreEqual(
                true,
                empty.Snoc(1).Init().IsEmpty());
        }

        [TestMethod]
        [UnitTest]
        public void initOnSingletonConsedDequeue()
        {
            var empty = BankersDequeue<int>.Empty;

            Assert.AreEqual(
                true,
                empty.Cons(1).Init().IsEmpty());
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
            var queue = BankersDequeue<int>.Empty.Snoc(2).Cons(1).Snoc(3).Cons(0).Snoc(4).Tail().Init();  // [1,2,3]
            var elements = queue.ToArray();

            Assert.AreEqual(3, elements.Length);
            Assert.AreEqual(1, elements[0]);
            Assert.AreEqual(2, elements[1]);
            Assert.AreEqual(3, elements[2]);
        }
    }
}
