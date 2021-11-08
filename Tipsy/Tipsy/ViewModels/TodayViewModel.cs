// <Copyright>Copyright (c) Birklid Software. All rights reserved.</Copyright>
// <File>TodayViewModel.cs</File>
// <Company>Birklid Software</Company>
// <Author>Ray Birklid</Author>
// <Email>Ray.Birklid@gmail.com</Email>
// <Date>11/7/2021 3:14:10 PM</Date>
namespace Com.Gmail.Birklid.Ray.Tipsy.ViewModels
{
    using Com.Gmail.Birklid.Ray.Tipsy.DataModels;
    using Com.Gmail.Birklid.Ray.Tipsy.Diagnostics;
    using Prism.Commands;
    using Prism.Mvvm;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class TodayViewModel : BindableBase
    {
        #region Private Fields

        private readonly ButtonDataModel[] _buttons;
        private string _title = Properties.Resources.TodayHeader;

        #endregion Private Fields

        #region Creation

        public TodayViewModel()
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
            yield return new ButtonDataModel { Content = "_addImage" };
            yield return new ButtonDataModel { Content = "_calculatorImage" };
        }

        #endregion Private Members
    }
}
