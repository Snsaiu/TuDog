using Avalonia;
using Avalonia.Controls;
using TuDog.Bootstrap;

namespace TuDog.Extensions;

internal static class AvaloniaObjectExtension
{
    public static void AttachLoadedBehavior(this AvaloniaObject view, ITuDogViewModel vm)
    {
        if (view is not Control control)
        {
            return;
        }

        control.Loaded += (_, _) =>
        {
            if (vm.LoadedCommand.CanExecute(null))
                vm.LoadedCommand.Execute(null);
        };
    }

    public static void AttachUnLoadedBehavior(this AvaloniaObject view, ITuDogViewModel vm)
    {
        if (view is not Control control)
        {
            return;
        }

        control.Unloaded += (_, _) =>
        {
            if (vm.UnLoadedCommand.CanExecute(null))
                vm.UnLoadedCommand.Execute(null);
        };
    }
}
