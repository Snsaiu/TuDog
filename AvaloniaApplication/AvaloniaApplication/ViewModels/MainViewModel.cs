using CommunityToolkit.Mvvm.ComponentModel;
using TuDog.Bootstrap;

namespace AvaloniaApplication.ViewModels
{
    public partial class MainViewModel : TuDogViewModelBase
    {
        [ObservableProperty]
        private string _greeting = "Welcome to Avalonia!";
    }
}
