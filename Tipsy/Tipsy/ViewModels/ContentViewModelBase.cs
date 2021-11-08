// <Copyright>Copyright (c) Birklid Software. All rights reserved.</Copyright>
// <File>ContentViewModelBase.cs</File>
// <Company>Birklid Software</Company>
// <Author>Ray Birklid</Author>
// <Email>Ray.Birklid@gmail.com</Email>
// <Date>11/7/2021 9:15:43 PM</Date>
namespace Com.Gmail.Birklid.Ray.Tipsy.ViewModels
{
    using Com.Gmail.Birklid.Ray.Tipsy.DataModels;
    using Prism.Commands;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Input;

    public class ContentViewModelBase : ViewModelBase
    {
        #region Private Fields

        private ButtonDataModel[] _buttons;
        private string _title = "No title";

        #endregion Private Fields

        #region Creation

        protected ContentViewModelBase(
            string title,
            IEnumerable<ButtonDataModel> buttons)
            : base()
        {
            _title = title;
            _buttons = buttons.ToArray();
            HelpCommand = new DelegateCommand(OnHelp);
        }

        #endregion Creation

        public IEnumerable<ButtonDataModel> Buttons => _buttons;

        public ICommand HelpCommand
        {
            get;
            private set;
        }

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

        protected virtual void OnHelp() { }
    }
}
