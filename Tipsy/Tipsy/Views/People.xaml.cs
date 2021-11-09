namespace Com.Gmail.Birklid.Ray.Tipsy.Views
{
    using System.Windows.Controls;
    using System.Windows.Input;

    /// <summary>
    /// Interaction logic for People
    /// </summary>
    public partial class People : UserControl
    {
        public People()
        {
            InitializeComponent();
        }

        private void ListViewItem_MouseDoubleClick(
            object sender,
            System.Windows.Input.MouseButtonEventArgs e)
        {
            // TODO: Revisit this.  I'd much prefer to be using an attached property/behavior.
            //       This way of doing it requires too much knowledge of the view model layer.
            if (sender is ListViewItem item)
            {
                var data = item.DataContext;
                var command = data.GetType().GetProperty("DoubleClickCommand").GetGetMethod().Invoke(data, null) as ICommand;
                if (command.CanExecute(data)) { command.Execute(data); }
            }
        }
    }
}
