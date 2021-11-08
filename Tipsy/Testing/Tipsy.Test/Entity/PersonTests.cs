// <Copyright>Copyright (c) Birklid Software. All rights reserved.</Copyright>
// <File>PersonTests.cs</File>
// <Company>Birklid Software</Company>
// <Author>Ray Birklid</Author>
// <Email>Ray.Birklid@gmail.com</Email>
// <Date>11/7/2021 10:29:34 PM</Date>
namespace Com.Gmail.Birklid.Ray.Tipsy.Test.Entity
{
    using Com.Gmail.Birklid.Ray.Tipsy.Entity;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;

    [TestClass]
    public class PersonTests
    {
        [TestMethod]
        public void CheckDefaults()
        {
            var person = new Person(1);
            Assert.AreEqual(1, person.Id);
            Assert.AreNotEqual(default(DateTime), person.Created);
            Assert.AreEqual(string.Empty, person.Name);
        }

        [TestMethod]
        public void TypicalUsage()
        {
            var person = new Person(1) { Name = "Eva" };
            Assert.AreEqual(1, person.Id);
            Assert.AreNotEqual(default(DateTime), person.Created);
            Assert.AreEqual("Eva", person.Name);
        }
    }
}
