using System.Threading.Tasks;
using Avalonia.Interactivity;
using Demo.ViewModels;
using DryIoc;
using FluentAvalonia.UI.Windowing;
using TuDog.Bootstrap;
using TuDog.Interfaces.IDialogServers;
using TuDog.Interfaces.MessageBarService;
using TuDog.Interfaces.RegionManagers;
using TuDog.UIs;

namespace Demo;

public partial class MainWindow : TuDogWindow
{
    private const string regionName = "mainContainer";

    private IRegionManager _regionManager;

    private IDialogServer _dialogServer;

    private IMessageBarService _messageBarService;

    public MainWindow()
    {
        InitializeComponent();
        _regionManager = TuDogApplication.ServiceProvider.Resolve<IRegionManager>();
        _messageBarService = TuDogApplication.ServiceProvider.Resolve<IMessageBarService>();
        _dialogServer = TuDogApplication.ServiceProvider.Resolve<IDialogServer>();
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

    private async void ShowConfirmBox(object? sender, RoutedEventArgs e)
    {
        await _dialogServer.ShowConfirmDialogAsync("Are you sure to delete this file?", "Confirmation", "Yes", "No");
    }
}