using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Threading;

namespace TuDog.UIs;

public partial class InputTextView : UserControl
{
    public InputTextView()
    {
        InitializeComponent();
    }

    protected override void OnLoaded(RoutedEventArgs e)
    {
        base.OnLoaded(e);
        Dispatcher.UIThread.Post(() => textBox.Focus(NavigationMethod.Tab), DispatcherPriority.Loaded);
    }
}