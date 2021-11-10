// <Copyright>Copyright (c) Birklid Software. All rights reserved.</Copyright>
// <File>Day.cs</File>
// <Company>Birklid Software</Company>
// <Author>Ray Birklid</Author>
// <Email>Ray.Birklid@gmail.com</Email>
// <Date>11/7/2021 8:13:47 AM</Date>
namespace Com.Gmail.Birklid.Ray.Tipsy.Entity
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public interface IDay : IEntityBase
    {
        DateTime Date { get; }
        IEnumerable<IDayEntry> Entries { get; set; }
    }

    [Serializable]
    public class Day : EntityBase, IDay
    {
        #region Private Fields

        private readonly DateTime _date;
        private IDayEntry[] _entries = new IDayEntry[0];

        #endregion Private Fields

        #region Creation

        public Day(
            long id,
            DateTime date)
            : base(id)
        {
            _date = date;
        }

        #endregion Creation

        #region IDay Members

        public DateTime Date => _date;

        public IEnumerable<IDayEntry> Entries
        {
            get => _entries;
            set => _entries = value == null ? new IDayEntry[0] : value.ToArray();
        }

        #endregion IDay Members

        #region Object Overrides

        public override string ToString()
        {
            return Date.ToString("d");
        }

        #endregion Object Overrides
    }
}
