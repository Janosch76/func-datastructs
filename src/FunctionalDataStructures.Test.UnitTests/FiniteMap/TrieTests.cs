﻿namespace FunctionalDataStructures.Test.UnitTests.FiniteMap
{
    using System;
    using FunctionalDataStructures.FiniteMap;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class TrieTests : TestBase
    {
        [TestMethod]
        [UnitTest]
        public void CountOfEmptyMap()
        {
            var empty = Trie<int>.Empty;

            Assert.AreEqual(
                0,
                empty.Count);
        }

        [TestMethod]
        [UnitTest]
        public void CountOfSingletonMap()
        {
            var empty = Trie<int>.Empty;

            Assert.AreEqual(
                1,
                empty.Bind("key1", 1).Count);
        }

        [TestMethod]
        [UnitTest]
        public void CountOfNonemptyMap()
        {
            var empty = Trie<int>.Empty;

            Assert.AreEqual(
                2,
                empty.Bind("key1", 1).Bind("key2", 2).Count);
        }

        [TestMethod]
        [UnitTest]
        public void DuplicateBindingsNotCounted()
        {
            var empty = Trie<int>.Empty;

            Assert.AreEqual(
                1,
                empty.Bind("key1", 1).Bind("key1", 2).Count);
        }

        [TestMethod]
        [UnitTest]
        public void LookupInEmptyMapThrowsException()
        {
            var empty = Trie<int>.Empty;

            AssertThrows<NotFoundException>(() =>
                empty.Lookup("key"));
        }

        [TestMethod]
        [UnitTest]
        public void LookupOfExistingKey()
        {
            var empty = Trie<int>.Empty;
            var map = empty.Bind("key1", 1).Bind("key2", 2).Bind("key3", 3);

            Assert.AreEqual(
                2,
                map.Lookup("key2"));
        }

        [TestMethod]
        [UnitTest]
        public void LookupOfNonexistingKey()
        {
            var empty = Trie<int>.Empty;
            var map = empty.Bind("key1", 1).Bind("key2", 2).Bind("key3", 3);

            AssertThrows<NotFoundException>(() =>
                map.Lookup("invalidKey"));
        }
    }
}
