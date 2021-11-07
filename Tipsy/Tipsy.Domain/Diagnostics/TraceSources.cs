// ****************************************************************************
// NOTE: This file was automatically generated from a text template.
//       Modify at your own risk.
// ****************************************************************************
// <Copyright>Copyright (c) Birklid Software. All rights reserved.</Copyright>
// <File>TraceSources.cs</File>
// <Company>Birklid Software</Company>
// <Author>Ray Birklid</Author>
// <Email>Ray.Birklid@gmail.com</Email>
// <Date>11/6/2021 10:51:34 PM</Date>
namespace Com.Gmail.Birklid.Ray.Tipsy.Diagnostics
{
    using System.Diagnostics;

    public class Logger : TraceSource
    {
        protected Logger(
            string name)
            : base(name)
        {
        }
    }

    /// <summary>
    /// A singleton implementation of a TraceSource used for the application layer.
    /// </summary>
    public class ApplicationTraceSource : Logger
    {
        #region Private Fields

        private static readonly ApplicationTraceSource instance = new ApplicationTraceSource();

        #endregion Private Fields

        #region Creation

        private ApplicationTraceSource()
            : base("Application")
        {
        }

        #endregion Creation

        /// <summary>
        /// Gets the instance of this TraceSource.
        /// </summary>
        public static Logger Instance { get { return ApplicationTraceSource.instance; } }
    }

    /// <summary>
    /// A singleton implementation of a TraceSource used for 'controller' types; generally in the application layer.
    /// </summary>
    public class ControllerTraceSource : Logger
    {
        #region Private Fields

        private static readonly ControllerTraceSource instance = new ControllerTraceSource();

        #endregion Private Fields

        #region Creation

        private ControllerTraceSource()
            : base("Controller")
        {
        }

        #endregion Creation

        /// <summary>
        /// Gets the instance of this TraceSource.
        /// </summary>
        public static Logger Instance { get { return ControllerTraceSource.instance; } }
    }

    /// <summary>
    /// A singleton implementation of a TraceSource used for the data layer.
    /// </summary>
    public class DataTraceSource : Logger
    {
        #region Private Fields

        private static readonly DataTraceSource instance = new DataTraceSource();

        #endregion Private Fields

        #region Creation

        private DataTraceSource()
            : base("Data")
        {
        }

        #endregion Creation

        /// <summary>
        /// Gets the instance of this TraceSource.
        /// </summary>
        public static Logger Instance { get { return DataTraceSource.instance; } }
    }

    /// <summary>
    /// A singleton implementation of a TraceSource used for data-model types.
    /// </summary>
    public class DataModelTraceSource : Logger
    {
        #region Private Fields

        private static readonly DataModelTraceSource instance = new DataModelTraceSource();

        #endregion Private Fields

        #region Creation

        private DataModelTraceSource()
            : base("DataModel")
        {
        }

        #endregion Creation

        /// <summary>
        /// Gets the instance of this TraceSource.
        /// </summary>
        public static Logger Instance { get { return DataModelTraceSource.instance; } }
    }

    /// <summary>
    /// A singleton implementation of a TraceSource catch all/default.
    /// </summary>
    public class GlobalTraceSource : Logger
    {
        #region Private Fields

        private static readonly GlobalTraceSource instance = new GlobalTraceSource();

        #endregion Private Fields

        #region Creation

        private GlobalTraceSource()
            : base("Global")
        {
        }

        #endregion Creation

        /// <summary>
        /// Gets the instance of this TraceSource.
        /// </summary>
        public static Logger Instance { get { return GlobalTraceSource.instance; } }
    }

    /// <summary>
    /// A singleton implementation of a TraceSource used for types concerned with i/o.
    /// </summary>
    public class IOTraceSource : Logger
    {
        #region Private Fields

        private static readonly IOTraceSource instance = new IOTraceSource();

        #endregion Private Fields

        #region Creation

        private IOTraceSource()
            : base("IO")
        {
        }

        #endregion Creation

        /// <summary>
        /// Gets the instance of this TraceSource.
        /// </summary>
        public static Logger Instance { get { return IOTraceSource.instance; } }
    }

    /// <summary>
    /// A singleton implementation of a TraceSource used for service types.
    /// </summary>
    public class ServiceTraceSource : Logger
    {
        #region Private Fields

        private static readonly ServiceTraceSource instance = new ServiceTraceSource();

        #endregion Private Fields

        #region Creation

        private ServiceTraceSource()
            : base("Service")
        {
        }

        #endregion Creation

        /// <summary>
        /// Gets the instance of this TraceSource.
        /// </summary>
        public static Logger Instance { get { return ServiceTraceSource.instance; } }
    }

    /// <summary>
    /// A singleton implementation of a TraceSource used for test classes.
    /// </summary>
    public class TestTraceSource : Logger
    {
        #region Private Fields

        private static readonly TestTraceSource instance = new TestTraceSource();

        #endregion Private Fields

        #region Creation

        private TestTraceSource()
            : base("Test")
        {
        }

        #endregion Creation

        /// <summary>
        /// Gets the instance of this TraceSource.
        /// </summary>
        public static Logger Instance { get { return TestTraceSource.instance; } }
    }

    /// <summary>
    /// A singleton implementation of a TraceSource used for types concerned with threading/concurrency.
    /// </summary>
    public class ThreadingTraceSource : Logger
    {
        #region Private Fields

        private static readonly ThreadingTraceSource instance = new ThreadingTraceSource();

        #endregion Private Fields

        #region Creation

        private ThreadingTraceSource()
            : base("Threading")
        {
        }

        #endregion Creation

        /// <summary>
        /// Gets the instance of this TraceSource.
        /// </summary>
        public static Logger Instance { get { return ThreadingTraceSource.instance; } }
    }

    /// <summary>
    /// A singleton implementation of a TraceSource used for view-model types.
    /// </summary>
    public class ViewModelTraceSource : Logger
    {
        #region Private Fields

        private static readonly ViewModelTraceSource instance = new ViewModelTraceSource();

        #endregion Private Fields

        #region Creation

        private ViewModelTraceSource()
            : base("ViewModel")
        {
        }

        #endregion Creation

        /// <summary>
        /// Gets the instance of this TraceSource.
        /// </summary>
        public static Logger Instance { get { return ViewModelTraceSource.instance; } }
    }
}
