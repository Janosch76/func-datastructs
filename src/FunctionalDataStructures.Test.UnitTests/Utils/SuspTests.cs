namespace FunctionalDataStructures.Test.UnitTests.Utils
{
    using System;
    using FunctionalDataStructures.Utils;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class SuspTests : TestBase
    {
        [TestMethod]
        [UnitTest]
        public void UnusedResultsNotEvaluated()
        {
            int evaluated = 0;
            Func<int> computation = () => { evaluated++; return 99; };

            var susp = new Susp<int>(computation);

            Assert.AreEqual(
                0,
                evaluated);
        }

        [TestMethod]
        [UnitTest]
        public void UsedResultEvaluated()
        {
            int evaluated = 0;
            Func<int> computation = () => { evaluated++; return 99; };

            var susp = new Susp<int>(computation);
            var result = susp.Force();

            Assert.AreEqual(
                1,
                evaluated);
        }

        [TestMethod]
        [UnitTest]
        public void UsedResultEvaluatedOnlyOnce()
        {
            int evaluated = 0;
            Func<int> computation = () => { evaluated++; return 99; };

            var susp = new Susp<int>(computation);
            var result1 = susp.Force();
            var result2 = susp.Force();

            Assert.AreEqual(
                1,
                evaluated);
        }

        [TestMethod]
        [UnitTest]
        public void UsedTransformedResultEvaluatedOnlyOnce()
        {
            int evaluated = 0;
            Func<int> computation = () => { evaluated++; return 99; };
            Func<int, int> f = x => x + 1;

            var susp = (new Susp<int>(computation)).Select(f);
            var result1 = susp.Force();
            var result2 = susp.Force();

            Assert.AreEqual(
                1,
                evaluated);
        }
    }
}
