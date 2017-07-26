namespace FunctionalDataStructures.Test.UnitTests.FiniteMap
{
    using System;
    using FunctionalDataStructures.FiniteMap;
    using FunctionalDataStructures.Utils;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class TrieOfTreesTests : TestBase
    {
        [TestMethod]
        [UnitTest]
        public void CountOfEmptyMap()
        {
            var empty = TrieOfTrees<int,int>.Empty;

            Assert.AreEqual(
                0,
                empty.Count);
        }

        [TestMethod]
        [UnitTest]
        public void CountOfSingletonMap()
        {
            var empty = TrieOfTrees<int, int>.Empty;
            var key1 = BinaryTree<int>.MakeTree(1,
                BinaryTree<int>.MakeTree(0, BinaryTree<int>.Empty, BinaryTree<int>.Empty),
                BinaryTree<int>.MakeTree(2, BinaryTree<int>.Empty, BinaryTree<int>.Empty));

            Assert.AreEqual(
                1,
                empty.Bind(key1, 1).Count);
        }

        [TestMethod]
        [UnitTest]
        public void CountOfNonemptyMap()
        {
            var empty = TrieOfTrees<int, int>.Empty;
            var key1 = BinaryTree<int>.MakeTree(1,
                BinaryTree<int>.MakeTree(0, BinaryTree<int>.Empty, BinaryTree<int>.Empty),
                BinaryTree<int>.MakeTree(2, BinaryTree<int>.Empty, BinaryTree<int>.Empty));
            var key2 = BinaryTree<int>.MakeTree(2,
                BinaryTree<int>.MakeTree(0, BinaryTree<int>.Empty, BinaryTree<int>.Empty),
                BinaryTree<int>.MakeTree(2, BinaryTree<int>.Empty, BinaryTree<int>.Empty));

            Assert.AreEqual(
                2,
                empty.Bind(key1, 1).Bind(key2, 2).Count);
        }

        [TestMethod]
        [UnitTest]
        public void DuplicateBindingsNotCounted()
        {
            var empty = TrieOfTrees<int, int>.Empty;
            var key1 = BinaryTree<int>.MakeTree(1,
                BinaryTree<int>.MakeTree(0, BinaryTree<int>.Empty, BinaryTree<int>.Empty),
                BinaryTree<int>.MakeTree(2, BinaryTree<int>.Empty, BinaryTree<int>.Empty));

            Assert.AreEqual(
                1,
                empty.Bind(key1, 1).Bind(key1, 2).Count);
        }

        [TestMethod]
        [UnitTest]
        public void LookupInEmptyMapThrowsException()
        {
            var empty = TrieOfTrees<int, int>.Empty;
            var key1 = BinaryTree<int>.MakeTree(1,
                BinaryTree<int>.MakeTree(0, BinaryTree<int>.Empty, BinaryTree<int>.Empty),
                BinaryTree<int>.MakeTree(2, BinaryTree<int>.Empty, BinaryTree<int>.Empty));

            AssertThrows<NotFoundException>(() =>
                empty.Lookup(key1));
        }

        [TestMethod]
        [UnitTest]
        public void LookupOfExistingKey()
        {
            var empty = TrieOfTrees<int, int>.Empty;
            var key1 = BinaryTree<int>.MakeTree(1,
                BinaryTree<int>.MakeTree(0, BinaryTree<int>.Empty, BinaryTree<int>.Empty),
                BinaryTree<int>.MakeTree(2, BinaryTree<int>.Empty, BinaryTree<int>.Empty));
            var key2 = BinaryTree<int>.MakeTree(2,
                BinaryTree<int>.MakeTree(0, BinaryTree<int>.Empty, BinaryTree<int>.Empty),
                BinaryTree<int>.MakeTree(2, BinaryTree<int>.Empty, BinaryTree<int>.Empty));
            var key3 = BinaryTree<int>.MakeTree(3,
                BinaryTree<int>.MakeTree(0, BinaryTree<int>.Empty, BinaryTree<int>.Empty),
                BinaryTree<int>.MakeTree(2, BinaryTree<int>.Empty, BinaryTree<int>.Empty));

            var map = empty.Bind(key1, 1).Bind(key2, 2).Bind(key3, 3);

            Assert.AreEqual(
                2,
                map.Lookup(key2));
        }

        [TestMethod]
        [UnitTest]
        public void LookupOfNonexistingKey()
        {
            var empty = TrieOfTrees<int, int>.Empty;
            var key1 = BinaryTree<int>.MakeTree(1,
                BinaryTree<int>.MakeTree(0, BinaryTree<int>.Empty, BinaryTree<int>.Empty),
                BinaryTree<int>.MakeTree(2, BinaryTree<int>.Empty, BinaryTree<int>.Empty));
            var key2 = BinaryTree<int>.MakeTree(2,
                BinaryTree<int>.MakeTree(0, BinaryTree<int>.Empty, BinaryTree<int>.Empty),
                BinaryTree<int>.MakeTree(2, BinaryTree<int>.Empty, BinaryTree<int>.Empty));
            var key3 = BinaryTree<int>.MakeTree(3,
                BinaryTree<int>.MakeTree(0, BinaryTree<int>.Empty, BinaryTree<int>.Empty),
                BinaryTree<int>.MakeTree(2, BinaryTree<int>.Empty, BinaryTree<int>.Empty));

            var invalidKey = BinaryTree<int>.MakeTree(2, BinaryTree<int>.Empty, BinaryTree<int>.Empty);

            var map = empty.Bind(key1, 1).Bind(key2, 2).Bind(key3, 3);

            AssertThrows<NotFoundException>(() =>
                empty.Lookup(invalidKey));
        }
    }
}
