using FluentAvalonia.UI.Controls;
using TuDog.Bootstrap;
using TuDog.Extensions;
using TuDog.IocContainers;
using TuDog.Models;
using TuDog.UIs;
using TuDog.ViewLocators;

namespace TuDog.Interfaces.IDialogServers.Impl;

internal class DialogServer(ViewLocatorBase viewLocatorBase, ITuDogContainer container) : IDialogServer
{
    public Task ShowMessageDialogAsync(string message, string title = "提示",
        string buttonText = "确定")
    {
        var dialog = new ContentDialog
        {
            Title = title,
            Content = message,
            PrimaryButtonText = buttonText
        };
        return dialog.ShowAsync();
    }

    public async Task<bool> ShowConfirmDialogAsync(string message, string title = "提示", string confirmButtonText = "确定",
        string cancelButtonText = "取消")
    {
        var dialog = new ContentDialog
        {
            Title = title,
            Content = message,
            PrimaryButtonText = confirmButtonText,
            SecondaryButtonText = cancelButtonText
        };
        var result = await dialog.ShowAsync();
        return result == ContentDialogResult.Primary;
    }

    public async Task<DialogResultData<string>> ShowInputDialogAsync(string message, string title = "提示",
        string placeHolder = "请输入...",
        string confirmButtonText = "确定",
        string cancelButtonText = "取消", string? defaultValue = null, int? maxLength = null)
    {
        var dialog = new ContentDialog
        {
            Title = title,
            PrimaryButtonText = confirmButtonText,
            SecondaryButtonText = cancelButtonText
        };

        var vm = new InputTextViewModel
        {
            Text = message,
            Watermark = placeHolder
        };
        if (maxLength is not null)
            vm.MaxLength = maxLength.Value;

        var control = new InputTextView
        {
            DataContext = vm
        };
        dialog.Content = control;
        var dialogResult = await dialog.ShowAsync();
        if (dialogResult == ContentDialogResult.Primary)
            return new DialogResultData<string>(true, vm.Confirm()?.ToString() ?? string.Empty);
        return new DialogResultData<string>(false, string.Empty);
    }

    public async Task<DialogResultData<TResult>?> ShowDialogAsync<TViewModel, TParameter, TResult>(string title,
        string confirmButtonText = "确定",
        string cancelButtonText = "取消", TParameter? parameter = default)
        where TViewModel : DialogViewModelBaseAsync<TParameter, TResult>
    {
        var vm = container.Resolve<TViewModel>();
        if (vm == null)
            throw new InvalidOperationException(
                $"Type {typeof(TViewModel).FullName} is not registered in the container.");

        if (!typeof(TViewModel).BaseType.Name.StartsWith("DialogViewModelBaseAsync"))
        {
            var baseType = typeof(TViewModel).BaseType;
            var viewModelFullName = $"{baseType.Namespace}.{baseType.Name}".Split("`")[0];
            var viewFullName = viewModelFullName.Replace("ViewModel", "View");
            var viewType = Type.GetType($"{viewFullName},{baseType.Assembly.GetName().Name}");
            var view = viewLocatorBase.GetControlByType(viewType);
            vm.Parameter = parameter;
            view.DataContext = vm;
            view.AttachLoadedBehavior(vm);
            view.AttachUnLoadedBehavior(vm);

            var dialog = new DialogWindow
            {
                Title = title,
                PrimaryButtonText = confirmButtonText,
                SecondaryButtonText = cancelButtonText,
                Content = view,
                DialogViewModel = vm
            };
            var result = await dialog.ShowDialog<DialogResultData>(TuDogApplication.MainWindow);
            return result;
        }
        else
        {
            if (vm is null)
                throw new ArgumentNullException();

            vm.Parameter = parameter;

            var view = viewLocatorBase.Build(vm);
            if (view is null)
                throw new ArgumentNullException();

            view.DataContext = vm;
            view.AttachLoadedBehavior(vm);
            view.AttachUnLoadedBehavior(vm);

            var dialog = new DialogWindow
            {
                Title = title,
                PrimaryButtonText = confirmButtonText,
                SecondaryButtonText = cancelButtonText,
                Content = view,
                DialogViewModel = vm
            };

            var result = await dialog.ShowDialog<DialogResultData>(TuDogApplication.MainWindow);
            return result;
        }
    }

