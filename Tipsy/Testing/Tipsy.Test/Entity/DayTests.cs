// <Copyright>Copyright (c) Birklid Software. All rights reserved.</Copyright>
// <File>DayTests.cs</File>
// <Company>Birklid Software</Company>
// <Author>Ray Birklid</Author>
// <Email>Ray.Birklid@gmail.com</Email>
// <Date>11/7/2021 10:11:58 PM</Date>
namespace Com.Gmail.Birklid.Ray.Tipsy.Test.Entity
{
    using Com.Gmail.Birklid.Ray.Tipsy.Entity;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.Linq;

    [TestClass]
    public class DayTests : TestBase
    {
        [TestMethod]
        public void CheckDefaults()
        {
            var date = DateTime.Today;
            var idFactory = new IdFactory();
            var day = new Day(idFactory, date);
            Assert.AreEqual(1, day.Id);
            Assert.AreNotEqual(default(DateTime), day.Created);
            Assert.AreEqual(date, day.Date);
            Assert.AreEqual(0, day.Entries.Count);
        }

        [TestMethod]
        public void TypicalUsage()
        {
            var idFactory = new IdFactory();
            var day = new Day(idFactory, DateTime.Today);
            var joe = new Person(idFactory.Next(typeof(Person))) { Name = "Joe" };
            var cliff = new Person(idFactory.Next(typeof(Person))) { Name = "Cliff" };
            var entry = day.Entries.CreateNew();
            entry.Action = DayAction.ClockOn;
            entry.Bank = 0;
            entry.BankDescription = "Joe is on";
            entry.ETips = 5;
            entry.Person = joe;
            entry.Time = DateTime.Parse("10:00 AM");

            entry = day.Entries.CreateNew();
            entry.Action = DayAction.ClockOff;
            entry.Bank = 10;
            entry.BankDescription = "Cliff is on";
            entry.ETips = 15;
            entry.Person = cliff;
            entry.Time = DateTime.Parse("2:00 PM");

            entry = day.Entries.First();
            Assert.AreEqual(DayAction.ClockOn, entry.Action);
            Assert.AreEqual(0, entry.Bank);
            Assert.AreEqual("Joe is on", entry.BankDescription);
            Assert.AreEqual(5, entry.ETips);
            Assert.AreEqual("Joe", entry.Person.Name);
            Assert.AreEqual(DateTime.Parse("10:00 AM"), entry.Time);

            entry = day.Entries.Last();
            Assert.AreEqual(DayAction.ClockOff, entry.Action);
            Assert.AreEqual(10, entry.Bank);
            Assert.AreEqual("Cliff is on", entry.BankDescription);
            Assert.AreEqual(15, entry.ETips);
            Assert.AreEqual("Cliff", entry.Person.Name);
            Assert.AreEqual(DateTime.Parse("2:00 PM"), entry.Time);

            Assert.AreEqual(2, day.Entries.Count);
        }
    }
}
