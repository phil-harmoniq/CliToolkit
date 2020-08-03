using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Diagnostics;
using System.Reflection;

namespace CliToolkit
{
    public class CliAppBuilderOld<TApp> where TApp : CliAppOld, new()
    {
        private const int _minimumWidth = 32;
        private const int _maximumWidth = 128;
        private const int _defaultWidth = 72;

        private TApp _app;
        private ServiceCollection _serviceCollection;
        private IConfigurationBuilder _configBuilder;

        public CliAppBuilderOld()
        {
            _app = new TApp();
            _serviceCollection = new ServiceCollection();
            _configBuilder = new ConfigurationBuilder();
            _app.AppInfo.Width = _defaultWidth;
            _serviceCollection.AddOptions();
        }

        public TApp Build()
        {
            var assembly = Assembly.GetEntryAssembly();

            if (string.IsNullOrEmpty(_app.AppInfo.Version))
            {
                var fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
                _app.AppInfo.Version = fvi.ProductVersion;
            }

            if (string.IsNullOrEmpty(_app.AppInfo.Name))
            {
                _app.AppInfo.Name = assembly.GetName().Name;
            }

            _app.AppInfo.ConfigurationBuilder = _configBuilder;
            _app.AppInfo.ServiceCollection = _serviceCollection;
            _app.AppInfo.ServiceCollection.AddSingleton<CliAppOld>(_app);

            return _app;
        }

        public TApp Start(string[] args)
        {
            Build();
            _app.Start(args);
            return _app;
        }

        public CliAppBuilderOld<TApp> Configure(Action<IConfigurationBuilder> configure)
        {
            _app.AppInfo.UserConfigBuilder = configure;
            return this;
        }

        public CliAppBuilderOld<TApp> RegisterServices(Action<IServiceCollection, IConfiguration> register)
        {
            _app.AppInfo.UserServiceRegistration = register;
            return this;
        }

        public CliAppBuilderOld<TApp> SetName(string name)
        {
            if (string.IsNullOrEmpty(name)) { throw new Exception("SetName() was called with an empty string."); }
            _app.AppInfo.Name = name;
            return this;
        }

        public CliAppBuilderOld<TApp> SetVersion(string version)
        {
            if (string.IsNullOrEmpty(version)) { throw new Exception("SetVersion() was called with an empty string."); }
            _app.AppInfo.Version = version;
            return this;
        }

        public CliAppBuilderOld<TApp> SetWidth(int width)
        {
            if (width > _maximumWidth)
            {
                throw new Exception($"Given width {width} is larger than the maximum width {_maximumWidth}");
            }
            if (width < _minimumWidth)
            {
                throw new Exception($"Given width {width} is smaller than the minimum width {_minimumWidth}");
            }

            _app.AppInfo.Width = width;
            return this;
        }
    }

    //public class CliAppBuilder<TApp, TOptions> where TApp : CliApp<TOptions>, new() where TOptions : class
    //{
    //    private const int _minimumWidth = 32;
    //    private const int _maximumWidth = 128;

    //    private TApp _app;
    //    private ServiceCollection _serviceCollection;
    //    private IConfigurationBuilder _configBuilder;

    //    public CliAppBuilder()
    //    {
    //        _app = new TApp();
    //        _serviceCollection = new ServiceCollection();
    //        _configBuilder = new ConfigurationBuilder();
    //    }

    //    public TApp Build()
    //    {
    //        _app.AppInfo.Configuration = _configBuilder.Build();
    //        return _app;
    //    }

    //    public TApp Start(string[] args)
    //    {
    //        _app.AppInfo.Parse(_serviceCollection, _configBuilder.Build(), args);
    //        return _app;
    //    }

    //    public CliAppBuilder<TApp, TOptions> Configure(Action<IConfigurationBuilder> configure)
    //    {
    //        configure(_configBuilder);
    //        return this;
    //    }

    //    public CliAppBuilder<TApp, TOptions> SetName(string name)
    //    {
    //        if (string.IsNullOrEmpty(name)) { throw new Exception("SetName() was called with an empty string."); }
    //        _app.AppInfo.Name = name;
    //        return this;
    //    }

    //    public CliAppBuilder<TApp, TOptions> SetVersion(string version)
    //    {
    //        if (string.IsNullOrEmpty(version)) { throw new Exception("SetVersion() was called with an empty string."); }
    //        _app.AppInfo.Version = version;
    //        return this;
    //    }

    //    public CliAppBuilder<TApp, TOptions> SetWidth(int width)
    //    {
    //        if (width > _maximumWidth)
    //        {
    //            throw new Exception($"Given width {width} is larger than the maximum width {_maximumWidth}");
    //        }
    //        if (width < _minimumWidth)
    //        {
    //            throw new Exception($"Given width {width} is smaller than the minimum width {_minimumWidth}");
    //        }

    //        _app.AppInfo.Width = width;
    //        return this;
    //    }
    //}
}
