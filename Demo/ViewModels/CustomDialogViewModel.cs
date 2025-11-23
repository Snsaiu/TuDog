using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Input;
using Demo.Models;
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

    [RelayCommand]
    public Task NoReturnShow()
    {
        return dialogServer.ShowDialogAsync<MyDialogNoReturnViewModel, bool>("custom");
    }

    [RelayCommand]
    public Task ShowSystemDialog()
    {
        return dialogServer.ShowDialogAsync<SystemDialogViewModel, bool, object>("system");
    }

    [RelayCommand]
    public Task OpenFindFatherDialog()
    {
        return dialogServer.ShowDialogAsync<ChildrenViewModel, bool, Children>("systen");
    }
}