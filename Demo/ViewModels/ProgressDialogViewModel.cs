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

        await Task.Delay(2000);
        ds.Progress("2", "3", 30);
        await Task.Delay(4000);
         ds.CancellationToken.ThrowIfCancellationRequested();
         await Task.Delay(4000);
    }
}