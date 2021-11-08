// <Copyright>Copyright (c) Birklid Software. All rights reserved.</Copyright>
// <File>ButtonDataModel.cs</File>
// <Company>Birklid Software</Company>
// <Author>Ray Birklid</Author>
// <Email>Ray.Birklid@gmail.com</Email>
// <Date>11/7/2021 4:04:36 PM</Date>
namespace Com.Gmail.Birklid.Ray.Tipsy.DataModels
{
    using Prism.Mvvm;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Input;

    public class ButtonDataModel : BindableBase
    {
        #region Private Fields

        private ICommand _command;
        private object _content;
        private string _name;
        private string _toolTip = string.Empty;

        #endregion Private Fields

        #region Creation

        public ButtonDataModel()
            : base()
        {
        }

        #endregion Creation

        public ICommand Command
        {
            get => _command;
            set => SetProperty(ref _command, value);
        }

        public object Content
        {
            get => _content;
            set => SetProperty(ref _content, value);
        }

        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        public string ToolTip
        {
            get => _toolTip;
            set => SetProperty(ref _toolTip, value);
        }
    }
}
