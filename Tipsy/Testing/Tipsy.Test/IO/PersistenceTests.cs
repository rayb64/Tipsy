// <Copyright>Copyright (c) Birklid Software. All rights reserved.</Copyright>
// <File>PersistenceTests.cs</File>
// <Company>Birklid Software</Company>
// <Author>Ray Birklid</Author>
// <Email>Ray.Birklid@gmail.com</Email>
// <Date>11/8/2021 1:42:53 PM</Date>
namespace Com.Gmail.Birklid.Ray.Tipsy.Test.IO
{
    using Com.Gmail.Birklid.Ray.Tipsy.Entity;
    using Com.Gmail.Birklid.Ray.Tipsy.IO;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.IO;
    using System.Linq;

    [TestClass]
    public class PersistenceTests
    {
        [TestMethod]
        public void StandardOperation()
        {
            var p = Persistence.Default;
            var file = new Uri(Path.GetFullPath("entities.bin"));
            DeleteFile(file);
            var ent = p.LoadOrCreate(file, () => new Entities());
            var person = ent.People = new[] { new Person(ent.IdFactory.Next(typeof(Person))) { Name = "Hot dog" } };
            p.Save(file, ent);
            var ent2 = p.LoadOrCreate(file, () => new Entities());
            Assert.AreEqual("Hot dog", ent2.People.Single().Name);
        }

        [TestMethod]
        public void ZipOperation()
        {
            var p = Persistence.Zip;
            var file = new Uri(Path.GetFullPath("entities.gz"));
            DeleteFile(file);
            var ent = p.LoadOrCreate(file, () => new Entities());
            var person = ent.People = new[] { new Person(ent.IdFactory.Next(typeof(Person))) { Name = "Zip dog" } };
            p.Save(file, ent);
            var ent2 = p.LoadOrCreate(file, () => new Entities());
            Assert.AreEqual("Zip dog", ent2.People.Single().Name);
        }

        private void DeleteFile(
            string path)
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

        private void DeleteFile(
            Uri uri)
        {
            DeleteFile(uri.LocalPath);
        }
    }
}
