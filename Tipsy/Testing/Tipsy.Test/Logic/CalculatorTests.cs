// <Copyright>Copyright (c) Birklid Software. All rights reserved.</Copyright>
// <File>CalculatorTests.cs</File>
// <Company>Birklid Software</Company>
// <Author>Ray Birklid</Author>
// <Email>Ray.Birklid@gmail.com</Email>
// <Date>11/7/2021 10:36:13 PM</Date>
namespace Com.Gmail.Birklid.Ray.Tipsy.Test.Logic
{
    using Com.Gmail.Birklid.Ray.Tipsy.Entity;
    using Com.Gmail.Birklid.Ray.Tipsy.Logic;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.Linq;

    [TestClass]
    public class CalculatorTests
    {
        [TestMethod]
        public void TypicalUsage()
        {
            var ent = new Entities() as IEntities;
            var calculator = new Calculator();
            var today = ent.Days.Today;
            var finalTotal = 310;

            ent.People.CreateNew("Joe");
            ent.People.CreateNew("Ray");

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
            entry.ETips = finalTotal;
            entry.Person = ent.People.Single(e => e.Name == "Joe");
            entry.Time = DateTime.Parse("November 7, 2021 10:00 PM");

            // Run the calculator:
            var results = calculator.Run(today).ToArray();
            Assert.AreEqual(4, results.Length);
            Assert.AreEqual(DateTime.Parse("November 7, 2021 10:00 AM"), results[0].Time);
            Assert.AreEqual(1, results[0].People.Count());
            Assert.AreEqual(0, results[0].ETips);
            Assert.AreEqual(50, results[0].Take);
            Assert.AreEqual("(50.00 - 0.00)/1 = 50.00", results[0].Description);

            Assert.AreEqual(DateTime.Parse("November 7, 2021 2 PM"), results[1].Time);
            Assert.AreEqual(2, results[1].People.Count());
            Assert.AreEqual(50, results[1].ETips);
            Assert.AreEqual(100, results[1].Take);
            Assert.AreEqual("(250.00 - 50.00)/2 = 100.00", results[1].Description);

            Assert.AreEqual(DateTime.Parse("November 7, 2021 6 PM"), results[2].Time);
            Assert.AreEqual(1, results[2].People.Count());
            Assert.AreEqual(250, results[2].ETips);
            Assert.AreEqual(60, results[2].Take);
            Assert.AreEqual("(310.00 - 250.00)/1 = 60.00", results[2].Description);

            Assert.AreEqual(DateTime.Parse("November 7, 2021 10:00 PM"), results[3].Time);
            Assert.AreEqual(0, results[3].People.Count());
            Assert.AreEqual(310, results[3].ETips);
            Assert.AreEqual(0, results[3].Take);
            Assert.AreEqual("No calculations to perform", results[3].Description);

            // Make sure everything adds up:
            var total = results.Select(e => e.People.Count() * e.Take).Sum();
            Assert.AreEqual(finalTotal, total);
        }
    }
}
