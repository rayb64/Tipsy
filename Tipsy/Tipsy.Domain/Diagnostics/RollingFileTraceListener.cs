// <Copyright>Copyright (c) Birklid Software. All rights reserved.</Copyright>
// <File>RollingFileTraceListener.cs</File>
// <Company>Birklid Software</Company>
// <Author>Ray Birklid</Author>
// <Email>Ray.Birklid@gmail.com</Email>
// <Date>11/5/2021 6:42:26 PM</Date>
namespace Com.Gmail.Birklid.Ray.Tipsy.Diagnostics
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Reflection;
    using System.Text.RegularExpressions;

    /// <summary>
    /// Extends TextWriterTraceListener to prevent massive log files.
    /// </summary>
    /// <remarks>
    /// The previous log file is renamed and retained in the original folder.
    /// These renamed files will be retained until they 'age out' which is
    /// currently determined by Properties.Settings.Default.MaximumLogAge.
    /// </remarks>
    public class RollingFileTraceListener : TextWriterTraceListener
    {
        #region Private Fields

        private static readonly string _defaultExtension = ".log";

        #endregion Private Fields

        #region Creation

        public RollingFileTraceListener(
            string initializeData)
            : this(GetWriter(initializeData))
        {
        }

        private RollingFileTraceListener(
            TextWriter writer)
            : base(writer)
        {
            if (writer is StreamWriter sw)
            {
                if (sw.BaseStream is FileStream fs)
                {
                    FileName = fs.Name;
                }
            }
        }

        #endregion Creation

        public string FileName { get; private set; }

        #region Private Members

        private static void EnsureDirectory(
            string path)
        {
            var directory = Path.GetDirectoryName(path);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
        }

        private static void DeleteOldLogs(
            string path)
        {
            // typical path = "c:\\temp\\testing\\logs\\Tipsy.Test.log"
            // typical 'rolled' log file name = "Tipsy.Test.11_05_2021_19_14_08.log"
            var extension = Path.GetExtension(path);
            var searchPattern = "*" + extension;
            var directory = Path.GetDirectoryName(path);
            var regexPattern = "\\\\" + Regex.Escape(Path.GetFileNameWithoutExtension(path) + ".") + "(\\d+_){5}\\d+" + Regex.Escape(extension);
            var regex = new Regex(regexPattern);
            try
            {
                var files = Directory.GetFiles(directory, searchPattern);
                foreach (var file in files)
                {
                    if (regex.IsMatch(file))
                    {
                        var fileAge = DateTime.Now - File.GetCreationTime(file);
                        if (fileAge > Properties.Settings.Default.MaximumLogAge)
                        {
                            File.Delete(file);
                        }
                    }
                }
            }
            catch (IOException)
            {
                // There's nothing I can do about it, but the control flow must continue
            }
        }

        private static void RenameLatestLog(
            string path)
        {
            // IMPORTANT: If you modify this, you will likely break the 'DeleteOldLogs' method.
            if (File.Exists(path))
            {
                var creationTime = File.GetCreationTime(path);
                var fileName = Path.GetFileNameWithoutExtension(path);
                var extension = Path.GetExtension(path);
                var directory = Path.GetDirectoryName(path);
                var target = Path.Combine(directory, fileName + "." + creationTime.ToString("MM_dd_yyyy_HH_mm_ss") + extension);
                File.Move(path, target);
            }
        }

        private static string GetPath(
            string initializeData)
        {
            var assembly = Assembly.GetEntryAssembly() ?? Assembly.GetCallingAssembly() ?? typeof(RollingFileTraceListener).Assembly;
            initializeData = string.IsNullOrWhiteSpace(initializeData) ? Path.GetFileNameWithoutExtension(assembly.Location) + _defaultExtension : initializeData;
            var uri = new Uri(initializeData, UriKind.RelativeOrAbsolute);
            if (!uri.IsAbsoluteUri)
            {
                uri = new Uri(assembly.Location + "/../" + initializeData, UriKind.Absolute);
            }
            return uri.LocalPath;
        }

        private static TextWriter GetWriter(
            string initializeData)
        {
            var path = GetPath(initializeData);
            try
            {
                EnsureDirectory(path);
                RenameLatestLog(path);
                DeleteOldLogs(path);
            }
            catch (IOException)
            {
                // It is possible that another instance of the application may try to open the same file.
                // I'm going to solve that problem later.
                // For now, return a unique writer:
                path = Path.Combine(Path.GetDirectoryName(path), Path.GetFileNameWithoutExtension(path) + ".access-denied." + Guid.NewGuid() + _defaultExtension);
            }
            return new StreamWriter(path);
        }

        #endregion Private Members
    }
}
