using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace TuDog.Bootstrap;

public partial class TuDogWindowViewModelBase : TuDogViewModelBase
{
    [ObservableProperty] private WindowState _windowState = WindowState.Normal;

    #region Commands

    /// <summary>
    /// close the window
    /// </summary>
    [RelayCommand]
    public async Task CloseWindowAsync()
    {
        if (!await CanCloseWindowAsync()) return;
        await OnClosedWindowAsync();
        TuDogApplication.MainWindow?.Close();
    }

    /// <summary>
    /// minimize window
    /// </summary>
    /// <returns></returns>
    [RelayCommand]
    public Task MinimizeWindowAsync()
    {
        WindowState = WindowState.Minimized;
        return Task.CompletedTask;
    }

    /// <summary>
    ///  maximize or normal window size
    /// </summary>
    /// <returns></returns>
    [RelayCommand]
    public Task MaximizeWindowAsync()
    {
        if (WindowState == WindowState.Maximized)
            WindowState = WindowState.Normal;
        else
            WindowState = WindowState.Maximized;
        return Task.CompletedTask;
    }

    #endregion

    #region Overrides

    /// <summary>
    /// determine whether the window can be closed
    /// </summary>
    /// <returns>return true is means windows will be closed</returns>
    protected virtual Task<bool> CanCloseWindowAsync()
    {
        return Task.FromResult(true);
    }

    /// <summary>
    /// when <see cref="CanCloseWindowAsync"/> return true,this method will be called,
    /// <remarks>
    /// on this method,you can not forbid the window to close, if you want to forbid the window to close,you must to
    /// return false in <see cref="CanCloseWindowAsync"/>
    /// </remarks>
    /// </summary>
    /// <returns></returns>
    protected virtual Task OnClosedWindowAsync()
    {
        return Task.CompletedTask;
    }

    #endregion
}