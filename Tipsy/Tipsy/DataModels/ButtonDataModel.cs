// <Copyright>Copyright (c) Birklid Software. All rights reserved.</Copyright>
// <File>ButtonDataModel.cs</File>
// <Company>Birklid Software</Company>
// <Author>Ray Birklid</Author>
// <Email>Ray.Birklid@gmail.com</Email>
// <Date>11/7/2021 4:04:36 PM</Date>
namespace Com.Gmail.Birklid.Ray.Tipsy.DataModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Input;

    public class ButtonDataModel
    {
        #region Creation

        public ButtonDataModel()
            : base()
        {
        }

        #endregion Creation

        public ICommand Command { get; set; }
        public object Content { get; set; }
    }
}
