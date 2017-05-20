namespace FunctionalDataStructures.Test.UnitTests
{
    using System.Collections.Generic;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    public class UnitTestAttribute : TestCategoryBaseAttribute
    {
        public override IList<string> TestCategories
        {
            get
            {
                return new List<string>() { "UnitTest" };
            }
        }
    }
}
