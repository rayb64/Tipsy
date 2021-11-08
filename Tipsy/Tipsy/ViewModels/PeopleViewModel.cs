// <Copyright>Copyright (c) Birklid Software. All rights reserved.</Copyright>
// <File>PeopleViewModel.cs</File>
// <Company>Birklid Software</Company>
// <Author>Ray Birklid</Author>
// <Email>Ray.Birklid@gmail.com</Email>
// <Date>11/7/2021 3:14:21 PM</Date>
namespace Com.Gmail.Birklid.Ray.Tipsy.ViewModels
{
    using Com.Gmail.Birklid.Ray.Tipsy.DataModels;
    using Com.Gmail.Birklid.Ray.Tipsy.Diagnostics;
    using Prism.Commands;
    using Prism.Mvvm;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class PeopleViewModel : ViewModelBase
    {
        #region Private Fields

        private readonly ButtonDataModel[] _buttons;
        private string _title = Properties.Resources.PeopleHeader;

        #endregion Private Fields

        #region Creation

        public PeopleViewModel()
        {
            _buttons = InitializeButtons().ToArray();
        }

        #endregion Creation

        public IEnumerable<ButtonDataModel> Buttons => _buttons;

        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        #region Private Members

        private IEnumerable<ButtonDataModel> InitializeButtons()
        {
            yield return new ButtonDataModel { Content = "Add" };
            yield return new ButtonDataModel { Content = "Edit" };
            yield return new ButtonDataModel { Content = "Remove" };
        }

        #endregion Private Members
    }
}
