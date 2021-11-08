// <Copyright>Copyright (c) Birklid Software. All rights reserved.</Copyright>
// <File>PersonCollection.cs</File>
// <Company>Birklid Software</Company>
// <Author>Ray Birklid</Author>
// <Email>Ray.Birklid@gmail.com</Email>
// <Date>11/7/2021 7:03:19 AM</Date>
namespace Com.Gmail.Birklid.Ray.Tipsy.Entity
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IPersonCollection : ICollection<Person>
    {
        Person CreateNew();
    }

    [Serializable]
    public class PersonCollection : IPersonCollection
    {
        #region Private Fields

        private readonly IIdFactory _idFactory;
        private readonly List<Person> _items = new List<Person>();

        #endregion Private Fields

        #region Creation

        public PersonCollection(
            IIdFactory idFactory)
            : base()
        {
            CheckArg.IsNotDefault(idFactory, nameof(idFactory));
            _idFactory = idFactory;
        }

        #endregion Creation

        #region IPersonCollection Members

        public Person CreateNew()
        {
            var person = new Person(_idFactory.Next(typeof(Person)));
            _items.Add(person);
            return person;
        }

        #endregion IPersonCollection Members

        #region ICollection<Person> Members

        public int Count => _items.Count;

        public bool IsReadOnly => false;

        public void Add(Person item) => throw new NotImplementedException();

        public void Clear() => _items.Clear();

        public bool Contains(Person item) => _items.Contains(item);

        public void CopyTo(Person[] array, int arrayIndex) => _items.CopyTo(array, arrayIndex);

        public IEnumerator<Person> GetEnumerator() => _items.GetEnumerator();

        public bool Remove(Person item) => _items.Remove(item);

        IEnumerator IEnumerable.GetEnumerator() => _items.GetEnumerator();

        #endregion ICollection<Person> Members

        #region Object Overrides

        public override string ToString()
        {
            return _items.Count == 1
                ? _items.Count + " item"
                : _items.Count + " items";
        }

        #endregion Object Overrides
    }

    public static class PersonCollectionExtensions
    {
        public static Person CreateNew(
            this IPersonCollection people,
            string personName)
        {
            var person = people.CreateNew();
            person.Name = personName;
            return person;
        }
    }
}
