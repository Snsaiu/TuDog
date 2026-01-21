using TuDog.Bootstrap;
using TuDog.Enums;
using TuDog.Interfaces.IDialogServers.Impl;
using TuDog.Models;

namespace TuDog.Interfaces.IDialogServers;

public interface IDialogServer
{
    public Task ShowMessageDialogAsync(string message, string title = "提示", string buttonText = "OK");

    public Task<bool> ShowConfirmDialogAsync(string message, string title = "提示", string confirmButtonText = "OK", string cancelButtonText = "Cancel");

    public Task<DialogResultData<string>> ShowInputDialogAsync(string message, string title = "提示", string placeHolder = "Please input ...", string confirmButtonText = "OK", string cancelButtonText = "Cancel", string? defaultValue = null, int? maxLength = null);

    public Task<DialogResultData<TResult>?> ShowDialogAsync<TViewModel, TParameter, TResult>(string title, string confirmButtonText = "OK", string cancelButtonText = "Cancel", TParameter? parameter = default) where TViewModel : DialogViewModelBaseAsync<TParameter, TResult>;

    public Task<DialogResultData<object>?> ShowDialogAsync<TViewModel, TParameter>(string title, string confirmButtonText = "OK", string cancelButtonText = "Cancel", TParameter? parameter = default) where TViewModel : DialogViewModelBaseAsync;

    public ProgressDialogResult ShowProgressDialog(string title, string subHeader, string cancelButton = "");
}