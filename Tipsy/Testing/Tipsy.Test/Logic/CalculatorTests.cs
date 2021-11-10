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
            var finalTotal = 310;
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
                { Action = DayAction.ClockOff, ETips = finalTotal, Person = joe, Time = DateTime.Parse("November 7, 2021 10:00 PM") }
            };

            // Run the calculator:
            var results = calculator.Run(day).ToArray();
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

        [TestMethod]
        public void RoundingError()
        {
            // This shows a bug (a rounding error) in the calculator.
            var entities = new Entities();
            var people = new[]
            {
                    new Person(entities.IdFactory.Next(typeof(Person))) { Name = "Cliff" },
                    new Person(entities.IdFactory.Next(typeof(Person))) { Name = "Daniela" },
                    new Person(entities.IdFactory.Next(typeof(Person))) { Name = "Eva" },
                    new Person(entities.IdFactory.Next(typeof(Person))) { Name = "Joe" },
                    new Person(entities.IdFactory.Next(typeof(Person))) { Name = "Ray" }
                };
            var days = new[]
            {
                    new Day(entities.IdFactory.Next(typeof(Day)), DateTime.Parse("November 8, 2021")),
                    new Day(entities.IdFactory.Next(typeof(Day)), DateTime.Parse("November 9, 2021"))
                };
            days[0].Entries = new[]
            {
                    new DayEntry(entities.IdFactory.Next(typeof(DayEntry))) { Action = DayAction.ClockOn, ETips = 0, Person = people[4], Time = DateTime.Parse("November 8, 2021 10:00 AM") },
                    new DayEntry(entities.IdFactory.Next(typeof(DayEntry))) { Action = DayAction.ClockOn, ETips = 30, Person = people[3], Time = DateTime.Parse("November 8, 2021 2:00 PM") },
                    new DayEntry(entities.IdFactory.Next(typeof(DayEntry))) { Action = DayAction.ClockOff, ETips = 190, Person = people[4], Time = DateTime.Parse("November 8, 2021 6:00 PM") },
                    new DayEntry(entities.IdFactory.Next(typeof(DayEntry))) { Action = DayAction.ClockOff, ETips = 210, Person = people[3], Time = DateTime.Parse("November 8, 2021 10:00 PM") }
                };
            days[1].Entries = new[]
            {
                    new DayEntry(entities.IdFactory.Next(typeof(DayEntry))) { Action = DayAction.ClockOn, ETips = 0, Person = people[4], Time = DateTime.Parse("November 9, 2021 10:00 AM") },
                    new DayEntry(entities.IdFactory.Next(typeof(DayEntry))) { Action = DayAction.ClockOn, ETips = 30, Person = people[0], Time = DateTime.Parse("November 9, 2021 12:00 PM") },
                    new DayEntry(entities.IdFactory.Next(typeof(DayEntry))) { Action = DayAction.ClockOn, ETips = 45, Person = people[3], Time = DateTime.Parse("November 9, 2021 2:00 PM") },
                    new DayEntry(entities.IdFactory.Next(typeof(DayEntry))) { Action = DayAction.ClockOff, ETips = 212, Person = people[4], Time = DateTime.Parse("November 9, 2021 6:00 PM") },
                    new DayEntry(entities.IdFactory.Next(typeof(DayEntry))) { Action = DayAction.ClockOff, ETips = 240, Person = people[0], Time = DateTime.Parse("November 9, 2021 7:00 PM") },
                    new DayEntry(entities.IdFactory.Next(typeof(DayEntry))) { Action = DayAction.ClockOff, ETips = 291, Person = people[3], Time = DateTime.Parse("November 9, 2021 10:00 PM") }
                };
            entities.People = people;
            entities.Days = days;
            var calculator = new Calculator();
            var day = entities.Days.Last();
            var results = calculator.Run(day).ToArray();

            foreach (var r in results)
            {
                Console.WriteLine("{0}, {1}, {2}, {3}, {4}", r.ETips, r.Take, r.Time.ToString("T"), string.Join(", ", r.People), r.Description);
            }
            Assert.AreEqual(day.Entries.Last().ETips, results.Sum(e => e.People.Count() * Math.Round(e.Take, 2)), "There is a rounding error in the calculator");
        }
    }
}
