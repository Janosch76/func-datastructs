namespace FunctionalDataStructures.Test.UnitTests.Set
{
    using System;
    using System.Linq;
    using FunctionalDataStructures.Set;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class UnbalancedSetTests : TestBase
    {
        [TestMethod]
        [UnitTest]
        public void CountOfEmptySet()
        {
            var empty = UnbalancedSet<int>.Empty;

            Assert.AreEqual(
                0,
                empty.Count);
        }

        [TestMethod]
        [UnitTest]
        public void CountOfSingletonSet()
        {
            var empty = UnbalancedSet<int>.Empty;

            Assert.AreEqual(
                1,
                empty.Insert(1).Count);
        }

        [TestMethod]
        [UnitTest]
        public void CountOfNonemptySet()
        {
            var empty = UnbalancedSet<int>.Empty;

            Assert.AreEqual(
                2,
                empty.Insert(1).Insert(2).Count);
        }

        [TestMethod]
        [UnitTest]
        public void CountIgnoresDuplicates()
        {
            var empty = UnbalancedSet<int>.Empty;

            Assert.AreEqual(
                1,
                empty.Insert(1).Insert(1).Count);
        }

        [TestMethod]
        [UnitTest]
        public void IsEmptyOnEmptySet()
        {
            var empty = UnbalancedSet<int>.Empty;

            Assert.AreEqual(
                true,
                empty.IsEmpty());
        }

        [TestMethod]
        [UnitTest]
        public void IsEmptyOnSingletonSet()
        {
            var empty = UnbalancedSet<int>.Empty;

            Assert.AreEqual(
                false,
                empty.Insert(1).IsEmpty());
        }

        [TestMethod]
        [UnitTest]
        public void IsMemberOnEmptySet()
        {
            var empty = UnbalancedSet<int>.Empty;

            Assert.AreEqual(
                false,
                empty.IsMember(5));
        }

        [TestMethod]
        [UnitTest]
        public void IsMemberOnNonemptySet()
        {
            var empty = UnbalancedSet<int>.Empty;

            Assert.AreEqual(
                true,
                empty.Insert(2).Insert(5).Insert(3).IsMember(5));
        }

        [TestMethod]
        [UnitTest]
        public void IsNonMemberOnNonemptySet()
        {
            var empty = UnbalancedSet<int>.Empty;

            Assert.AreEqual(
                false,
                empty.Insert(2).Insert(5).Insert(3).IsMember(4));
        }

        [TestMethod]
        [UnitTest]
        public void OrderedEnumerationOfSet()
        {
            var set = UnbalancedSet<int>.Empty.Insert(3).Insert(1).Insert(2);  // {3,1,2}
            var elements = set.ToArray();

            Assert.AreEqual(3, elements.Length);
            Assert.AreEqual(1, elements[0]);
            Assert.AreEqual(2, elements[1]);
            Assert.AreEqual(3, elements[2]);
        }
    }
}
