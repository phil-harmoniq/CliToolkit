using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace CliToolkit
{
    public class CliAppBuilder<TApp> where TApp : CliApp, new()
    {
        private const int _minimumWidth = 32;
        private const int _maximumWidth = 128;

        private TApp _app;
        private ServiceCollection _serviceCollection;
        private IConfigurationBuilder _configBuilder;

        public CliAppBuilder()
        {
            _app = new TApp();
            _serviceCollection = new ServiceCollection();
            _configBuilder = new ConfigurationBuilder();
        }

        public TApp Build()
        {
            _app.Configuration = _configBuilder.Build();
            _app.ServiceCollection = _serviceCollection;
            return _app;
        }

        public TApp Start(string[] args)
        {
            Build();
            _app.Start(args);
            return _app;
        }
        
        public CliAppBuilder<TApp> Configure(Action<IConfigurationBuilder> configure)
        {
            configure(_configBuilder);
            return this;
        }

        public CliAppBuilder<TApp> RegisterServices(Action<IServiceCollection> register)
        {
            register(_serviceCollection);
            return this;
        }

        public CliAppBuilder<TApp> SetName(string name)
        {
            if (string.IsNullOrEmpty(name)) { throw new Exception("SetName() was called with an empty string."); }
            _app.Name = name;
            return this;
        }

        public CliAppBuilder<TApp> SetVersion(string version)
        {
            if (string.IsNullOrEmpty(version)) { throw new Exception("SetVersion() was called with an empty string."); }
            _app.Version = version;
            return this;
        }

        public CliAppBuilder<TApp> SetWidth(int width)
        {
            if (width > _maximumWidth)
            {
                throw new Exception($"Given width {width} is larger than the maximum width {_maximumWidth}");
            }
            if (width < _minimumWidth)
            {
                throw new Exception($"Given width {width} is smaller than the minimum width {_minimumWidth}");
            }

            _app.Width = width;
            return this;
        }
    }

    public class CliAppBuilder<TApp, TOptions> where TApp : CliApp<TOptions>, new() where TOptions : class
    {
        private const int _minimumWidth = 32;
        private const int _maximumWidth = 128;

        private TApp _app;
        private ServiceCollection _serviceCollection;
        private IConfigurationBuilder _configBuilder;

        public CliAppBuilder()
        {
            _app = new TApp();
            _serviceCollection = new ServiceCollection();
            _configBuilder = new ConfigurationBuilder();
        }

        public TApp Build()
        {
            _app.Configuration = _configBuilder.Build();
            return _app;
        }

        public TApp Start(string[] args)
        {
            _app.Parse(_serviceCollection, _configBuilder.Build(), args);
            return _app;
        }

        public CliAppBuilder<TApp, TOptions> Configure(Action<IConfigurationBuilder> configure)
        {
            configure(_configBuilder);
            return this;
        }

        public CliAppBuilder<TApp, TOptions> SetName(string name)
        {
            if (string.IsNullOrEmpty(name)) { throw new Exception("SetName() was called with an empty string."); }
            _app.Name = name;
            return this;
        }

        public CliAppBuilder<TApp, TOptions> SetVersion(string version)
        {
            if (string.IsNullOrEmpty(version)) { throw new Exception("SetVersion() was called with an empty string."); }
            _app.Version = version;
            return this;
        }

        public CliAppBuilder<TApp, TOptions> SetWidth(int width)
        {
            if (width > _maximumWidth)
            {
                throw new Exception($"Given width {width} is larger than the maximum width {_maximumWidth}");
            }
            if (width < _minimumWidth)
            {
                throw new Exception($"Given width {width} is smaller than the minimum width {_minimumWidth}");
            }

            _app.Width = width;
            return this;
        }
    }
}
