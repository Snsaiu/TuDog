using Avalonia.Markup.Xaml;
using AvaloniaApplication.ViewModels;
using AvaloniaApplication.Views;
using TuDog.Bootstrap;

namespace AvaloniaApplication;

public partial class App : TuDogApplication
{
    public override void Initialize()
    {
        base.Initialize();
        AvaloniaXamlLoader.Load(this);
    }

    public override object CreateShell()
    {
        return new HomeView { DataContext = new HomeViewModel() };
    }
}