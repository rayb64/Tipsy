// <Copyright>Copyright (c) Birklid Software. All rights reserved.</Copyright>
// <File>DayCollection.cs</File>
// <Company>Birklid Software</Company>
// <Author>Ray Birklid</Author>
// <Email>Ray.Birklid@gmail.com</Email>
// <Date>11/7/2021 8:26:47 AM</Date>
namespace Com.Gmail.Birklid.Ray.Tipsy.Entity
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IDayCollection : ICollection<Day>
    {
        Day Today { get; }
    }

    [Serializable]
    public class DayCollection : IDayCollection
    {
        #region Private Fields

        private readonly IIdFactory _idFactory;
        private readonly List<Day> _items = new List<Day>();

        #endregion Private Fields

        #region Creation

        public DayCollection(
            IIdFactory idFactory)
            : base()
        {
            CheckArg.IsNotDefault(idFactory, nameof(idFactory));
            _idFactory = idFactory;
        }

        public DayCollection()
            : this(new IdFactory())
        {
        }

        #endregion Creation

        #region IDayCollection Members

        public Day Today
        {
            get
            {
                var date = DateTime.Today;
                var today = _items.SingleOrDefault(e => DateTime.Equals(e.Date, date));
                if (today == null)
                {
                    today = new Day(_idFactory, date);
                    _items.Add(today);
                }
                return today;
            }
        }

        #endregion IDayCollection Members

        #region ICollection<Day> Members

        public int Count => _items.Count;

        public bool IsReadOnly => false;

        public void Add(Day item) => throw new NotImplementedException();

        public void Clear() => _items.Clear();

        public bool Contains(Day item) => _items.Contains(item);

        public void CopyTo(Day[] array, int arrayIndex) => _items.CopyTo(array, arrayIndex);

        public IEnumerator<Day> GetEnumerator() => _items.GetEnumerator();

        public bool Remove(Day item) => _items.Remove(item);

        IEnumerator IEnumerable.GetEnumerator() => _items.GetEnumerator();

        #endregion ICollection<Day> Members

        #region Object Overrides

        public override string ToString()
        {
            return _items.Count == 1
                ? _items.Count + " item"
                : _items.Count + " items";
        }

        #endregion Object Overrides
    }
}
