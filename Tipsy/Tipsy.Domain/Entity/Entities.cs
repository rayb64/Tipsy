// <Copyright>Copyright (c) Birklid Software. All rights reserved.</Copyright>
// <File>Entities.cs</File>
// <Company>Birklid Software</Company>
// <Author>Ray Birklid</Author>
// <Email>Ray.Birklid@gmail.com</Email>
// <Date>11/7/2021 7:21:20 AM</Date>
namespace Com.Gmail.Birklid.Ray.Tipsy.Entity
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public interface IEntities
    {
        IIdFactory IdFactory { get; }
        IEnumerable<Day> Days { get; set; }
        IEnumerable<Person> People { get; set; }
    }

    [Serializable]
    public class Entities : IEntities
    {
        #region Private Fields

        private readonly IIdFactory _idFactory = new IdFactory();
        private Day[] _days = new Day[0];
        private Person[] _people = new Person[0];

        #endregion Private Fields

        #region Creation

        public Entities()
            : base()
        {
        }

        #endregion Creation

        #region IEntities Members

        public IIdFactory IdFactory => _idFactory;

        public IEnumerable<Day> Days
        {
            get => _days;
            set => _days = value == null ? new Day[0] : value.ToArray();
        }

        public IEnumerable<Person> People
        {
            get => _people;
            set => _people = value == null ? new Person[0] : value.ToArray();
        }

        #endregion IEntities Members

        #region Object Overrides

        public override string ToString()
        {
            return $"Days = {_days.Length}; People = {_people.Length}; IdFactory = {_idFactory}";
        }

        #endregion Object Overrides
    }
}

