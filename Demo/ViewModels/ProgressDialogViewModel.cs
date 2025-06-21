using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Input;
using TuDog.Bootstrap;
using TuDog.Interfaces.IDialogServers;
using TuDog.IocAttribute;

namespace Demo.ViewModels;

[Register]
public partial class ProgressDialogViewModel(IDialogServer dialogServer) : TuDogViewModelBase
{
    [RelayCommand]
    private async Task Show()
    {
        using var ds = dialogServer.ShowProgressDialog("title", "subtitle", "Cancel");

        await Task.Delay(5000);
        ds.Progress("2", "3", null);
        await Task.Delay(5000);
        // ds.CancellationToken.ThrowIfCancellationRequested();
    }
}