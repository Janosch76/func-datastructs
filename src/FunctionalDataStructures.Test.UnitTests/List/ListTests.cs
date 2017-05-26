namespace FunctionalDataStructures.Test.UnitTests.List
{
    using System;
    using FunctionalDataStructures.List;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ListTests : TestBase
    {
        [TestMethod]
        [UnitTest]
        public void CountOfEmptyList()
        {
            var empty = List<int>.Empty;

            Assert.AreEqual(
                0,
                empty.Count);
        }

        [TestMethod]
        [UnitTest]
        public void CountOfSingletonList()
        {
            var empty = List<int>.Empty;

            Assert.AreEqual(
                1,
                empty.Cons(1).Count);
        }

        [TestMethod]
        [UnitTest]
        public void CountOfNonemptyList()
        {
            var empty = List<int>.Empty;

            Assert.AreEqual(
                2,
                empty.Cons(1).Cons(2).Count);
        }

        [TestMethod]
        [UnitTest]
        public void IsEmptyOnEmptyList()
        {
            var empty = List<int>.Empty;

            Assert.AreEqual(
                true,
                empty.IsEmpty());
        }

        [TestMethod]
        [UnitTest]
        public void IsEmptyOnSingletonList()
        {
            var empty = List<int>.Empty;

            Assert.AreEqual(
                false,
                empty.Cons(1).IsEmpty());
        }

        [TestMethod]
        [UnitTest]
        public void HeadOnEmptyList()
        {
            var empty = List<int>.Empty;

            AssertThrows<EmptyCollectionException>(() =>
                empty.Head());
        }

        [TestMethod]
        [UnitTest]
        public void HeadOnSingletonList()
        {
            var empty = List<int>.Empty;

            Assert.AreEqual(
                1,
                empty.Cons(1).Head());
        }

        [TestMethod]
        [UnitTest]
        public void TailOnEmptyList()
        {
            var empty = List<int>.Empty;

            AssertThrows<EmptyCollectionException>(() =>
                empty.Tail());
        }

        [TestMethod]
        [UnitTest]
        public void TailOnSingletonList()
        {
            var empty = List<int>.Empty;

            Assert.AreEqual(
                List<int>.Empty,
                empty.Cons(1).Tail());
        }

        [TestMethod]
        [UnitTest]
        public void ReverseOnNonemptyList()
        {
            var list = List<int>.Empty.Cons(1).Cons(2);  // [2,1]
            var reversed = list.Reverse();  // [1,2]

            Assert.AreEqual(
                1,
                reversed.Head());

            Assert.AreEqual(
                2,
                reversed.Tail().Head());
        }
    }
}
