using Microsoft.Extensions.Configuration;

namespace CliToolkit.TestApp
{
    class Program
    {
        static int Main(string[] args)
        {
            var app = new CliAppBuilder<ApplicationRoot>()
                .SetName("CLI Test App")
                .Configure(c => c.AddJsonFile("appsettings.json"))
                //.RegisterServices(RegisterServices)
                .Start(args);
            return app.AppInfo.ExitCode;
        }
    }
}
