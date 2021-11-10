// <Copyright>Copyright (c) Birklid Software. All rights reserved.</Copyright>
// <File>PeopleDataModel.cs</File>
// <Company>Birklid Software</Company>
// <Author>Ray Birklid</Author>
// <Email>Ray.Birklid@gmail.com</Email>
// <Date>11/8/2021 6:39:32 PM</Date>
namespace Com.Gmail.Birklid.Ray.Tipsy.DataModels
{
    using Com.Gmail.Birklid.Ray.Tipsy.Diagnostics;
    using Com.Gmail.Birklid.Ray.Tipsy.Entity;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class PeopleDataModel : ReadOnlyObservableCollection<PersonDataModel>
    {
        #region Private Fields

        private readonly IIdFactory _idFactory;
        private readonly ObservableCollection<PersonDataModel> _mutable;

        #endregion Private Fields

        #region Creation

        private PeopleDataModel(
            ObservableCollection<PersonDataModel> mutable)
            : base(mutable)
        {
            Log.Created(this);
            _mutable = mutable;
        }

        public PeopleDataModel(
            IIdFactory idFactory,
            IEnumerable<IPerson> people)
            : this(new ObservableCollection<PersonDataModel>(people.Select(e => new PersonDataModel(e as Person))))
        {
            CheckArg.IsNotDefault(idFactory, nameof(idFactory));
            _idFactory = idFactory;
        }

        #endregion Creation

        public PersonDataModel CreateNew(
            string name)
        {
            // NOTE: If this is ever called from a thread other than the UI thread,
            // I'll need to use the synchronizer.
            //
            Log.MethodCall(this);
            var result = new PersonDataModel(new Person(_idFactory.Next(typeof(Person))) { Name = name });
            _mutable.Add(result);
            Log.Information($"Person added: {name}");
            return result;
        }

        public bool Remove(
            PersonDataModel person)
        {
            // NOTE: If this is ever called from a thread other than the UI thread,
            // I'll need to use the synchronizer.
            //
            // TODO: Refactor the entity data structures...  Because of some choices
            // I made earlier, I am maintaining two separate collections here.
            Log.MethodCall(this);
            var result = _mutable.Remove(person);// && _people.Remove(person.Entity as Person);
            if (result)
            {
                Log.Information($"Person removed: {person.Name}");
            }
            else
            {
                Log.Warning($"Unable to remove person: {person.Name}");
            }
            return result;
        }

        #region Private Members

        private Logger Log => DataModelTraceSource.Instance;

        #endregion Private Members
    }
}
