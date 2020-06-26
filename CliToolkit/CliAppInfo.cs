using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CliToolkit
{
    public class CliAppInfo
    {
        internal IConfiguration Configuration { get; set; }
        internal IServiceCollection ServiceCollection { get; set; }
        public int ExitCode { get; internal set; }
        public string Name { get; internal set; }
        public string Version { get; internal set; }
        public  int Width { get; internal set; }
    }
}
