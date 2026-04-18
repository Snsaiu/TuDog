using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using TuDog.Bootstrap;
using TuDog.Extensions;
using TuDog.IocContainers;
using TuDog.Interfaces;
using TuDog.Models;
using TuDog.UIs;
using TuDog.ViewLocators;

namespace TuDog.Interfaces.IDialogServers.Impl;

internal class DialogServer(ViewLocatorBase viewLocatorBase, ITuDogContainer container) : IDialogServer
{
    public async Task ShowMessageDialogAsync(string message, string title = "提示", string buttonText = "OK")
    {
        var dialog = CreateDialog(
            title,
            BuildMessageContent(message),
            new CallbackDialogViewModel(
                onConfirm: () => Task.FromResult<object?>(true),
                onCancel: () => Task.FromResult<object?>(false)),
            buttonText,
            string.Empty);

        await dialog.ShowDialog<DialogResultData>(TuDogApplication.MainWindow);
    }

    public async Task<bool> ShowConfirmDialogAsync(string message, string title = "提示", string confirmButtonText = "OK", string cancelButtonText = "Cancel")
    {
        var dialog = CreateDialog(
            title,
            BuildMessageContent(message),
            new CallbackDialogViewModel(
                onConfirm: () => Task.FromResult<object?>(true),
                onCancel: () => Task.FromResult<object?>(false)),
            confirmButtonText,
            cancelButtonText);

        var result = await dialog.ShowDialog<DialogResultData>(TuDogApplication.MainWindow);
        return result?.Ok == true;
    }

    public async Task<DialogResultData<string>> ShowInputDialogAsync(string message, string title = "提示", string placeHolder = "Please input ...", string confirmButtonText = "OK", string cancelButtonText = "Cancel", string? defaultValue = null, int? maxLength = null)
    {
        var vm = new InputTextViewModel
        {
            Text = defaultValue ?? string.Empty,
            Watermark = placeHolder
        };
        if (maxLength is not null)
        {
            vm.MaxLength = maxLength.Value;
        }

        var dialog = CreateDialog(
            title,
            new StackPanel
            {
                Spacing = 12,
                Children =
                {
                    new TextBlock
                    {
                        Text = message,
                        TextWrapping = TextWrapping.Wrap
                    },
                    new InputTextView
                    {
                        DataContext = vm
                    }
                }
            },
            new CallbackDialogViewModel(
                onConfirm: () => Task.FromResult<object?>(vm.Confirm()?.ToString() ?? string.Empty),
                onCancel: () => Task.FromResult<object?>(string.Empty)),
            confirmButtonText,
            cancelButtonText);

        var result = await dialog.ShowDialog<DialogResultData>(TuDogApplication.MainWindow);
        if (result?.Ok == true && result.Data is string value)
        {
            return new DialogResultData<string>(true, value);
        }

        return new DialogResultData<string>(false, string.Empty);
    }

    public async Task<DialogResultData<TResult>?> ShowDialogAsync<TViewModel, TParameter, TResult>(string title, string confirmButtonText = "OK", string cancelButtonText = "Cancel", TParameter? parameter = default) where TViewModel : DialogViewModelBaseAsync<TParameter, TResult>
    {
        var vm = container.Resolve<TViewModel>();
        if (vm == null)
            throw new InvalidOperationException($"Type {typeof(TViewModel).FullName} is not registered in the container.");

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

    public async Task<DialogResultData<object>?> ShowDialogAsync<TViewModel, TParameter>(string title, string confirmButtonText = "OK", string cancelButtonText = "Cancel", TParameter? parameter = default) where TViewModel : DialogViewModelBaseAsync
    {
        var vm = container.Resolve<TViewModel>();
        if (vm == null)
            throw new InvalidOperationException($"Type {typeof(TViewModel).FullName} is not registered in the container.");

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

        if (progressDialogWindow is not { DialogViewModel: ProgressDialogViewModel tdViewModel })
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

    private static DialogWindow CreateDialog(string title, Control content, IViewModelResultAsync dialogViewModel, string confirmButtonText, string cancelButtonText)
    {
        var dialog = new DialogWindow
        {
            Title = title,
            PrimaryButtonText = confirmButtonText,
            SecondaryButtonText = cancelButtonText,
            Content = content,
            DialogViewModel = dialogViewModel
        };

        dialog.DataContext = dialogViewModel;
        return dialog;
    }

    private static Control BuildMessageContent(string message)
    {
        return new TextBlock
        {
            Text = message,
            TextWrapping = TextWrapping.Wrap,
            Margin = new Thickness(24),
            MaxWidth = 420
        };
    }

    private sealed class CallbackDialogViewModel(Func<Task<object?>> onConfirm, Func<Task<object?>> onCancel) : DialogViewModelBaseAsync
    {
        public override Task<object?> ConfirmAsync()
        {
            return onConfirm();
        }

        public override Task<object?> CancelAsync()
        {
            return onCancel();
        }
    }
}
