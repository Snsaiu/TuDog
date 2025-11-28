using System;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TuDog.Bootstrap;
using TuDog.Interfaces.IDialogServers;
using TuDog.Interfaces.MessageBarService;
using TuDog.IocAttribute;
using TuDog.Models;

namespace Demo.ViewModels;

[Register]
public partial class TextInputDialogViewModel(IDialogServer dialogServer, IMessageBarService messageBarService)
    : TuDogViewModelBase
{
    [ObservableProperty] private string _resultMessage = string.Empty;

    [RelayCommand]
    private async Task BaseUsage()
    {
        var result = await dialogServer.ShowInputDialogAsync(string.Empty);
        Display(result);
    }

    [RelayCommand]
    private async Task TextHasContent()
    {
        var result = await dialogServer.ShowInputDialogAsync("原始内容");
        Display(result);
    }

    [RelayCommand]
    private async Task LimitLengthContent()
    {
        var result = await dialogServer.ShowInputDialogAsync(string.Empty, maxLength: 10);
        Display(result);
    }

    private void Display(DialogResultData<string> result)
    {
        if (result is not { Ok: true, Data: var data })
        {
            return;
        }

        ResultMessage = data;
    }
}