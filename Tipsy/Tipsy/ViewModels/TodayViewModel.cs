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
    using Prism.Regions;
    using Prism.Navigation;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class TodayViewModel : ContentViewModelBase, INavigationAware
    {
        #region Creation

        public TodayViewModel()
            : base(Properties.Resources.TodayHeader, InitializeButtons())
        {
            Buttons.Single(e => e.Name == "Add").Command = new DelegateCommand(OnAdd);
            Buttons.Single(e => e.Name == "Calculate").Command = new DelegateCommand(OnCalculate);
        }

        #endregion Creation

        #region INavigationAware Members

        public bool IsNavigationTarget(NavigationContext navigationContext) => true;

        public void OnNavigatedFrom(
            NavigationContext navigationContext)
        {
        }

        public void OnNavigatedTo(
            NavigationContext navigationContext)
        {
        }

        #endregion INavigationAware Members

        #region ContentViewModelBase Overrides

        protected override void OnHelp()
        {
            Log.MethodCall(this);
        }

        #endregion ContentViewModelBase Overrides

        #region Private Members

        private static IEnumerable<ButtonDataModel> InitializeButtons()
        {
            yield return new ButtonDataModel { Content = "_addImage", Name = "Add", ToolTip = Properties.Resources.DayEntry_AddToolTip };
            yield return new ButtonDataModel { Content = "_calculatorImage", Name = "Calculate", ToolTip = Properties.Resources.DayEntry_CalculatorToolTip };
        }

        private void OnAdd()
        {
            Log.MethodCall(this);
        }

        private void OnCalculate()
        {
            Log.MethodCall(this);
        }

        #endregion Private Members
    }
}
