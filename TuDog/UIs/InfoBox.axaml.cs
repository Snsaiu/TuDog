using Avalonia.Controls;
using Avalonia.Controls.Metadata;
using Avalonia.Controls.Primitives;
using Avalonia.Threading;
using CommunityToolkit.Mvvm.Input;
using DryIoc;
using TuDog.Bootstrap;
using TuDog.Interfaces.MessageBarService;
using TuDog.Interfaces.MessageBarService.Impl;
using TuDog.Models;

namespace TuDog.UIs;

[TemplatePart(Name = "PART_ItemControl", Type = typeof(ItemsControl))]
public partial class InfoBox : TemplatedControl
{
    private const string PART_ItemControl = nameof(PART_ItemControl);

    private ItemsControl _itemControl;

    private IMessageBarService _messageBarService =
        TuDogApplication.ServiceProvider.Resolve<IMessageBarService>();

    public void AddNewMessage(InfoModel message)
    {
        message.CloseAction += RemoveItem;
        Dispatcher.UIThread.Post(() => { _itemControl.Items.Insert(0, message); }, DispatcherPriority.Background);
    }

    protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
    {
        base.OnApplyTemplate(e);
        _itemControl = e.NameScope.Get<ItemsControl>(PART_ItemControl);

        if (Parent is not { Parent : DialogWindow { Content: ProgressDialog } })
            ((MessageBarService)_messageBarService).RegisterInfoBox(this);
    }

    [RelayCommand]
    private Task Close(InfoModel parameter)
    {
        RemoveItem(parameter.Key);
        return Task.CompletedTask;
    }

    private void RemoveItem(Guid key)
    {
        var find = _itemControl.Items.FirstOrDefault(x => ((InfoModel)x).Key == key);
        if (find != null) _itemControl.Items.Remove(find);
    }
}