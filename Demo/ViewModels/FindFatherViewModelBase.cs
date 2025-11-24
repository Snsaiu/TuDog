using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using Demo.Models;
using TuDog.Bootstrap;
using TuDog.IocAttribute;

namespace Demo.ViewModels;

[Register]
public abstract partial class FindFatherViewModel<T> : DialogViewModelBaseAsync<bool, T>
{
    [ObservableProperty] private string _title;
}

[Register]
[NoView]
public class ChildrenViewModel : FindFatherViewModel<Children>
{
    public ChildrenViewModel()
    {
        Title = "children";
    }

    public override Task<Children> ConfirmAsync()
    {
        throw new System.NotImplementedException();
    }

    public override Task<Children> CancelAsync()
    {
        return Task.FromResult<Children>(null);
    }
}

public class DogViewModel : FindFatherViewModel<Dog>
{
    public DogViewModel()
    {
        Title = "dog";
    }

    public override Task<Dog> ConfirmAsync()
    {
        throw new System.NotImplementedException();
    }

    public override Task<Dog> CancelAsync()
    {
        throw new System.NotImplementedException();
    }
}