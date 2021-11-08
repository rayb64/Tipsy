// <Copyright>Copyright (c) Birklid Software. All rights reserved.</Copyright>
// <File>TestBase.cs</File>
// <Company>Birklid Software</Company>
// <Author>Ray Birklid</Author>
// <Email>Ray.Birklid@gmail.com</Email>
// <Date>11/7/2021 9:54:18 PM</Date>
namespace Com.Gmail.Birklid.Ray.Tipsy.Test
{
    using Com.Gmail.Birklid.Ray.Tipsy.Diagnostics;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;

    [TestClass]
    public class TestBase
    {
        #region Private Fields

        private static int _failures = 0;
        private static Logger _log = TestTraceSource.Instance;
        private TestContext _testContext;
        private static int _testCount;

        #endregion Private Fields

        public static Logger Log => _log;

        public TestContext TestContext
        {
            get => _testContext;
            set => _testContext = value;
        }

        [AssemblyCleanup()]
        public static void AssemblyCleanup()
        {
            _log.Information("{0} tests run", _testCount);
            //if (_failures == 0)
            //{
            //    MessageBox.Show("All tests passed!");
            //}
            //else
            //{
            //    MessageBox.Show("There were test failures.  See the log file for deatils.");
            //}
        }

        [TestCleanup]
        public virtual void TestCleanup()
        {
            _log.Information("Test complete: {0}:{1}:{2}", GetType().FullName, TestContext.TestName, TestContext.CurrentTestOutcome);
            ++_testCount;
            if (TestContext.CurrentTestOutcome != UnitTestOutcome.Passed)
            {
                _failures++;
            }
            //CoreConfiguration.Instance.ModuleController.Shutdown();
        }

        [TestInitialize]
        public virtual void TestInitialize()
        {
            var x = AppDomain.CurrentDomain;
            _log.Information("Starting test: {0}:{1}", GetType().FullName, TestContext.TestName);
        }
    }
}
