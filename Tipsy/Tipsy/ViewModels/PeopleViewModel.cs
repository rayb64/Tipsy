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

    public class PeopleViewModel : ContentViewModelBase
    {
        #region Creation

        public PeopleViewModel()
            : base(Properties.Resources.PeopleHeader, InitializeButtons())
        {
        }

        #endregion Creation

        #region Private Members

        private static IEnumerable<ButtonDataModel> InitializeButtons()
        {
            yield return new ButtonDataModel { Content = "_addImage", Name = "Add", ToolTip = Properties.Resources.People_AddToolTip };
        }

        #endregion Private Members
    }
}
