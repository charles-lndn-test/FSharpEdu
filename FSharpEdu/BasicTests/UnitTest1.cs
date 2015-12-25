using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Basics;

namespace BasicTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var result = Basics101.add(1, 2);
            Assert.AreEqual(3, result);
        }
    }
}
