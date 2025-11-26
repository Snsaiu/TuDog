using System.Threading.Tasks;
using Avalonia.Threading;
using CommunityToolkit.Mvvm.Input;
using DryIoc;
using TuDog.Bootstrap;
using TuDog.Interfaces.IDialogServers;
using TuDog.Interfaces.MessageBarService;
using TuDog.IocAttribute;

namespace Demo.ViewModels;

[Register]
public partial class MessageDialogViewModel : TuDogViewModelBase
{
    private IMessageBarService _messageBarService = TuDogApplication.ServiceProvider.Resolve<IMessageBarService>();

    private IDialogServer _dialogServer = TuDogApplication.ServiceProvider.Resolve<IDialogServer>();

    [RelayCommand]
    public async Task ShowInfoMessage()
    {
        // _messageBarService.ShowSuccess("This is an info message!","info",true);
        using var progress = _dialogServer.ShowProgressDialog("Loading", "Loading info message");

        await Task.Delay(2000);

        _messageBarService.ShowSuccess("This is an info message!", "info", true);
    }
}