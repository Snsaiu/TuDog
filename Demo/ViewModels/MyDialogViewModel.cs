using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using TuDog.Bootstrap;
using TuDog.Enums;
using TuDog.IocAttribute;

namespace Demo.ViewModels;

public sealed partial class CustomDialogData : ModelBase
{
    [ObservableProperty] private string _message;

    [ObservableProperty] private bool _isSync;
}

[Register]
public partial class MyDialogViewModel : DialogViewModelBaseAsync<string, CustomDialogData>
{
    [ObservableProperty] private CustomDialogData _data = new();

    protected override Task OnLoaded()
    {
        Data.Message = Parameter;
        return base.OnLoaded();
    }

    public override Task<CustomDialogData> ConfirmAsync()
    {
        return Task.FromResult(Data);
    }

    public override Task<bool> CanConfirmAsync()
    {
        if (string.IsNullOrEmpty(Data.Message))
        {
            ErrorMessageAction("message不能为空", "错误", MessageState.Error);
            return Task.FromResult(false);
        }

        return Task.FromResult(true);
    }

    public override Task<CustomDialogData> CancelAsync()
    {
        return Task.FromResult<CustomDialogData>(null);
    }
}