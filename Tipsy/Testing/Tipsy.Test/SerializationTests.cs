// <Copyright>Copyright (c) Birklid Software. All rights reserved.</Copyright>
// <File>SerializationTests.cs</File>
// <Company>Birklid Software</Company>
// <Author>Ray Birklid</Author>
// <Email>Ray.Birklid@gmail.com</Email>
// <Date>11/6/2021 7:45:00 AM</Date>
namespace Com.Gmail.Birklid.Ray.Tipsy.Test
{
    using Com.Gmail.Birklid.Ray.Tipsy;
    using Com.Gmail.Birklid.Ray.Tipsy.Diagnostics;
    using Com.Gmail.Birklid.Ray.Tipsy.Entity;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.IO;
    using System.Runtime.Serialization.Formatters.Binary;

    [TestClass]
    public class SerializationTests : TestBase
    {
        #region Private Fields

        private static readonly BinaryFormatter _formatter = new BinaryFormatter();

        #endregion Private Fields

        public static T Clone<T>(
            T obj)
        {
            return GetObject<T>(GetBytes(obj));
        }

        public static byte[] GetBytes<T>(
            T obj)
        {
            using (var stream = new MemoryStream())
            {
                _formatter.Serialize(stream, obj);
                return stream.ToArray();
            }
        }

        public static T GetObject<T>(
            byte[] bytes)
        {
            using (var stream = new MemoryStream(bytes))
            {
                return (T)_formatter.Deserialize(stream);
            }
        }

        public static void VerifyIsSerializable<T>(
            T obj)
        {
            Log.Information("Checking type for serialization: {0}", obj.GetType());
            Assert.IsNotNull(obj);
            var cloned = Clone(obj);
            Assert.IsNotNull(cloned);
            Assert.AreNotSame(obj, cloned);
        }

        [TestMethod]
        public void VerifyIsSerializable()
        {
            var idFactory = new IdFactory();
            var items = new object[]
            {
                idFactory,
                new Day(idFactory, DateTime.Today),
                new DayCollection(idFactory),
                new DayEntry(idFactory.Next(typeof(DayEntry))),
                new DayEntryCollection(idFactory),
                new Entities(),
                new Person(idFactory.Next(typeof(Person))),
                new PersonCollection(idFactory)
            };
            foreach (var item in items)
            {
                VerifyIsSerializable(item);
            }
        }
    }
}
