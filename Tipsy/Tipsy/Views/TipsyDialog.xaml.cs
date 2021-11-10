namespace Com.Gmail.Birklid.Ray.Tipsy.Views
{
    using Prism.Services.Dialogs;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Documents;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;
    using System.Windows.Shapes;

    /// <summary>
    /// Interaction logic for TipsyDialog.xaml
    /// </summary>
    public partial class TipsyDialog : Window, IDialogWindow
    {
        #region Creation

        public TipsyDialog()
        {
            InitializeComponent();
        }

        #endregion Creation

        #region IDialogWindow Members

        public IDialogResult Result
        {
            get;
            set;
        }

        #endregion IDialogWindow Members
    }
}
