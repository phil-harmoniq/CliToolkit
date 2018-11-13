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
        internal static readonly Assembly Assembly = Assembly.GetEntryAssembly();
        internal static readonly OperatingSystem OS = Environment.OSVersion;
        internal static readonly string NewLine = Environment.NewLine;
        internal static readonly FileVersionInfo FileVersionInfo = FileVersionInfo.GetVersionInfo(Assembly.Location);
        
        /// <summary>
        /// This application's name.
        /// </summary>
        public string Name { get; internal set; }

        /// <summary>
        /// This application's assembly version.
        /// </summary>
        public string Version { get; internal set; }

        /// <summary>
        /// Defines the width of the CLI window.
        /// </summary>
        public int Width { get; internal set; }

        /// <summary>
        /// Defines the color that will be used to print the title in the header.
        /// </summary>
        /// <value></value>
        public ConsoleColor TitleColor { get; internal set; }
        
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
            Name = AppInfo.Assembly.GetName().Name;
            Version = AppInfo.FileVersionInfo.ProductVersion;
            TitleColor = ConsoleColor.Cyan;
            Width = 64;

            IsLinux = RuntimeInformation.IsOSPlatform(OSPlatform.Linux);
            IsMacOS = RuntimeInformation.IsOSPlatform(OSPlatform.OSX);
            IsWindows = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
        }
    }
}