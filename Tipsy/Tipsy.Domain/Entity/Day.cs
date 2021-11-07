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
    using System.Text;
    using System.Threading.Tasks;

    public interface IDay : IEntityBase
    {
        DateTime Date { get; }
        IDayEntryCollection Entries { get; }
    }

    [Serializable]
    public class Day : EntityBase, IDay
    {
        #region Private Fields

        private readonly DateTime _date = DateTime.Today;
        private readonly DayEntryCollection _entries;

        #endregion Private Fields

        #region Creation

        public Day(
            IIdFactory idFactory,
            DateTime date)
            : base(idFactory.Next(typeof(Day)))
        {
            _date = date;
            _entries = new DayEntryCollection(idFactory);
        }

        #endregion Creation

        #region IDay Members

        public DateTime Date => _date;
        public IDayEntryCollection Entries => _entries;

        #endregion IDay Members
    }
}
