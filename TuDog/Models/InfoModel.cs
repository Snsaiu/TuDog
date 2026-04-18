using CommunityToolkit.Mvvm.ComponentModel;
using Avalonia.Media;
using TuDog.Bootstrap;
using TuDog.Enums;

namespace TuDog.Models;

/// <summary>
/// 消息模型
/// </summary>
public partial class InfoModel : ModelBase
{
    [ObservableProperty] private string _message = string.Empty;

    [ObservableProperty] private string _title = string.Empty;

    [ObservableProperty] private Guid _key = Guid.CreateVersion7();

    [ObservableProperty] private bool _showClose = true;

    [ObservableProperty] private MessageState _type = MessageState.Warning;

    [ObservableProperty] private IBrush? _backgroundBrush;

    public Action<Guid> CloseAction { get; set; }

    private InfoModel()
    {
        var t = new System.Timers.Timer();
        t.Interval = 5000;
        t.AutoReset = false;
        t.Elapsed += (s, e) => { CloseAction?.Invoke(Key); };
        t.Start();
    }

    public static InfoModel Create(string message, string title, bool showClose, MessageState type)
    {
        return new InfoModel
        {
            Message = message,
            Title = title,
            ShowClose = showClose,
            Type = type,
            BackgroundBrush = CreateBackgroundBrush(type)
        };
    }

    private static IBrush CreateBackgroundBrush(MessageState type)
    {
        return type switch
        {
            MessageState.Success => new SolidColorBrush(Color.Parse("#FFDCFCE7")),
            MessageState.Error => new SolidColorBrush(Color.Parse("#FFFEE2E2")),
            MessageState.Warning => new SolidColorBrush(Color.Parse("#FFFEF3C7")),
            _ => new SolidColorBrush(Color.Parse("#FFE0F2FE"))
        };
    }

    //TODO warning..
}
