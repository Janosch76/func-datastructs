namespace FunctionalDataStructures.Test.UnitTests.Utils
{
    using System;
    using FunctionalDataStructures.Utils;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class SuspTests : TestBase
    {
        [TestMethod]
        public void TestMethod1()
        {
            int evaluated = 0;
            Func<int> computation = () => { evaluated++; return 99; };

            var susp = new Susp<int>(computation);

            Assert.AreEqual(
                0,
                evaluated);
        }

        [TestMethod]
        public void TestMethod2()
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
        public void TestMethod3()
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

    }
}
