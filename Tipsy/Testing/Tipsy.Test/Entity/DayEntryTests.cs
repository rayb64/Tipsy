// <Copyright>Copyright (c) Birklid Software. All rights reserved.</Copyright>
// <File>DayEntryTests.cs</File>
// <Company>Birklid Software</Company>
// <Author>Ray Birklid</Author>
// <Email>Ray.Birklid@gmail.com</Email>
// <Date>11/7/2021 10:04:36 PM</Date>
namespace Com.Gmail.Birklid.Ray.Tipsy.Test.Entity
{
    using Com.Gmail.Birklid.Ray.Tipsy.Entity;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;

    [TestClass]
    public class DayEntryTests : TestBase
    {
        [TestMethod]
        public void CheckDefaults()
        {
            var idFactory = new IdFactory();
            var entry = new DayEntry(idFactory.Next(typeof(DayEntry)));
            Assert.AreEqual(1, entry.Id);
            Assert.AreNotEqual(default(DateTime), entry.Created);
            Assert.AreEqual(DayAction.Undefined, entry.Action);
            Assert.AreEqual(0, entry.Bank);
            Assert.AreEqual(string.Empty, entry.BankDescription);
            Assert.AreEqual(0, entry.ETips);
            Assert.IsNull(entry.Person);
        }

        [TestMethod]
        public void TypicalUsage()
        {
            var idFactory = new IdFactory();
            var entry = new DayEntry(idFactory.Next(typeof(DayEntry)))
            {
                Action = DayAction.ClockOn,
                Bank = 10,
                BankDescription = "not important",
                ETips = 22
            };
            Assert.AreEqual(1, entry.Id);
            Assert.AreNotEqual(default(DateTime), entry.Created);
            Assert.AreEqual(DayAction.ClockOn, entry.Action);
            Assert.AreEqual(10, entry.Bank);
            Assert.AreEqual("not important", entry.BankDescription);
            Assert.AreEqual(22, entry.ETips);
            Assert.IsNull(entry.Person);
            entry.Person = new Person(idFactory.Next(typeof(Person))) { Name = "Jim" };
            Assert.AreEqual("Jim", entry.Person.Name);
        }
    }
}
