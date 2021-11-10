// <Copyright>Copyright (c) Birklid Software. All rights reserved.</Copyright>
// <File>PeopleViewModel.cs</File>
// <Company>Birklid Software</Company>
// <Author>Ray Birklid</Author>
// <Email>Ray.Birklid@gmail.com</Email>
// <Date>11/7/2021 3:14:21 PM</Date>
namespace Com.Gmail.Birklid.Ray.Tipsy.ViewModels
{
    using Com.Gmail.Birklid.Ray.Tipsy.Controllers;
    using Com.Gmail.Birklid.Ray.Tipsy.DataModels;
    using Com.Gmail.Birklid.Ray.Tipsy.Diagnostics;
    using Com.Gmail.Birklid.Ray.Tipsy.Entity;
    using Prism.Commands;
    using Prism.Mvvm;
    using Prism.Services.Dialogs;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows.Data;
    using System.Windows.Input;

    public class PeopleViewModel : ContentViewModelBase
    {
        #region Private Fields

        private readonly IDataController _dataController;
        private readonly IDialogService _dialogService;
        private DelegateCommand _editCommand;
        private readonly ListCollectionView _people;
        private DelegateCommand _removeCommand;

        #endregion Private Fields

        #region Creation

        public PeopleViewModel(
            IDataController dataController,
            IDialogService dialogService)
            : base(Properties.Resources.PeopleHeader, InitializeButtons())
        {
            _dataController = dataController;
            _dialogService = dialogService;
            _people = new ListCollectionView(_dataController.People);
            _people.SortDescriptions.Add(new SortDescription { Direction = ListSortDirection.Ascending, PropertyName = "Name" });
            _people.MoveCurrentToFirst();
            InitializeCommands();
        }

        #endregion Creation

        public ListCollectionView People => _people;

        #region Private Members

        private static IEnumerable<ButtonDataModel> InitializeButtons()
        {
            yield return new ButtonDataModel { Content = "_addImage", Name = "Add", ToolTip = Properties.Resources.People_AddToolTip };
        }

        private void InitializeCommands()
        {
            Buttons.Single(e => e.Name == "Add").Command = new DelegateCommand(OnAdd);
            _editCommand = new DelegateCommand(OnEdit);
            _removeCommand = new DelegateCommand(OnRemove);
            foreach (var person in _people.OfType<PersonDataModel>())
            {
                person.EditCommand = _editCommand;
                person.DoubleClickCommand = _editCommand;
                person.RemoveCommand = _removeCommand;
            }
        }

        private void OnAdd()
        {
            Log.MethodCall(this);
            _dialogService.ShowDialog("PersonDialog", r =>
            {
                if (r.Result == ButtonResult.OK)
                {
                    var person = _dataController.People.CreateNew(r.Parameters.GetValue<string>("_name"));
                    person.EditCommand = _editCommand;
                    person.DoubleClickCommand = _editCommand;
                    person.RemoveCommand = _removeCommand;
                }
            });
        }

        private void OnEdit()
        {
            Log.MethodCall(this);
            var current = People.CurrentItem as PersonDataModel;
            var parameters = new DialogParameters($"_originalName={current.Name}");
            _dialogService.ShowDialog("PersonDialog", parameters, r =>
            {
                if (r.Result == ButtonResult.OK)
                {
                    current.Name = r.Parameters.GetValue<string>("_name");
                }
            });
        }

        private void OnRemove()
        {
            Log.MethodCall(this);
            if (People.CurrentItem is PersonDataModel person)
            {
                _dataController.People.Remove(person);
            }
        }

        #endregion Private Members
    }
}
