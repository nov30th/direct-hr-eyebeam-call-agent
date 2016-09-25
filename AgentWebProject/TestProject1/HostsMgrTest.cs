using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestProject1
{
    [TestClass]
    public class HostsMgrTest
    {
        [TestMethod]
        public void TestLoadCurrentTimePeriodName()
        {
            DhrAgentDatabaseUtils.HostsMgr mgr = new DhrAgentDatabaseUtils.HostsMgr();
            var time = mgr.LoadCurrentTimePeriodName();
            return;
        }


        [TestMethod]
        public void TestHostsGet()
        {
            DhrAgentDatabaseUtils.HostsMgr mgr = new DhrAgentDatabaseUtils.HostsMgr();
            var hosts = mgr.GetHosts("Vincent Qiu", "Ningbo", true);
            return;
        }
    }
}
