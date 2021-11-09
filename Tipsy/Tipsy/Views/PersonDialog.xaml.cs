namespace Com.Gmail.Birklid.Ray.Tipsy.Views
{
    using System.Windows.Controls;

    /// <summary>
    /// Interaction logic for PersonDialog
    /// </summary>
    public partial class PersonDialog : UserControl
    {
        public PersonDialog()
        {
            InitializeComponent();
        }

        public override void EndInit()
        {
            base.EndInit();
            _textBox.Focus();
        }
    }
}
