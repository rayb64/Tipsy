// <Copyright>Copyright (c) Birklid Software. All rights reserved.</Copyright>
// <File>DataController.cs</File>
// <Company>Birklid Software</Company>
// <Author>Ray Birklid</Author>
// <Email>Ray.Birklid@gmail.com</Email>
// <Date>11/8/2021 2:04:58 PM</Date>
namespace Com.Gmail.Birklid.Ray.Tipsy.Controllers
{
    using Com.Gmail.Birklid.Ray.Tipsy.Entity;
    using Com.Gmail.Birklid.Ray.Tipsy.Diagnostics;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Threading.Tasks;
    using System.Diagnostics;
    using Com.Gmail.Birklid.Ray.Tipsy.DataModels;

    public interface IDataController
    {
        //IDayCollection Days { get; }
        PeopleDataModel People { get; }
        void Initialize();
        void Shutdown();
    }

    internal class DataController : IDataController
    {
        #region Private Fields

        private IEntities _entities;
        private readonly Uri _file;
        private PeopleDataModel _people;
        private readonly IO.IPersistence _persistence = IO.Persistence.Zip;

        #endregion Private Fields

        #region Creation

        public DataController()
            : base()
        {
            Log.Created(this);
            _file = InitializeFile();
        }

        #endregion Creation

        #region IDataController Members

        //public IDayCollection Days => _entities.Days;
        public PeopleDataModel People => _people;

        public void Initialize()
        {
            Log.MethodCall(this);
            _entities = _persistence.LoadOrCreate(_file, () => new Entities());
            Log.Information($"Entities loaded: {_entities}");
            _people = new PeopleDataModel(_entities.IdFactory, _entities.People);

            // Temporary.AddSamplePeople(People);
        }
        
        public void Shutdown()
        {
            Log.MethodCall(this);
            _entities.People = People.Select(e => e.Entity);
            _persistence.Save(_file, _entities);
            Log.Information($"Entities saved: {_entities}");
        }

        #endregion IDataController Members

        #region Private Members

        private Logger Log => DataTraceSource.Instance;

        private Uri InitializeFile()
        {
            var assembly = GetType().Assembly;
            var company = assembly.GetCustomAttributes(typeof(AssemblyCompanyAttribute)).Cast<AssemblyCompanyAttribute>().Single().Company;
            var title = assembly.GetCustomAttributes(typeof(AssemblyTitleAttribute)).Cast<AssemblyTitleAttribute>().Single().Title;
            var version = assembly.GetName().Version.ToString();
            var directory = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
            var path = System.IO.Path.Combine(directory, company, title, version, "entity-store.gzser");
            return new Uri(path);
        }

        //private static class Temporary
        //{
        //    [Conditional("DEBUG")]
        //    public static void AddSamplePeople(
        //        IPersonCollection people)
        //    {
        //        if (people.Count == 0)
        //        {
        //            people.CreateNew("Joe");
        //            people.CreateNew("Ray");
        //            people.CreateNew("Eva");
        //        }
        //    }
        //}

        #endregion Private Members
    }
}
