using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core.Plugins;
using Avalonia.Media;
using DryIoc;
using TuDog.Bases.Regions;
using TuDog.Bases.Regions.Impl;
using TuDog.Interfaces;
using TuDog.Interfaces.IDialogServers;
using TuDog.Interfaces.IDialogServers.Impl;
using TuDog.Interfaces.MessageBarService;
using TuDog.Interfaces.MessageBarService.Impl;
using TuDog.Interfaces.Navigations;
using TuDog.Interfaces.Navigations.Impl;
using TuDog.Interfaces.PreferenceServices;
using TuDog.Interfaces.PreferenceServices.Impl;
using TuDog.Interfaces.RegionManagers;
using TuDog.Interfaces.RegionManagers.Impl;
using TuDog.IocContainers;
using TuDog.IocContainers.Impl;
using TuDog.ViewLocators;
using TuDog.ViewLocators.Impl;


namespace TuDog.Bootstrap;

public abstract class TuDogApplication : Application
{
    public static IContainer ServiceProvider { get; } = new Container();

    public static TopLevel? TopLevel { get; set; }

    public static Window? MainWindow { get; private set; }

    public override void Initialize()
    {
        AutoRegister(ServiceProvider);
        SystemServiceRegister(ServiceProvider);
        Register(ServiceProvider);
        InitGlobalExceptionHandlers();
    }

    protected virtual void AutoRegister(IContainer collection)
    {
    }

    protected void SystemServiceRegister(IContainer collection)
    {
        collection.Register<ViewLocatorBase, ViewLocator>(Reuse.Singleton);
        collection.Register<RegionContainerBase, RegionContainer>(Reuse.Singleton);
        collection.Register<ITuDogContainer, TuDogContainer>(Reuse.Singleton);
        collection.Register<IRegionManager, RegionManager>(Reuse.Singleton);
        collection.Register<IDialogServer, DialogServer>(Reuse.Singleton);
        collection.Register<IPreferenceService, JsonPreferenceService>(Reuse.Singleton);
        collection.RegisterInstance<INavigationService>(new NavigationService(ApplicationLifetime));
        collection.Register<IMessageBarService, MessageBarService>(Reuse.Singleton);
    }

    private void DisableAvaloniaDataAnnotationValidation()
    {
        // Get an array of plugins to remove
        var dataValidationPluginsToRemove =
            BindingPlugins.DataValidators.OfType<DataAnnotationsValidationPlugin>().ToArray();

        // remove each entry found
        foreach (var plugin in dataValidationPluginsToRemove) BindingPlugins.DataValidators.Remove(plugin);
    }

    public abstract object CreateShell();

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            var window = (Window)CreateShell();
            MainWindow = window;
            DisableAvaloniaDataAnnotationValidation();
            desktop.MainWindow = window;
            TopLevel = TopLevel.GetTopLevel(desktop.MainWindow);
        }
        else if (ApplicationLifetime is ISingleViewApplicationLifetime singleViewPlatform)
        {
            var window = CreateShell();
            //MainWindow =window;
            singleViewPlatform.MainView = (UserControl)window;
            TopLevel = TopLevel.GetTopLevel(singleViewPlatform.MainView);

            if (TopLevel?.InsetsManager is not null and var m)
            {
                m.IsSystemBarVisible = true;
                m.DisplayEdgeToEdge = false;
                m.SystemBarColor = Colors.White;
            }
        }

        base.OnFrameworkInitializationCompleted();
    }


    protected virtual void Register(IContainer collection)
    {
    }

    public void InitGlobalExceptionHandlers()
    {
        // var logger = ServiceProvider.Resolve<ILogger>();
        //
        // AppDomain.CurrentDomain.UnhandledException += (sender, e) =>
        // {
        //     if (e is OperationCanceledException) DialogServer.ProgressDialogWindow.Close();
        //
        //     if (logger is null) return;
        //     var ex = (Exception)e.ExceptionObject;
        //     logger.LogError(ex.Message);
        // };
        //
        // TaskScheduler.UnobservedTaskException += (sender, e) =>
        // {
        //     if (logger is not null) logger.LogError(e.Exception.Message);
        //     e.SetObserved();
        // };
    }
}