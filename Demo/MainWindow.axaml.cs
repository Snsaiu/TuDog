using Avalonia.Interactivity;
using Demo.ViewModels;
using DryIoc;
using FluentAvalonia.UI.Windowing;
using TuDog.Bootstrap;
using TuDog.Interfaces.MessageBarService;
using TuDog.Interfaces.RegionManagers;

namespace Demo;

public partial class MainWindow : AppWindow
{
    private const string regionName = "mainContainer";

    private IRegionManager _regionManager;

    private IMessageBarService _messageBarService;
        

    public MainWindow()
    {
        InitializeComponent();
        _regionManager = TuDogApplication.ServiceProvider.Resolve<IRegionManager>();
        _messageBarService = TuDogApplication.ServiceProvider.Resolve<IMessageBarService>();
    }

    private void OpenWaitingDialog(object? sender, RoutedEventArgs e)
    {
        _regionManager.AddToRegion<ProgressDialogViewModel>(regionName);
    }

    private void OpenTextInputDialog(object? sender, RoutedEventArgs e)
    {
        _regionManager.AddToRegion<TextInputDialogViewModel>(regionName);
    }

    private void OpenCustomDialog(object? sender, RoutedEventArgs e)
    {
        _regionManager.AddToRegion<CustomDialogViewModel>(regionName);
    }

    private void ShowMessageBox(object? sender, RoutedEventArgs e)
    {
        _regionManager.AddToRegion<MessageDialogViewModel>(regionName);
      //  _messageBarService.ShowSuccess("This is a success message!","info",true);
    }
}