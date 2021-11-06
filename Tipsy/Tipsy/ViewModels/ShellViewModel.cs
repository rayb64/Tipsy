// <Copyright>Copyright (c) Birklid Software. All rights reserved.</Copyright>
// <File>ShellViewModel.cs</File>
// <Company>Birklid Software</Company>
// <Author>Ray Birklid</Author>
// <Email>Ray.Birklid@gmail.com</Email>
// <Date>11/5/2021 3:34:06 PM</Date>
namespace Com.Gmail.Birklid.Ray.Tipsy.ViewModels
{
    using Prism.Commands;
    using Prism.Mvvm;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class ShellViewModel : BindableBase
    {
        #region Private Fields

        private string _title = Properties.Resources.ShellTitle;

        #endregion Private Fields

        #region Creation

        public ShellViewModel()
        {
        }

        #endregion Creation

        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        #region Object Overrides

        public override string ToString()
        {
            return _title;
        }

        #endregion Object Overrides
    }
}
