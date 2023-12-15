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
    using TimeAndTune.Pages;

    public partial class App : Application
    {
        private readonly ILogger logger = Log.ForContext<MainWindow>();

        protected override void OnStartup(StartupEventArgs e)
        {   
            ConfigureLogging();
            SubscribeToExceptionEvents();
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

        private void SubscribeToExceptionEvents()
        {
            this.DispatcherUnhandledException += OnDispatcherUnhandledException;
        }

        private void OnDispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            Log.Error("Unhandled exception occurred");

            e.Handled = false;
        }
    }
}
