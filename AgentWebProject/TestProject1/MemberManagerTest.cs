using DhrAgentDatabaseUtils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace TestProject1
{
    
    
    /// <summary>
    ///This is a test class for MemberManagerTest and is intended
    ///to contain all MemberManagerTest Unit Tests
    ///</summary>
    [TestClass()]
    public class MemberManagerTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for GetMembers
        ///</summary>
        [TestMethod()]
        public void GetMembersTest()
        {
            MemberManager target = new MemberManager(); // TODO: Initialize to an appropriate value
            List<DhrMember> expected = null; // TODO: Initialize to an appropriate value
            List<DhrMember> actual;
            actual = target.GetMembers();
            return;
        }

        [TestMethod()]
        public void GetOneDayOnlineStatus()
        {
            LoginRecordMgr mgr = new LoginRecordMgr();
            var allmemberstatus = mgr.GetAllOnlineStatus(DateTime.Today);
        }


        [TestMethod()]
        public void GetSystemOnlineTime()
        {
            LoginRecordMgr mgr = new LoginRecordMgr();
            var systemTime = mgr.GetSystemOnlineTime(DateTime.Now);
            var systemTime1 = mgr.GetSystemOnlineTime(DateTime.MinValue);
        }
    }
}
