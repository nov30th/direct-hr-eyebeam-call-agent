using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DhrAgentDatabaseUtils;

namespace TestProject1
{
    [TestClass]
    public class DevelopmentUserTest
    {
        [TestMethod]
        public void TestDevelopmentUsers()
        {
            var isAdmin = new DevelopmentMgr().IsDevelopmentUser("Vincent Qiu");
            Assert.IsTrue(isAdmin);
            isAdmin = new DevelopmentMgr().IsDevelopmentUser("Vincent iu");
            Assert.IsFalse(isAdmin);
            Assert.IsTrue(new DevelopmentMgr().IsIgnoreAllBlocing("Vincent Qiu"));
        }
    }
}
