namespace Com.Gmail.Birklid.Ray.Tipsy.ViewModels
{
    using Prism.Commands;
    using Prism.Mvvm;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class ShellViewModel : BindableBase
    {
        #region Private Fields

        private string _title = Properties.Resources.ShellTitle;

        #endregion Private Fields

        #region Creation

        public ShellViewModel()
        {
        }

        #endregion Creation

        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        #region Object Overrides

        public override string ToString()
        {
            return _title;
        }

        #endregion Object Overrides
    }
}
