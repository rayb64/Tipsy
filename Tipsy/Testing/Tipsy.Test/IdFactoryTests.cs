// <Copyright>Copyright (c) Birklid Software. All rights reserved.</Copyright>
// <File>IdFactoryTests.cs</File>
// <Company>Birklid Software</Company>
// <Author>Ray Birklid</Author>
// <Email>Ray.Birklid@gmail.com</Email>
// <Date>11/6/2021 7:28:35 AM</Date>
namespace Com.Gmail.Birklid.Ray.Tipsy.Test
{
    using Com.Gmail.Birklid.Ray.Tipsy;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    [TestClass]
    public class IdFactoryTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void IdForNull()
        {
            var factory = new IdFactory();
            string obj = null;
            var id = factory.Next(obj);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void IdFromNullFactory()
        {
            IIdFactory factory = null;
            factory.Next(this);
        }

        [TestMethod]
        public void VerifyUniqueIdsForMultipleTypes()
        {
            var factory = new IdFactory();
            var ids0 = Enumerable.Range(0, 10).Select(e => factory.Next(this)).ToArray();
            var ids1 = Enumerable.Range(0, 10).Select(e => factory.Next(factory)).ToArray();

            // Ids WILL be duplicated...  however, each type is given it's own 'pool' of ids.
            // For a given type, the ids will be unique.
            Assert.IsTrue(ids0.SequenceEqual(ids1));
            Assert.AreEqual(10, ids0.Distinct().Count());
            Assert.AreEqual(10, ids1.Distinct().Count());
            Assert.IsTrue(ids0.SequenceEqual(Enumerable.Range(1, 10).Select(e => (long)e)));
            Assert.IsTrue(ids1.SequenceEqual(Enumerable.Range(1, 10).Select(e => (long)e)));
        }

        [TestMethod]
        public void VerifyUniqueIdsForType()
        {
            var factory = new IdFactory();
            var ids = Enumerable.Range(0, 10).Select(e => factory.Next(this)).ToArray();
            Assert.AreEqual(10, ids.Distinct().Count());
            Assert.IsTrue(ids.SequenceEqual(Enumerable.Range(1, 10).Select(e => (long)e)));
        }
    }
}
