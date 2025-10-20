using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Input;
using TuDog.Bootstrap;
using TuDog.Interfaces.IDialogServers;
using TuDog.IocAttribute;

namespace Demo.ViewModels;

[Register]
public partial class CustomDialogViewModel(IDialogServer dialogServer) : TuDogViewModelBase
{
    [RelayCommand]
    public async Task Show()
    {
        var result =
            await dialogServer.ShowDialogAsync<MyDialogViewModel, string, CustomDialogData>("自定义", parameter: "custom");
    }
}