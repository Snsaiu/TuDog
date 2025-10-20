using System.Threading.Tasks;
using TuDog.Bootstrap;
using TuDog.IocAttribute;

namespace Demo.ViewModels;

[Register]
public partial class MyDialogNoReturnViewModel:DialogViewModelBaseAsync
{
    public override Task<object?> ConfirmAsync()
    {
        return new Task<object?>(null);
    }

    public override Task<object?> CancelAsync()
    {
        return Task.FromResult<object?>(null);
    }
}