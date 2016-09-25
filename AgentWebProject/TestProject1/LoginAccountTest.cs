using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DhrAgentDatabaseUtils;
using System.Xml.Linq;
using System.Linq;

namespace TestProject1
{
    [TestClass]
    public class LoginAccountTest
    {
        [TestMethod]
        public void TestAccountFunction()
        {
            var vinc = "vincent Qiu";
            var db = new DBConn();
            var account = new LoginMgr();
            var vincent = db.LoginUsers.SingleOrDefault(c => c.Username.ToUpper() == vinc.ToUpper());
            if (vincent != null)
            {
                db.LoginUsers.DeleteOnSubmit(vincent);
                db.SubmitChanges();
            }
            Assert.IsTrue(account.CreateUser("vincent qiu", "123456", "127.0.0.1"));
            Assert.IsFalse(account.CreateUser("vincent qiu", "123456", "127.0.0.1"));
            Assert.IsTrue(account.Login("vincent qiu","123456","127.0.0.1"));
            Assert.IsFalse(account.Login("vincent ppiu","123456","127.0.0.1"));
            Assert.IsFalse(account.Login("vincent qiu","156","127.0.0.1"));
            Assert.IsFalse(account.ChangePassword("vincent qiu","12356","789456123","127.0.0.1"));
            Assert.IsTrue(account.ChangePassword("vincent qiu","123456","789456123","127.0.0.1"));
        }
    }
}
