// <Copyright>Copyright (c) Birklid Software. All rights reserved.</Copyright>
// <File>PersonDataModel.cs</File>
// <Company>Birklid Software</Company>
// <Author>Ray Birklid</Author>
// <Email>Ray.Birklid@gmail.com</Email>
// <Date>11/8/2021 3:28:17 PM</Date>
namespace Com.Gmail.Birklid.Ray.Tipsy.DataModels
{
    using Com.Gmail.Birklid.Ray.Tipsy.Entity;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Input;

    public class PersonDataModel : EntityDataModelBase<IPerson>, IPerson
    {
        #region Private Fields

        private ICommand _doubleClick;
        private ICommand _edit;
        private ICommand _remove;

        #endregion Private Fields

        #region Creation

        public PersonDataModel(
            IPerson person)
            : base(person)
        {
        }

        #endregion Creation

        public string Name
        {
            get => Entity.Name;
            set
            {
                if (Entity.Name != value)
                {
                    Entity.Name = value;
                    RaisePropertyChanged();
                }
            }
        }

        public ICommand DoubleClickCommand
        {
            get => _doubleClick;
            set => SetProperty(ref _doubleClick, value);
        }

        public ICommand EditCommand
        {
            get => _edit;
            set => SetProperty(ref _edit, value);
        }

        public ICommand RemoveCommand
        {
            get => _remove;
            set => SetProperty(ref _remove, value);
        }

        #region Object Overrides

        public override string ToString()
        {
            return Entity.ToString();
        }

        #endregion Object Overrides
    }
}
