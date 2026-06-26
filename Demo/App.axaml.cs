using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Demo.ViewModels;
using DryIoc;
using TuDog.Bootstrap;
using TuDog.Extensions;

namespace Demo;

public partial class App : TuDogApplication
{
    public override void Initialize()
    {
        base.Initialize();
        AvaloniaXamlLoader.Load(this);
    }

    public override object CreateShell()
    {
        return new MainWindow { DataContext = new MainWindowViewModel() };
    }

    protected override void Register(IContainer collection)
    {
        base.Register(collection);
        collection.ConfigPreferenceFileName(() => "Demo.json");
    }
}