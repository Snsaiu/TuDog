using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Input;
using DryIoc;
using TuDog.Bootstrap;
using TuDog.Interfaces.IDialogServers;
using TuDog.Interfaces.MessageBarService;
using TuDog.Interfaces.RegionManagers;

namespace Demo.ViewModels;

public partial class MainWindowViewModel : TuDogWindowViewModelBase
{
    private IRegionManager _regionManager;

    private const string regionName = "mainContainer";

    private IDialogServer _dialogServer;

    private IMessageBarService _messageBarService;

    public MainWindowViewModel()
    {
        _regionManager = TuDogApplication.ServiceProvider.Resolve<IRegionManager>();
        _messageBarService = TuDogApplication.ServiceProvider.Resolve<IMessageBarService>();
        _dialogServer = TuDogApplication.ServiceProvider.Resolve<IDialogServer>();
    }

    [RelayCommand]
    public void ShowWaitDialog()
    {
        _regionManager.AddToRegion<ProgressDialogViewModel>(regionName);
    }

    [RelayCommand]
    public void ShowTextInputDialog()
    {
        _regionManager.AddToRegion<TextInputDialogViewModel>(regionName);
    }

    [RelayCommand]
    public void ShowConfirmDialog()
    {
        _dialogServer.ShowConfirmDialogAsync("Are you sure to delete this file?", "Confirmation", "Yes", "No");
    }

    [RelayCommand]
    public void ShowCustomDialog()
    {
        _regionManager.AddToRegion<CustomDialogViewModel>(regionName);
    }

    [RelayCommand]
    public void ShowMessageBar()
    {
        _regionManager.AddToRegion<MessageDialogViewModel>(regionName);
    }

    protected override Task OnLoaded()
    {
        return base.OnLoaded();
    }
}