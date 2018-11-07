using System;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;

namespace CliToolkit.Meta
{
    /// <summary>
    /// Information about this application and its runtime environment.
    /// </summary>
    public sealed class AppInfo
    {
        internal readonly Assembly Assembly;
        internal readonly OperatingSystem OS = Environment.OSVersion;
        internal readonly string NewLine = Environment.NewLine;
        internal readonly FileVersionInfo FileVersionInfo;
        
        /// <summary>
        /// True if this application is running in Linux.
        /// </summary>
        public readonly bool IsLinux;

        /// <summary>
        /// True if this application is running in Linux.
        /// </summary>
        public readonly bool IsMacOS;

        /// <summary>
        /// True if this application is running in Linux.
        /// </summary>
        public readonly bool IsWindows;

        internal AppInfo()
        {
            Assembly = Assembly.GetEntryAssembly();
            NewLine = Environment.NewLine;
            OS = Environment.OSVersion;
            FileVersionInfo = FileVersionInfo.GetVersionInfo(Assembly.Location);

            IsLinux = RuntimeInformation.IsOSPlatform(OSPlatform.Linux);
            IsMacOS = RuntimeInformation.IsOSPlatform(OSPlatform.OSX);
            IsWindows = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
        }
    }
}