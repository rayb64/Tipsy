// <Copyright>Copyright (c) Birklid Software. All rights reserved.</Copyright>
// <File>EntitiesTests.cs</File>
// <Company>Birklid Software</Company>
// <Author>Ray Birklid</Author>
// <Email>Ray.Birklid@gmail.com</Email>
// <Date>11/7/2021 10:32:03 PM</Date>
namespace Com.Gmail.Birklid.Ray.Tipsy.Test.Entity
{
    using Com.Gmail.Birklid.Ray.Tipsy.Entity;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.Linq;

    [TestClass]
    public class EntitiesTests : TestBase
    {
        [TestMethod]
        public void CheckDefaults()
        {
            var entities = new Entities();
            Assert.AreEqual(0, entities.Days.Count());
            Assert.AreEqual(0, entities.People.Count());
        }

        [TestMethod]
        public void TypicalUsage()
        {
            var ent = new Entities() as IEntities;
            var joe = new Person(ent.IdFactory.Next(typeof(Person))) { Name = "Joe" };
            var eva = new Person(ent.IdFactory.Next(typeof(Person))) { Name = "Eva" };
            var ray = new Person(ent.IdFactory.Next(typeof(Person))) { Name = "Ray" };
            var julie = new Person(ent.IdFactory.Next(typeof(Person))) { Name = "Julie" };
            ent.People = new[] { joe, eva, ray, julie };
            var day = new Day(ent.IdFactory.Next(typeof(Day)), DateTime.Today);
            day.Entries = new[]
            {
                new DayEntry(ent.IdFactory.Next(typeof(DayEntry)))
                { Action = DayAction.ClockOn, ETips = 0, Person = ray, Time = DateTime.Parse("November 7, 2021 10:00 AM") },
                new DayEntry(ent.IdFactory.Next(typeof(DayEntry)))
                { Action = DayAction.ClockOn, ETips = 50, Person = joe, Time = DateTime.Parse("November 7, 2021 2 PM") },
                new DayEntry(ent.IdFactory.Next(typeof(DayEntry)))
                { Action = DayAction.ClockOff, ETips = 250, Person = ray, Time = DateTime.Parse("November 7, 2021 6:00 PM") },
                new DayEntry(ent.IdFactory.Next(typeof(DayEntry)))
                { Action = DayAction.ClockOff, ETips = 310, Person = joe, Time = DateTime.Parse("November 7, 2021 10:00 PM") }
            };
            ent.Days = new[] { day };

            // Check the results on a clone:
            var e2 = SerializationTests.Clone(ent);
            Assert.AreEqual(4, e2.People.Count());
            Assert.IsNotNull(e2.People.SingleOrDefault(e => e.Name == "Eva"));
            Assert.IsNotNull(e2.People.SingleOrDefault(e => e.Name == "Ray"));
            Assert.IsNotNull(e2.People.SingleOrDefault(e => e.Name == "Joe"));
            Assert.AreEqual(1, e2.Days.Count());
            Assert.AreEqual(4, e2.Days.Single().Entries.Count());
            Assert.IsTrue(e2.Days.Single().Entries.Select(e => e.Time.Date).All(e => e.Year == 2021 && e.Month == 11 && e.Day == 7));

            // Verify the entries (1)
            var current = e2.Days.Single().Entries.First();
            Assert.AreEqual(DayAction.ClockOn, current.Action);
            Assert.AreEqual(0, current.ETips);
            Assert.AreEqual("Ray", current.Person.Name);
            Assert.AreEqual(10, current.Time.Hour);
            Assert.AreEqual(0, current.Time.Minute);

            // Verify the entries (2)
            current = e2.Days.Single().Entries.Skip(1).First();
            Assert.AreEqual(DayAction.ClockOn, current.Action);
            Assert.AreEqual(50, current.ETips);
            Assert.AreEqual("Joe", current.Person.Name);
            Assert.AreEqual(14, current.Time.Hour);
            Assert.AreEqual(0, current.Time.Minute);

            // Verify the entries (3)
            current = e2.Days.Single().Entries.Skip(2).First();
            Assert.AreEqual(DayAction.ClockOff, current.Action);
            Assert.AreEqual(250, current.ETips);
            Assert.AreEqual("Ray", current.Person.Name);
            Assert.AreEqual(18, current.Time.Hour);
            Assert.AreEqual(0, current.Time.Minute);

            // Verify the entries (4)
            current = e2.Days.Single().Entries.Skip(3).First();
            Assert.AreEqual(DayAction.ClockOff, current.Action);
            Assert.AreEqual(310, current.ETips);
            Assert.AreEqual("Joe", current.Person.Name);
            Assert.AreEqual(22, current.Time.Hour);
            Assert.AreEqual(0, current.Time.Minute);
        }
    }
}
