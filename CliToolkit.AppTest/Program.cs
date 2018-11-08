using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CliToolkit;
using CliToolkit.Arguments;
using CliToolkit.Arguments.Styles;
using CliToolkit.Exceptions;

namespace CliToolkit.AppTest
{
    class Program : CliApp
    {
        static void Main(string[] args)
        {
            var app = new AppBuilder<Program>()
                .SetName("File List")
                .Start(args);
        }

        public Flag HiddenFlag = new Flag("Show hidden files", "hidden", 'h');
        public Flag VerboseFlag = new Flag("Show header and footer information", "verbose", 'v');

        public override void OnExecute(string[] args)
        {
            if (VerboseFlag.IsActive) { PrintHeader(); }
            
            var path = args.Length < 1 ? Directory.GetCurrentDirectory() : Path.GetFullPath(args[0]);
            if (!Directory.Exists(path)) new AppRuntimeException($"{path} is an invalid path");

            var allFiles = new Dictionary<string, FileAttributes>();

            var directories = Directory.GetDirectories(path);
            var files = Directory.GetFiles(path);
            foreach (var d in directories) { allFiles.Add(Path.GetFileName(d), File.GetAttributes(d)); }
            foreach (var f in files) { allFiles.Add(Path.GetFileName(f), File.GetAttributes(f)); }

            Console.WriteLine($"Path: {path}{Environment.NewLine}");

            foreach (var file in allFiles.OrderBy(f => f.Key))
            {
                var isHidden = file.Value.HasFlag(FileAttributes.Hidden);
                var isDirectory = file.Value.HasFlag(FileAttributes.Directory);
                var isReadOnly = file.Value.HasFlag(FileAttributes.ReadOnly);

                if (!isHidden || isHidden && HiddenFlag.IsActive)
                {
                    var print = file.Key;

                    if (isDirectory)
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        print += Path.DirectorySeparatorChar;
                    }

                    Console.WriteLine(print);
                    Console.ResetColor();
                }
            }
        }
    }
}
