namespace FunctionalDataStructures.Test.UnitTests.Set
{
    using System;
    using FunctionalDataStructures.Set;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class RedBlackSetTests : TestBase
    {
        [TestMethod]
        [UnitTest]
        public void CountOfEmptySet()
        {
            var empty = RedBlackSet<int>.Empty;

            Assert.AreEqual(
                0,
                empty.Count);
        }

        [TestMethod]
        [UnitTest]
        public void CountOfSingletonSet()
        {
            var empty = RedBlackSet<int>.Empty;

            Assert.AreEqual(
                1,
                empty.Insert(1).Count);
        }

        [TestMethod]
        [UnitTest]
        public void CountOfNonemptySet()
        {
            var empty = RedBlackSet<int>.Empty;

            Assert.AreEqual(
                2,
                empty.Insert(1).Insert(2).Count);
        }

        [TestMethod]
        [UnitTest]
        public void CountIgnoresDuplicates()
        {
            var empty = RedBlackSet<int>.Empty;

            Assert.AreEqual(
                1,
                empty.Insert(1).Insert(1).Count);
        }

        [TestMethod]
        [UnitTest]
        public void IsEmptyOnEmptySet()
        {
            var empty = RedBlackSet<int>.Empty;

            Assert.AreEqual(
                true,
                empty.IsEmpty());
        }

        [TestMethod]
        [UnitTest]
        public void IsEmptyOnSingletonSet()
        {
            var empty = RedBlackSet<int>.Empty;

            Assert.AreEqual(
                false,
                empty.Insert(1).IsEmpty());
        }

        [TestMethod]
        [UnitTest]
        public void IsMemberOnEmptySet()
        {
            var empty = RedBlackSet<int>.Empty;

            Assert.AreEqual(
                false,
                empty.IsMember(5));
        }

        [TestMethod]
        [UnitTest]
        public void IsMemberOnNonemptySet()
        {
            var empty = RedBlackSet<int>.Empty;

            Assert.AreEqual(
                true,
                empty.Insert(2).Insert(5).Insert(3).IsMember(5));
        }

        [TestMethod]
        [UnitTest]
        public void IsNonMemberOnNonemptySet()
        {
            var empty = RedBlackSet<int>.Empty;

            Assert.AreEqual(
                false,
                empty.Insert(2).Insert(5).Insert(3).IsMember(4));
        }
    }
}
