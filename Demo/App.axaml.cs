using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Demo.ViewModels;
using DryIoc;
using TuDog.Bootstrap;

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
        return new MainWindow();
    }

    protected override void Register(IContainer collection)
    {
        base.Register(collection);
    }
}