namespace FunctionalDataStructures.Test.UnitTests.RandomAccessList
{
    using System;
    using FunctionalDataStructures.RandomAccessList;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class BinaryRandomAccessListTests : TestBase
    {
        [TestMethod]
        [UnitTest]
        public void CountOfEmptyRandomAccessList()
        {
            var empty = BinaryRandomAccessList<int>.Empty;

            Assert.AreEqual(
                0,
                empty.Count);
        }

        [TestMethod]
        [UnitTest]
        public void CountOfSingletonRandomAccessList()
        {
            var empty = BinaryRandomAccessList<int>.Empty;

            Assert.AreEqual(
                1,
                empty.Cons(1).Count);
        }

        [TestMethod]
        [UnitTest]
        public void CountOfNonemptyRandomAccessList()
        {
            var empty = BinaryRandomAccessList<int>.Empty;

            Assert.AreEqual(
                2,
                empty.Cons(1).Cons(2).Count);
        }

        [TestMethod]
        [UnitTest]
        public void IsEmptyOnEmptyRandomAccessList()
        {
            var empty = BinaryRandomAccessList<int>.Empty;

            Assert.AreEqual(
                true,
                empty.IsEmpty());
        }

        [TestMethod]
        [UnitTest]
        public void IsEmptyOnSingletonRandomAccessList()
        {
            var empty = BinaryRandomAccessList<int>.Empty;

            Assert.AreEqual(
                false,
                empty.Cons(1).IsEmpty());
        }

        [TestMethod]
        [UnitTest]
        public void HeadOnEmptyRandomAccessList()
        {
            var empty = BinaryRandomAccessList<int>.Empty;

            AssertThrows<EmptyCollectionException>(() =>
                empty.Head());
        }

        [TestMethod]
        [UnitTest]
        public void HeadOnSingletonRandomAccessList()
        {
            var empty = BinaryRandomAccessList<int>.Empty;

            Assert.AreEqual(
                1,
                empty.Cons(1).Head());
        }

        [TestMethod]
        [UnitTest]
        public void TailOnEmptyRandomAccessList()
        {
            var empty = BinaryRandomAccessList<int>.Empty;

            AssertThrows<EmptyCollectionException>(() =>
                empty.Tail());
        }

        [TestMethod]
        [UnitTest]
        public void TailOnSingletonRandomAccessList()
        {
            var empty = BinaryRandomAccessList<int>.Empty;

            Assert.AreEqual(
                BinaryRandomAccessList<int>.Empty,
                empty.Cons(1).Tail());
        }

        [TestMethod]
        [UnitTest]
        public void LookupWithIndexOutOfUpperRangeLimit()
        {
            var empty = BinaryRandomAccessList<int>.Empty;

            AssertThrows<IndexOutOfRangeException>(() =>
            { var xs = empty.Cons(1)[1]; });
        }

        [TestMethod]
        [UnitTest]
        public void LookupWithIndexOutOfLowerRangeLimit()
        {
            var empty = BinaryRandomAccessList<int>.Empty;

            AssertThrows<IndexOutOfRangeException>(() =>
            { var xs = empty.Cons(1)[-1]; });
        }

        [TestMethod]
        [UnitTest]
        public void LookupWithIndexInRange()
        {
            var empty = BinaryRandomAccessList<int>.Empty;

            Assert.AreEqual(
                99,
                empty.Cons(17).Cons(25).Cons(99)[0],
                "Lookup of element at position 0 failed.");

            Assert.AreEqual(
                25,
                empty.Cons(17).Cons(25).Cons(99)[1],
                "Lookup of element at position 1 failed.");

            Assert.AreEqual(
                17,
                empty.Cons(17).Cons(25).Cons(99)[2],
                "Lookup of element at position 2 failed.");
        }

        [TestMethod]
        [UnitTest]
        public void UpdateWithIndexOutOfUpperRangeLimit()
        {
            var empty = BinaryRandomAccessList<int>.Empty;

            AssertThrows<IndexOutOfRangeException>(() =>
            {
                var xs = empty.Cons(1).Update(1, 17);
            });
        }

        [TestMethod]
        [UnitTest]
        public void UpdateWithIndexOutOfLowerRangeLimit()
        {
            var empty = BinaryRandomAccessList<int>.Empty;

            AssertThrows<IndexOutOfRangeException>(() =>
            { var xs = empty.Cons(1).Update(-1, 17); });
        }

        [TestMethod]
        [UnitTest]
        public void UpdateWithIndexInRange()
        {
            var empty = BinaryRandomAccessList<int>.Empty;

            Assert.AreEqual(
                99,
                empty.Cons(17).Cons(25).Cons(99).Update(1, 26)[0],
                "Update changed element at position 0.");

            Assert.AreEqual(
                26,
                empty.Cons(17).Cons(25).Cons(99).Update(1, 26)[1],
                "Element at position 1 not updated.");

            Assert.AreEqual(
                17,
                empty.Cons(17).Cons(25).Cons(99).Update(1, 26)[2],
                "Update changed element at position 2.");
        }
    }
}
