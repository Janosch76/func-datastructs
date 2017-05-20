namespace FunctionalDataStructures.Test.UnitTests
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class TestBase
    {
        public static T AssertThrows<T>(Action block) where T : Exception
        {
            try
            {
                block();
            }
            catch (T e)
            {
                return e;
            }
            catch (Exception e)
            {
                Assert.Fail(
                    "Expected exception of type {0}, but an exception of type {1} was thrown.",
                    typeof(T).Name,
                    e.GetType().Name);
                return null;
            }

            Assert.Fail(
                "Expected exception of type {0}, but no exception was thrown.",
                typeof(T).Name);
            return null;
        }
    }
}
