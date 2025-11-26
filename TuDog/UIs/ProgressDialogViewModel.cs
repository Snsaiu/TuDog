using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TuDog.Bootstrap;
using TuDog.Enums;
using TuDog.Interfaces;

namespace TuDog.UIs;

internal sealed partial class ProgressDialogViewModel : TuDogViewModelBase, IViewModelResultAsync
{
    [ObservableProperty] private string _subTitle = string.Empty;

    [ObservableProperty] private bool _isIndeterminate = true;

    [ObservableProperty] private double _value = 0;

    public IAsyncRelayCommand LoadedCommand { get; }
    public IAsyncRelayCommand UnLoadedCommand { get; }
    public Action<string, string, MessageState> ErrorMessageAction { get; set; }

    public Task<object?> ConfirmAsync()
    {
        return Task.FromResult<object?>(true);
    }

    public Task<object?> CancelAsync()
    {
        return Task.FromResult<object?>(false);
    }

    public Task<bool> CanConfirmAsync()
    {
        return Task.FromResult(true);
    }
}