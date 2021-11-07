// <Copyright>Copyright (c) Birklid Software. All rights reserved.</Copyright>
// <File>DayEntryCollection.cs</File>
// <Company>Birklid Software</Company>
// <Author>Ray Birklid</Author>
// <Email>Ray.Birklid@gmail.com</Email>
// <Date>11/7/2021 8:05:22 AM</Date>
namespace Com.Gmail.Birklid.Ray.Tipsy.Entity
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IDayEntryCollection : ICollection<DayEntry>
    {
        DayEntry CreateNew();
    }

    [Serializable]
    public class DayEntryCollection : IDayEntryCollection
    {
        #region Private Fields

        private readonly IIdFactory _idFactory;
        private readonly List<DayEntry> _items = new List<DayEntry>();

        #endregion Private Fields

        #region Creation

        public DayEntryCollection(
            IIdFactory idFactory)
            : base()
        {
            CheckArg.IsNotDefault(idFactory, nameof(idFactory));
            _idFactory = idFactory;
        }

        public DayEntryCollection()
            : this(new IdFactory())
        {
        }

        #endregion Creation

        #region IDayEntryCollection Members

        public DayEntry CreateNew()
        {
            var dayEntry = new DayEntry(_idFactory.Next(typeof(DayEntry)));
            _items.Add(dayEntry);
            return dayEntry;
        }

        #endregion IDayEntryCollection Members

        #region ICollection<DayEntry> Members

        public int Count => _items.Count;

        public bool IsReadOnly => false;

        public void Add(DayEntry item) => throw new NotImplementedException();

        public void Clear() => _items.Clear();

        public bool Contains(DayEntry item) => _items.Contains(item);

        public void CopyTo(DayEntry[] array, int arrayIndex) => _items.CopyTo(array, arrayIndex);

        public IEnumerator<DayEntry> GetEnumerator() => _items.GetEnumerator();

        public bool Remove(DayEntry item) => _items.Remove(item);

        IEnumerator IEnumerable.GetEnumerator() => _items.GetEnumerator();

        #endregion ICollection<DayEntry> Members

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
