using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DHRSoftphone.Plugins.DNSFixer;

namespace TestProject
{
    [TestClass]
    public class DNSFixerTest
    {
        [TestMethod]
        public void TestFlushDNS()
        {
            LocalHostsController.FlushDNS();
        }
    }
}
