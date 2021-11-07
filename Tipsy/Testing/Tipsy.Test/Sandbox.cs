// <Copyright>Copyright (c) Birklid Software. All rights reserved.</Copyright>
// <File>Sandbox.cs</File>
// <Company>Birklid Software</Company>
// <Author>Ray Birklid</Author>
// <Email>Ray.Birklid@gmail.com</Email>
// <Date>11/7/2021 7:36:43 AM</Date>
namespace Com.Gmail.Birklid.Ray.Tipsy.Test
{
    using Com.Gmail.Birklid.Ray.Tipsy;
    using Com.Gmail.Birklid.Ray.Tipsy.Entity;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    [TestClass]
    public class Sandbox
    {
        [TestMethod]
        public void WorkingWithEntities()
        {
            var ent = new Entities() as IEntities;
            var joe = ent.People.CreateNew("Joe");
            Assert.AreEqual(1, joe.Id);
            Assert.AreEqual("Joe", joe.Name);
            var ray = ent.People.CreateNew("Ray");
            Assert.AreEqual(2, ray.Id);
            Assert.AreEqual("Ray", ray.Name);
            var eva = ent.People.CreateNew("Eva");
            Assert.AreEqual(3, eva.Id);
            Assert.AreEqual("Eva", eva.Name);
            var e2 = SerializationTests.Clone(ent);
            var julie = e2.People.CreateNew("Julie");
            Assert.AreEqual(4, julie.Id);
            Assert.AreEqual("Julie", julie.Name);

            Assert.IsFalse(ent.People.Contains(julie));
            Assert.IsTrue(e2.People.Contains(julie));
            Assert.AreEqual(3, e2.People.Single(e => e.Name == "Eva").Id);

            Assert.AreEqual(0, ent.Days.Count);
            var today = ent.Days.Today;
            Assert.AreEqual(1, ent.Days.Count);

            // Simulate entries for a typical day:
            
            // First, Ray clocks on
            var entry = today.Entries.CreateNew();
            entry.Action = DayAction.ClockOn;
            entry.ETips = 0;
            entry.Person = ent.People.Single(e => e.Name == "Ray");
            entry.Time = DateTime.Parse("November 7, 2021 10:00 AM");

            // Next, Joe clocks on
            entry = today.Entries.CreateNew();
            entry.Action = DayAction.ClockOn;
            entry.ETips = 50;
            entry.Person = ent.People.Single(e => e.Name == "Joe");
            entry.Time = DateTime.Parse("November 7, 2021 2:00 PM");

            // Then, Ray clocks off
            entry = today.Entries.CreateNew();
            entry.Action = DayAction.ClockOff;
            entry.ETips = 250;
            entry.Person = ent.People.Single(e => e.Name == "Ray");
            entry.Time = DateTime.Parse("November 7, 2021 6:00 PM");

            // Finally, Joe clocks off
            entry = today.Entries.CreateNew();
            entry.Action = DayAction.ClockOff;
            entry.ETips = 310;
            entry.Person = ent.People.Single(e => e.Name == "Joe");
            entry.Time = DateTime.Parse("November 7, 2021 10:00 PM");
        
            // Check the results on a clone:
            e2 = SerializationTests.Clone(ent);
            Assert.AreEqual(3, e2.People.Count);
            Assert.IsNotNull(e2.People.SingleOrDefault(e => e.Name == "Eva"));
            Assert.IsNotNull(e2.People.SingleOrDefault(e => e.Name == "Ray"));
            Assert.IsNotNull(e2.People.SingleOrDefault(e => e.Name == "Joe"));
            Assert.AreEqual(1, e2.Days.Count);
            Assert.AreEqual(4, e2.Days.Single().Entries.Count);
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
