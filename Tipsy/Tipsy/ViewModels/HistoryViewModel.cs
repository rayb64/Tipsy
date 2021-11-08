// <Copyright>Copyright (c) Birklid Software. All rights reserved.</Copyright>
// <File>HistoryViewModel.cs</File>
// <Company>Birklid Software</Company>
// <Author>Ray Birklid</Author>
// <Email>Ray.Birklid@gmail.com</Email>
// <Date>11/7/2021 3:14:37 PM</Date>
namespace Com.Gmail.Birklid.Ray.Tipsy.ViewModels
{
    using Com.Gmail.Birklid.Ray.Tipsy.DataModels;
    using Com.Gmail.Birklid.Ray.Tipsy.Diagnostics;
    using Prism.Commands;
    using Prism.Mvvm;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class HistoryViewModel : ContentViewModelBase
    {
        public HistoryViewModel()
            : base(Properties.Resources.HistoryHeader, new ButtonDataModel[0])
        {
        }
    }
}
