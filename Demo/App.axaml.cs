using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using TuDog.Bootstrap;

namespace Demo;

public partial class App : TuDogApplication
{
    public override void Initialize()
    {   AvaloniaXamlLoader.Load(this);
        base.Initialize();
     
    }

    public override object CreateShell()
    {
        return new MainWindow();
    }
}