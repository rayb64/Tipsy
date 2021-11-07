// ****************************************************************************
// NOTE: This file was automatically generated from a text template.
//       Modify at your own risk.
// ****************************************************************************
// <Copyright>Copyright (c) Birklid Software. All rights reserved.</Copyright>
// <File>TraceSourceExtensions.cs</File>
// <Company>Birklid Software</Company>
// <Author>Ray Birklid</Author>
// <Email>Ray.Birklid@gmail.com</Email>
// <Date>11/6/2021 10:56:18 PM</Date>
namespace Com.Gmail.Birklid.Ray.Tipsy.Diagnostics
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Globalization;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Text;
    using System.Threading.Tasks;
 
    public static class TraceSourceExtensions
    {
        #region Private Fields

        private static readonly IIdFactory _idFactory = new IdFactory();

        #endregion Private Fields

        #region Basic Tracing Methods

        public static string Critical(
            this TraceSource traceSource,
            string message)
        {
            traceSource.TraceEvent(TraceEventType.Critical, _idFactory.NextInt(traceSource), message);
            return message;
        }

        public static string Critical<T>(
            this TraceSource traceSource,
            T obj)
        {
            return Critical(traceSource, obj == null ? "(null)" : obj.ToString());
        }

        public static string Critical(
            this TraceSource traceSource,
            string format,
            params object[] args)
        {
            var message = string.Format(CultureInfo.InvariantCulture, format, args);
            return Critical(traceSource, message);
        }

        public static string Error(
            this TraceSource traceSource,
            string message)
        {
            traceSource.TraceEvent(TraceEventType.Error, _idFactory.NextInt(traceSource), message);
            return message;
        }

        public static string Error<T>(
            this TraceSource traceSource,
            T obj)
        {
            return Error(traceSource, obj == null ? "(null)" : obj.ToString());
        }

        public static string Error(
            this TraceSource traceSource,
            string format,
            params object[] args)
        {
            var message = string.Format(CultureInfo.InvariantCulture, format, args);
            return Error(traceSource, message);
        }

        public static string Information(
            this TraceSource traceSource,
            string message)
        {
            traceSource.TraceEvent(TraceEventType.Information, _idFactory.NextInt(traceSource), message);
            return message;
        }

        public static string Information<T>(
            this TraceSource traceSource,
            T obj)
        {
            return Information(traceSource, obj == null ? "(null)" : obj.ToString());
        }

        public static string Information(
            this TraceSource traceSource,
            string format,
            params object[] args)
        {
            var message = string.Format(CultureInfo.InvariantCulture, format, args);
            return Information(traceSource, message);
        }

        public static string Resume(
            this TraceSource traceSource,
            string message)
        {
            traceSource.TraceEvent(TraceEventType.Resume, _idFactory.NextInt(traceSource), message);
            return message;
        }

        public static string Resume<T>(
            this TraceSource traceSource,
            T obj)
        {
            return Resume(traceSource, obj == null ? "(null)" : obj.ToString());
        }

        public static string Resume(
            this TraceSource traceSource,
            string format,
            params object[] args)
        {
            var message = string.Format(CultureInfo.InvariantCulture, format, args);
            return Resume(traceSource, message);
        }

        public static string Start(
            this TraceSource traceSource,
            string message)
        {
            traceSource.TraceEvent(TraceEventType.Start, _idFactory.NextInt(traceSource), message);
            return message;
        }

        public static string Start<T>(
            this TraceSource traceSource,
            T obj)
        {
            return Start(traceSource, obj == null ? "(null)" : obj.ToString());
        }

        public static string Start(
            this TraceSource traceSource,
            string format,
            params object[] args)
        {
            var message = string.Format(CultureInfo.InvariantCulture, format, args);
            return Start(traceSource, message);
        }

        public static string Stop(
            this TraceSource traceSource,
            string message)
        {
            traceSource.TraceEvent(TraceEventType.Stop, _idFactory.NextInt(traceSource), message);
            return message;
        }

        public static string Stop<T>(
            this TraceSource traceSource,
            T obj)
        {
            return Stop(traceSource, obj == null ? "(null)" : obj.ToString());
        }

        public static string Stop(
            this TraceSource traceSource,
            string format,
            params object[] args)
        {
            var message = string.Format(CultureInfo.InvariantCulture, format, args);
            return Stop(traceSource, message);
        }

        public static string Suspend(
            this TraceSource traceSource,
            string message)
        {
            traceSource.TraceEvent(TraceEventType.Suspend, _idFactory.NextInt(traceSource), message);
            return message;
        }

        public static string Suspend<T>(
            this TraceSource traceSource,
            T obj)
        {
            return Suspend(traceSource, obj == null ? "(null)" : obj.ToString());
        }

        public static string Suspend(
            this TraceSource traceSource,
            string format,
            params object[] args)
        {
            var message = string.Format(CultureInfo.InvariantCulture, format, args);
            return Suspend(traceSource, message);
        }

        public static string Transfer(
            this TraceSource traceSource,
            string message)
        {
            traceSource.TraceEvent(TraceEventType.Transfer, _idFactory.NextInt(traceSource), message);
            return message;
        }

        public static string Transfer<T>(
            this TraceSource traceSource,
            T obj)
        {
            return Transfer(traceSource, obj == null ? "(null)" : obj.ToString());
        }

        public static string Transfer(
            this TraceSource traceSource,
            string format,
            params object[] args)
        {
            var message = string.Format(CultureInfo.InvariantCulture, format, args);
            return Transfer(traceSource, message);
        }

        public static string Verbose(
            this TraceSource traceSource,
            string message)
        {
            traceSource.TraceEvent(TraceEventType.Verbose, _idFactory.NextInt(traceSource), message);
            return message;
        }

        public static string Verbose<T>(
            this TraceSource traceSource,
            T obj)
        {
            return Verbose(traceSource, obj == null ? "(null)" : obj.ToString());
        }

        public static string Verbose(
            this TraceSource traceSource,
            string format,
            params object[] args)
        {
            var message = string.Format(CultureInfo.InvariantCulture, format, args);
            return Verbose(traceSource, message);
        }

        public static string Warning(
            this TraceSource traceSource,
            string message)
        {
            traceSource.TraceEvent(TraceEventType.Warning, _idFactory.NextInt(traceSource), message);
            return message;
        }

        public static string Warning<T>(
            this TraceSource traceSource,
            T obj)
        {
            return Warning(traceSource, obj == null ? "(null)" : obj.ToString());
        }

        public static string Warning(
            this TraceSource traceSource,
            string format,
            params object[] args)
        {
            var message = string.Format(CultureInfo.InvariantCulture, format, args);
            return Warning(traceSource, message);
        }

        #endregion Basic Tracing Methods

        #region Extended Tracing Methods

        public static string Created<T>(
            this TraceSource traceSource,
            T obj)
        {
            return Created(traceSource, obj.GetType());
        }

        public static string Created(
            this TraceSource traceSource,
            Type type)
        {
            return Verbose(traceSource, "Created instance of {0}", NameFromType(type));
        }

        public static string MethodCall<T>(
            this TraceSource traceSource,
            T obj,
            [CallerMemberName] string methodName = null,
            params object[] args)
        {
            return MethodCall(traceSource, obj.GetType(), methodName, args);
        }

        public static string MethodCall(
            this TraceSource traceSource,
            Type type,
            [CallerMemberName] string methodName = null,
            params object[] args)
        {
            var message = string.Format(CultureInfo.InvariantCulture, "Method called {0}:{1}", NameFromType(type), methodName);
            if (args != null && args.Length != 0)
            {
                message = string.Format(CultureInfo.InvariantCulture, "{0}({1})", message, string.Join(", ", args.Select(e => e == null ? "(null)" : e.ToString())));
            }
            return Verbose(traceSource, message);
        }

        #endregion Extended Tracing Methods

        #region Private Members

        // Used for consistency...  I may want to switch to FullName later
        private static string NameFromType(Type type) => type.Name;

        #endregion Private Members
    }
}