    public async Task<DialogResultData<object>?> ShowDialogAsync<TViewModel, TParameter>(string title,
        string confirmButtonText = "确定",
        string cancelButtonText = "取消", TParameter? parameter = default)
        where TViewModel : DialogViewModelBaseAsync
    {
        var vm = container.Resolve<TViewModel>();
        if (vm == null)
            throw new InvalidOperationException(
                $"Type {typeof(TViewModel).FullName} is not registered in the container.");

        if (!typeof(TViewModel).BaseType.Name.StartsWith("DialogViewModelBaseAsync"))
        {
            var baseType = typeof(TViewModel).BaseType;
            var viewModelFullName = $"{baseType.Namespace}.{baseType.Name}".Split("`")[0];
            var viewFullName = viewModelFullName.Replace("ViewModel", "View");
            var viewType = Type.GetType($"{viewFullName},{baseType.Assembly.GetName().Name}");
            var view = viewLocatorBase.GetControlByType(viewType);
            vm.Parameter = parameter;
            view.DataContext = vm;
            view.AttachLoadedBehavior(vm);
            view.AttachUnLoadedBehavior(vm);

            var dialog = new DialogWindow
            {
                Title = title,
                PrimaryButtonText = confirmButtonText,
                SecondaryButtonText = cancelButtonText,
                Content = view,
                DialogViewModel = vm
            };
            var result = await dialog.ShowDialog<DialogResultData>(TuDogApplication.MainWindow);
            return result;
        }
        else
        {
            if (vm is null)
                throw new ArgumentNullException();

            vm.Parameter = parameter;

            var view = viewLocatorBase.Build(vm);
            if (view is null)
                throw new ArgumentNullException();

            view.DataContext = vm;
            view.AttachLoadedBehavior(vm);
            view.AttachUnLoadedBehavior(vm);

            var dialog = new DialogWindow
            {
                Title = title,
                PrimaryButtonText = confirmButtonText,
                SecondaryButtonText = cancelButtonText,
                Content = view,
                DialogViewModel = vm
            };

            var result = await dialog.ShowDialog<DialogResultData>(TuDogApplication.MainWindow);
            return result;
        }
    }

    internal static DialogWindow? ProgressDialogWindow { get; set; }

    public ProgressDialogResult ShowProgressDialog(string title, string subHeader, string cancelButton = "")
    {
        var progressDialogWindow = new DialogWindow
        {
            Topmost = true, Content = new ProgressDialog(),
            DialogViewModel = new ProgressDialogViewModel(),
            WhenCancelCloseWindow = false
        };

        ProgressDialogWindow = progressDialogWindow;

        if (progressDialogWindow is not
            { DialogViewModel: ProgressDialogViewModel tdViewModel })
            throw new InvalidOperationException();

        progressDialogWindow.DataContext = tdViewModel;
        progressDialogWindow.Title = title;
        tdViewModel.SubTitle = subHeader;

        var token = CancellationToken.None;
        if (!string.IsNullOrEmpty(cancelButton))
        {
            progressDialogWindow.SecondaryButtonText = cancelButton;
            var source = new CancellationTokenSource();
            progressDialogWindow.CancellationTokenSource = source;

            token = source.Token;
        }
        else
        {
            progressDialogWindow.SecondaryButtonText = string.Empty;
        }

        progressDialogWindow.ShowDialog(TuDogApplication.MainWindow);

        ProgressProcess progressProcess = UpdateProgress;

        void UpdateProgress(string header, string subHeader, double? percentage)
        {
            progressDialogWindow.Title = header;
            tdViewModel.SubTitle = subHeader;

            if (!percentage.HasValue)
            {
                tdViewModel.IsIndeterminate = true;
            }
            else
            {
                tdViewModel.IsIndeterminate = false;
                tdViewModel.Value = percentage.Value;
            }
        }

        return new ProgressDialogResult(progressDialogWindow, progressProcess, token);
    }
}