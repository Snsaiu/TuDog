using FluentAvalonia.UI.Controls;
using TuDog.UIs;

namespace TuDog.Interfaces.IDialogServers.Impl;

public class ProgressDialogResult : IDisposable
{
    private readonly DialogWindow _taskDialog;

    public ProgressProcess Progress { get; }

    public CancellationToken CancellationToken { get; }

    public ProgressDialogResult(DialogWindow taskDialog, ProgressProcess progress, CancellationToken cancellationToken)
    {
        _taskDialog = taskDialog;
        Progress = progress;
        CancellationToken = cancellationToken;
    }

    public void Dispose()
    {
        if (_taskDialog is not null)
            _taskDialog.Close();
    }
}