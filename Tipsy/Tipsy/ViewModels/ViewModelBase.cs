// <Copyright>Copyright (c) Birklid Software. All rights reserved.</Copyright>
// <File>ViewModelBase.cs</File>
// <Company>Birklid Software</Company>
// <Author>Ray Birklid</Author>
// <Email>Ray.Birklid@gmail.com</Email>
// <Date>11/7/2021 3:18:18 PM</Date>
namespace Com.Gmail.Birklid.Ray.Tipsy.ViewModels
{
    using Com.Gmail.Birklid.Ray.Tipsy.Diagnostics;
    using Prism.Mvvm;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class ViewModelBase : BindableBase
    {
        #region Creation

        public ViewModelBase()
            : base()
        {
            Log.Created(this);
        }

        #endregion Creation

        protected Logger Log => ViewModelTraceSource.Instance;
    }
}
