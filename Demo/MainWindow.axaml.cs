using Avalonia.Controls;
using Avalonia.Interactivity;
using Demo.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using TuDog.Bootstrap;
using TuDog.Interfaces.RegionManagers;

namespace Demo;

public partial class MainWindow : Window
{
    private const string regionName = "mainContainer";

    private IRegionManager _regionManager;

    public MainWindow()
    {
        InitializeComponent();
        _regionManager = TuDogApplication.ServiceProvider.GetRequiredService<IRegionManager>();
    }

    private void OpenWaitingDialog(object? sender, RoutedEventArgs e)
    {
        _regionManager.AddToRegion<ProgressDialogViewModel>(regionName);
    }

    private void OpenTextInputDialog(object? sender, RoutedEventArgs e)
    {
        _regionManager.AddToRegion<TextInputDialogViewModel>(regionName);
    }
}