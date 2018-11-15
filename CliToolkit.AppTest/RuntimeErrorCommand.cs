using System;

namespace CliToolkit.AppTest
{
    public class RuntimeErrorCommand : Command
    {
        public RuntimeErrorCommand(string description, string keyword) : base(description, keyword)
        {
        }

        public Flag HeaderFlag = new Flag("Displays the header and footer regardless of errors.", "header");

        public override void OnExecute(string[] args)
        {
            if (HeaderFlag.IsActive) { PrintHeader(); }
            throw new AppRuntimeException("This command should display an error but exit gracefully");
        }
    }
}