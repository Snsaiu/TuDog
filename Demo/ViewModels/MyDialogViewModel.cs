using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using TuDog.Bootstrap;
using TuDog.IocAttribute;

namespace Demo.ViewModels;


public sealed class CustomDialogData
{
    public string Message { get; set; } = string.Empty;
    
    public bool IsSync { get; set; }
}

[Register]
public partial class MyDialogViewModel:DialogViewModelBaseAsync<string,CustomDialogData>
{
    [ObservableProperty]
    private CustomDialogData _data = new CustomDialogData();

    protected override Task OnLoaded()
    {
        Data.Message = Parameter;
        return base.OnLoaded();
    }

    public override Task<CustomDialogData> ConfirmAsync()
    {
        return Task.FromResult(Data);
    }

    public override Task<CustomDialogData> CancelAsync()
    {
        return Task.FromResult<CustomDialogData>(null);
    }
}