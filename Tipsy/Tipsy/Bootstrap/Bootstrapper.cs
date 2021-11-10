// <Copyright>Copyright (c) Birklid Software. All rights reserved.</Copyright>
// <File>Bootstrapper.cs</File>
// <Company>Birklid Software</Company>
// <Author>Ray Birklid</Author>
// <Email>Ray.Birklid@gmail.com</Email>
// <Date>11/6/2021 7:00:43 AM</Date>
namespace Com.Gmail.Birklid.Ray.Tipsy.Bootstrap
{
    using Com.Gmail.Birklid.Ray.Tipsy.Controllers;
    using Com.Gmail.Birklid.Ray.Tipsy.Diagnostics;
    using Com.Gmail.Birklid.Ray.Tipsy.Views;
    using Prism.DryIoc;
    using Prism.Ioc;
    using Prism.Regions;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows;

    internal class Bootstrapper : PrismBootstrapper
    {
        #region Creation

        internal Bootstrapper()
            : base()
        {
            Log.Created(this);
        }

        #endregion Creation

        #region PrismBootstrapper Overrides

        protected override DependencyObject CreateShell()
        {
            Log.MethodCall(this);
            return new Shell();
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();
            Container.Resolve<IDataController>().Initialize();
            Container.Resolve<IRegionManager>().RequestNavigate("ContentRegion", Properties.Settings.Default.DefaultView);
        }

        protected override void RegisterTypes(
            IContainerRegistry containerRegistry)
        {
            Log.MethodCall(this);
            containerRegistry.RegisterSingleton<IApplicationCommands, ApplicationCommands>();
            containerRegistry.RegisterSingleton<IDataController, DataController>();
            containerRegistry.RegisterForNavigation<History>();
            containerRegistry.RegisterForNavigation<Today>();
            containerRegistry.RegisterForNavigation<People>();
            containerRegistry.RegisterDialogWindow<TipsyDialog>();
            containerRegistry.RegisterDialog<PersonDialog, ViewModels.PersonDialogViewModel>();
        }

        #endregion PrismBootstrapper Overrides

        private Logger Log => ApplicationTraceSource.Instance;
    }
}
