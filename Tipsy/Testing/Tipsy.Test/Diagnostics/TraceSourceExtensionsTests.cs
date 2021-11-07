// <Copyright>Copyright (c) Birklid Software. All rights reserved.</Copyright>
// <File>TraceSourceExtensionsTests.cs</File>
// <Company>Birklid Software</Company>
// <Author>Ray Birklid</Author>
// <Email>Ray.Birklid@gmail.com</Email>
// <Date>11/6/2021 11:00:04 PM</Date>
namespace Com.Gmail.Birklid.Ray.Tipsy.Test.Diagnostics
{
    using Com.Gmail.Birklid.Ray.Tipsy.Diagnostics;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;

    [TestClass]
    public class TraceSourceExtensionsTests
    {
        // NOTE: I'm not going to test every one of the 'Basic Tracing Methods' at this time,
        // since they are all created via the TraceSourceExtensions.tt file.

        #region Private Fields

        private Logger _log;

        #endregion Private Fields

        [TestCleanup]
        public void OnTestCleanup()
        {
            (_log.Listeners[0] as TraceListener).Dispose();
            _log.Listeners.Clear();
        }

        [TestMethod]
        public void TestCreated()
        {
            _log = InitializeLogger(DataTraceSource.Instance);
            _log.Created(this);
            CheckBasicMessage(TraceEventType.Verbose, "Created instance of " + GetType().Name);
        }

        [TestMethod]
        public void TestCritical()
        {
            _log = InitializeLogger(ApplicationTraceSource.Instance);
            var type = TraceEventType.Critical;
            var text = "Oh no!";
            _log.Critical(text);
            CheckBasicMessage(type, text);
            text = "Why me!";
            _log.Critical(text);
            CheckBasicMessage(type, text);
            text = "Why us!";
            _log.Critical(text);
            CheckBasicMessage(type, text);
            text = "Why anybody!";
            _log.Critical(text);
            CheckBasicMessage(type, text);
            _log.Critical("one = {0}", 1);
            CheckBasicMessage(type, "one = 1");
            _log.Critical(3.14159);
            CheckBasicMessage(type, "3.14159");
        }

        [TestMethod]
        public void TestError()
        {
            _log = InitializeLogger(GlobalTraceSource.Instance);
            var type = TraceEventType.Error;
            var text = "Oh no!";
            _log.Error(text);
            CheckBasicMessage(type, text);
            text = "Why me!";
            _log.Error(text);
            CheckBasicMessage(type, text);
            text = "Why us!";
            _log.Error(text);
            CheckBasicMessage(type, text);
            text = "Why anybody!";
            _log.Error(text);
            CheckBasicMessage(type, text);
            _log.Error("one = {0}", 1);
            CheckBasicMessage(type, "one = 1");
            _log.Error(3.14159);
            CheckBasicMessage(type, "3.14159");
        }

        [TestMethod]
        public void TestMethodCall()
        {
            _log = InitializeLogger(DataTraceSource.Instance);
            _log.MethodCall(this);
            CheckBasicMessage(TraceEventType.Verbose, "Method called " + GetType().Name + ":TestMethodCall");
        }

        [TestMethod]
        public void TestVerbose()
        {
            _log = InitializeLogger(ServiceTraceSource.Instance);
            var type = TraceEventType.Verbose;
            var text = "Oh no!";
            _log.Verbose(text);
            CheckBasicMessage(type, text);
            text = "Why me!";
            _log.Verbose(text);
            CheckBasicMessage(type, text);
            text = "Why us!";
            _log.Verbose(text);
            CheckBasicMessage(type, text);
            text = "Why anybody!";
            _log.Verbose(text);
            CheckBasicMessage(type, text);
            _log.Verbose("one = {0}", 1);
            CheckBasicMessage(type, "one = 1");
            _log.Verbose(3.14159);
            CheckBasicMessage(type, "3.14159");
        }

        #region Private Members

        private void CheckBasicMessage(
            TraceEventType eventType,
            string expectedText)
        {
            var regex = new Regex("^(?<name>" + _log.Name + ")\\s+(?<event>" + eventType + "):\\s+\\d+\\s+:\\s+(?<text>.+)\r\n$");
            var text = GetLastLoggedLine();
            var match = regex.Match(text);
            Assert.IsTrue(match.Success, "Failed to match: {0}", text);
            Assert.AreEqual(_log.Name, match.Groups["name"].Value);
            Assert.AreEqual(eventType.ToString(), match.Groups["event"].Value);
            Assert.AreEqual(expectedText, match.Groups["text"].Value);
        }

        private string GetLastLoggedLine()
        {
            if (_log.Listeners[0] is TextWriterTraceListener tw)
            {
                if (tw.Writer is MyWriter mw)
                {
                    return mw.Lines.Last();
                }
                return tw.Writer.ToString();
            }
            return string.Empty;
        }

        private static Logger InitializeLogger(
            Logger logger)
        {
            var listener = new TextWriterTraceListener { Writer = new MyWriter(), Filter = new EventTypeFilter(SourceLevels.All) };
            logger.Listeners.Clear();
            logger.Listeners.Add(listener);
            logger.Listeners.Add(new ConsoleTraceListener());
            return logger;
        }

        private class MyWriter : StringWriter
        {
            private readonly StringBuilder _builder = new StringBuilder();
            private readonly List<string> _lines = new List<string>();

            public List<string> Lines => _lines;

            public override void Write(string value)
            {
                base.Write(value);
                _builder.Append(value);
            }
            public override void WriteLine(string value)
            {
                base.WriteLine(value);
                _builder.AppendLine(value);
                _lines.Add(_builder.ToString());
                _builder.Clear();
            }
        }

        #endregion Private Members
    }
}
