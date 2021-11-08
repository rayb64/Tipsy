namespace Com.Gmail.Birklid.Ray.Tipsy.Views
{
    using Com.Gmail.Birklid.Ray.Tipsy.Diagnostics;
    using System.Windows;

    /// <summary>
    /// Interaction logic for Shell.xaml
    /// </summary>
    public partial class Shell : Window
    {
        public Shell()
        {
            ApplicationTraceSource.Instance.Created(this);
            InitializeComponent();
        }
    }
}
