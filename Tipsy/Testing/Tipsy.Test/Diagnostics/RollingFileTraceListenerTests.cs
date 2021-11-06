// <Copyright>Copyright (c) Birklid Software. All rights reserved.</Copyright>
// <File>RollingFileTraceListenerTests.cs</File>
// <Company>Birklid Software</Company>
// <Author>Ray Birklid</Author>
// <Email>Ray.Birklid@gmail.com</Email>
// <Date>11/5/2021 8:17:11 PM</Date>
namespace Com.Gmail.Birklid.Ray.Tipsy.Test.Diagnostics
{
    using Com.Gmail.Birklid.Ray.Tipsy.Diagnostics;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;

    // TODO: Revisit these tests.
    //       - Ensure they are writing to the file at the expected path.
    //       - Ensure they are writing the correct content.


    [TestClass]
    public class RollingFileTraceListenerTests
    {
        [TestMethod]
        public void InitializeWithEmptyString()
        {
            using (var item = new RollingFileTraceListener(string.Empty))
            {
                item.WriteLine("some test output - this file is safe to delete.");
            }
        }

        [TestMethod]
        public void InitializeWithNull()
        {
            using (var item = new RollingFileTraceListener(null))
            {
                item.WriteLine("some test output - this file is safe to delete.");
            }
        }

        [TestMethod]
        public void SecondInstance()
        {
            using (var item0 = new RollingFileTraceListener(@"c:\temp\testing\logs\Tipsy.Test.log"))
            {
                using (var item1 = new RollingFileTraceListener(@"c:\temp\testing\logs\Tipsy.Test.log"))
                {
                    item1.WriteLine("some test output - this file is safe to delete.");
                    Console.WriteLine(item1.FileName);
                }
                item0.WriteLine("some test output - this file is safe to delete.");
            }
        }

        [TestMethod]
        public void StandardUse_RelativePath()
        {
            using (var item = new RollingFileTraceListener("Tipsy.Test.log"))
            {
                item.WriteLine("some test output - this file is safe to delete.");
            }
        }

        [TestMethod]
        public void StandardUse()
        {
            using (var item = new RollingFileTraceListener(@"c:\temp\testing\logs\Tipsy.Test.log"))
            {
                item.WriteLine("some test output - this file is safe to delete.");
            }
        }
    }
}
