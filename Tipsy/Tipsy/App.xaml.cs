namespace Com.Gmail.Birklid.Ray.Tipsy
{
    using Com.Gmail.Birklid.Ray.Tipsy.Views;
    using Prism.Ioc;
    using Prism.Modularity;
    using System.Windows;

    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<Shell>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {

        }
    }
}
