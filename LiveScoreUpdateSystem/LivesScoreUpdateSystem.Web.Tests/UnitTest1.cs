using System;
using NUnit.Framework;
using LiveScoreUpdateSystem.Web.Controllers;

namespace LiveScoreUpdateSystem.Web.Tests
{
    [TestFixture]
    public class UnitTest1
    {
        [Test]
        public void TestMethod1()
        {
            var controller = new HomeController();

            Assert.AreEqual(1, 1);
        }
    }
}
