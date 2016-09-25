using DHRSoftphone.Plugins.ProcessKiller;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestProject
{
    /// <summary>
    ///This is a test class for ProcessControllerTest and is intended
    ///to contain all ProcessControllerTest Unit Tests
    ///</summary>
    [TestClass()]
    public class ProcessControllerTest
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

        #endregion Additional test attributes

        /// <summary>
        ///A test for GetAllWindows
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DHRSoftphone.Plugins.ProcessKiller.dll")]
        public void GetAllWindowsTest()
        {
            ProcessController_Accessor target = new ProcessController_Accessor(); // TODO: Initialize to an appropriate value
            target.Refresh();

            //target.KillProcess("fetion", true);
            target.KillWindow("飞信", false);

            // Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }
    }
}