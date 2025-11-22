using System.Threading.Tasks;
using Avalonia.Platform.Storage;
using CommunityToolkit.Mvvm.Input;
using TuDog.Bootstrap;
using TuDog.IocAttribute;

namespace Demo.ViewModels;

[Register]
public partial class SystemDialogViewModel : DialogViewModelBaseAsync<bool, object>
{
    [RelayCommand]
    public async Task open()
    {
        var res = await TuDogApplication.TopLevel.StorageProvider.OpenFolderPickerAsync(new FolderPickerOpenOptions
        {
            AllowMultiple = false,
            Title = "选择文件夹"
        });
    }

    public override Task<object?> ConfirmAsync()
    {
        return Task.FromResult(default(object));
    }

    public override Task<object?> CancelAsync()
    {
        return Task.FromResult(default(object));
    }
}