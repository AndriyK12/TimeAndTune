namespace TimeAndTune
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows;
    using Microsoft.Extensions.DependencyInjection;
    using Serilog;
    using Serilog.Events;

    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            ConfigureLogging();

            base.OnStartup(e);
        }

        private static void ConfigureLogging()
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Debug()
                .WriteTo.File("log.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();
        }
    }
}
