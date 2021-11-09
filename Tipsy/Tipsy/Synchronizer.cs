// <Copyright>Copyright (c) Birklid Software. All rights reserved.</Copyright>
// <File>Synchronizer.cs</File>
// <Company>Birklid Software</Company>
// <Author>Ray Birklid</Author>
// <Email>Ray.Birklid@gmail.com</Email>
// <Date>11/8/2021 6:17:09 PM</Date>
namespace Com.Gmail.Birklid.Ray.Tipsy
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Threading;

    public interface ISynchronizer
    {
        bool IsInvokeRequired { get; }
        void BeginInvoke(Action action);
        void Invoke(Action action);
    }

    public class Synchronizer : ISynchronizer
    {
        #region Private Fields

        public static readonly ISynchronizer _instance = new Synchronizer();

        #endregion Private Fields

        #region Creation

        private Synchronizer()
            : base()
        {
        }

        #endregion Creation

        public static ISynchronizer Instance => _instance;

        #region ISynchronizer Members

        public bool IsInvokeRequired
        {
            get
            {
                return _dispatcher?.CheckAccess() == false;
            }
        }

        public void BeginInvoke(
            Action action)
        {
            if (_dispatcher == null)
            {
                action();
            }
            else
            {
                _dispatcher.BeginInvoke(action);
            }
        }
        
        public void Invoke(
            Action action)
        {
            if (_dispatcher == null)
            {
                action();
            }
            else
            {
                _dispatcher.Invoke(action);
            }
        }

        #endregion ISynchronizer Members

        #region Object Overrides

        public override string ToString()
        {
            return $"Dispatcher present: {_dispatcher != null}";
        }

        #endregion Object Overrides

        #region Private Members

        private Dispatcher _dispatcher => System.Windows.Application.Current?.Dispatcher;

        #endregion Private Members
    }

    public static class SynchronizerExtensions
    {
        public static void BeginInvokeAsRequired(
            this ISynchronizer synchronizer,
            Action action)
        {
            if (synchronizer.IsInvokeRequired)
            {
                synchronizer.BeginInvoke(action);
            }
            else
            {
                action();
            }
        }

        public static void InvokeAsRequired(
            this ISynchronizer synchronizer,
            Action action)
        {
            if (synchronizer.IsInvokeRequired)
            {
                synchronizer.Invoke(action);
            }
            else
            {
                action();
            }
        }
    }
}
