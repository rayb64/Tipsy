// <Copyright>Copyright (c) Birklid Software. All rights reserved.</Copyright>
// <File>App.xaml.cs</File>
// <Company>Birklid Software</Company>
// <Author>Ray Birklid</Author>
// <Email>Ray.Birklid@gmail.com</Email>
// <Date>11/5/2021 3:12:30 PM</Date>
namespace Com.Gmail.Birklid.Ray.Tipsy
{
    using Com.Gmail.Birklid.Ray.Tipsy.Diagnostics;
    using Com.Gmail.Birklid.Ray.Tipsy.Views;
    using Prism.Ioc;
    using Prism.Modularity;
    using System.Windows;

    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        #region Private Fields

        private IContainerProvider _container;

        #endregion Private Fields

        public App()
        {
            ApplicationTraceSource.Instance.Created(this);
        }

        protected override void OnExit(
            ExitEventArgs e)
        {
            _container.Resolve<Controllers.IDataController>().Shutdown();
            base.OnExit(e);
        }

        protected override void OnStartup(
            StartupEventArgs e)
        {
            // TODO: Need to protect this as a 'single instance' application.
            base.OnStartup(e);
            var bootstrapper = new Bootstrap.Bootstrapper();
            bootstrapper.Run();
            _container = bootstrapper.Container;
        }
    }
}
