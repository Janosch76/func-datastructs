namespace FunctionalDataStructures.Test.UnitTests.SortableCollection
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using FunctionalDataStructures.SortableCollection;
    using System.Linq;

    [TestClass]
    public class BottomUpMergesortTests
    {
        [TestMethod]
        [UnitTest]
        public void CountOfEmptySet()
        {
            var empty = BottomUpMergesort<int>.Empty;

            Assert.AreEqual(
                0,
                empty.Count);
        }

        [TestMethod]
        [UnitTest]
        public void CountOfSingletonSet()
        {
            var empty = BottomUpMergesort<int>.Empty;

            Assert.AreEqual(
                1,
                empty.Add(1).Count);
        }

        [TestMethod]
        [UnitTest]
        public void CountOfNonemptySet()
        {
            var empty = BottomUpMergesort<int>.Empty;

            Assert.AreEqual(
                2,
                empty.Add(1).Add(2).Count);
        }

        [TestMethod]
        [UnitTest]
        public void EnumerationOfCollectionIsComplete()
        {
            var empty = BottomUpMergesort<int>.Empty;
            var collection = empty.Add(3).Add(1).Add(2).Add(4);

            var elements = collection.ToArray();

            Assert.AreEqual(4, elements.Count());
            Assert.IsTrue(elements.Contains(1));
            Assert.IsTrue(elements.Contains(2));
            Assert.IsTrue(elements.Contains(3));
            Assert.IsTrue(elements.Contains(4));
        }

        [TestMethod]
        [UnitTest]
        public void SortReturnsEnumerationOfCollection()
        {
            var empty = BottomUpMergesort<int>.Empty;
            var collection = empty.Add(3).Add(1).Add(2).Add(4);

            var elements = collection.Sort().ToArray();

            Assert.AreEqual(4, elements.Count());
            Assert.AreEqual(1, elements[0]);
            Assert.AreEqual(2, elements[1]);
            Assert.AreEqual(3, elements[2]);
            Assert.AreEqual(4, elements[3]);
        }
    }
}
