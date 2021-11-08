// <Copyright>Copyright (c) Birklid Software. All rights reserved.</Copyright>
// <File>ApplicationCommands.cs</File>
// <Company>Birklid Software</Company>
// <Author>Ray Birklid</Author>
// <Email>Ray.Birklid@gmail.com</Email>
// <Date>11/7/2021 2:58:47 PM</Date>
namespace Com.Gmail.Birklid.Ray.Tipsy
{
    using Com.Gmail.Birklid.Ray.Tipsy.Diagnostics;
    using Prism.Commands;
    using Prism.Regions;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Input;

    public interface IApplicationCommands
    {
        ICommand ExitCommand { get; }
        ICommand ViewHistory { get; }
        ICommand ViewPeople { get; }
        ICommand ViewToday { get; }
    }

    internal class ApplicationCommands : IApplicationCommands
    {
        #region Private Fields

        private readonly IRegionManager _regionManager;

        #endregion Private Fields

        #region Creation

        public ApplicationCommands(
            IRegionManager regionManager)
            : base()
        {
            Log.Created(this);
            CheckArg.IsNotDefault(regionManager, nameof(regionManager));
            _regionManager = regionManager;
            ExitCommand = new DelegateCommand(OnExit);
            ViewHistory = new DelegateCommand(OnViewHistory);
            ViewPeople = new DelegateCommand(OnViewPeople);
            ViewToday = new DelegateCommand(OnViewToday);
        }

        #endregion Creation
   
        public ICommand ExitCommand { get; private set; }
        public ICommand ViewHistory { get; private set; }
        public ICommand ViewPeople { get; private set; }
        public ICommand ViewToday { get; private set; }

        #region Private Members

        private Logger Log => ApplicationTraceSource.Instance;

        private void OnExit()
        {
            Log.MethodCall(this);
            Application.Current?.MainWindow?.Close();
        }

        private void OnViewHistory()
        {
            Log.MethodCall(this);
            _regionManager.RequestNavigate("ContentRegion", "History");
        }

        private void OnViewPeople()
        {
            Log.MethodCall(this);
            _regionManager.RequestNavigate("ContentRegion", "People");
        }

        private void OnViewToday()
        {
            Log.MethodCall(this);
            _regionManager.RequestNavigate("ContentRegion", "Today");
        }

        #endregion Private Members
    }
}
