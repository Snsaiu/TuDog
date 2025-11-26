using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Metadata;
using Avalonia.Controls.Primitives;
using Avalonia.Controls.Templates;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using Avalonia.Markup.Xaml.Templates;
using DryIoc;
using FluentAvalonia.UI.Windowing;
using TuDog.Bootstrap;
using TuDog.Interfaces.MessageBarService;
using TuDog.Interfaces.MessageBarService.Impl;

namespace TuDog.UIs;

[TemplatePart("PART_InfoBox", typeof(InfoBox))]
[TemplatePart("PART_TitleBar", typeof(Border))]
public partial class TuDogWindow : AppWindow
{
    private InfoBox _infoBox;
    private Border _titleBar;

    private IMessageBarService _messageBarService;

    public static readonly StyledProperty<ControlTemplate?> TitleBarTemplateProperty =
        AvaloniaProperty.Register<TuDogWindow, ControlTemplate?>(nameof(TitleBarTemplate));

    public ControlTemplate? TitleBarTemplate
    {
        get => GetValue(TitleBarTemplateProperty);
        set => SetValue(TitleBarTemplateProperty, value);
    }

    public TuDogWindow()
    {
        InitializeComponent();
        _messageBarService = TuDogApplication.ServiceProvider.Resolve<IMessageBarService>();
    }

    protected override Type StyleKeyOverride { get; } = typeof(TuDogWindow);

    protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
    {
        base.OnApplyTemplate(e);
        _infoBox = e.NameScope.Get<InfoBox>("PART_InfoBox");
        _titleBar = e.NameScope.Get<Border>("PART_TitleBar");
        _titleBar.PointerMoved += WindowDragHandle_OnPointerMoved;
        _titleBar.PointerPressed += WindowDragHandle_OnPointerPressed;
        _titleBar.PointerReleased += WindowDragHandle_OnPointerReleased;
    }

    protected override void OnGotFocus(GotFocusEventArgs e)
    {
        base.OnGotFocus(e);
        if (_infoBox is { } box)
            ((MessageBarService)_messageBarService).RegisterInfoBox(box);
    }

    private bool _isWindowDragInEffect = false;
    private Point _cursorPositionAtWindowDragStart = new(0, 0);

    private void WindowDragHandle_OnPointerMoved(object? sender, PointerEventArgs e)
    {
        if (_isWindowDragInEffect)
        {
            var currentCursorPosition = e.GetPosition(this);
            var cursorPositionDelta = currentCursorPosition - _cursorPositionAtWindowDragStart;

            Position = this.PointToScreen(cursorPositionDelta);
        }
    }

    private void WindowDragHandle_OnPointerPressed(object? sender, PointerPressedEventArgs e)
    {
        _isWindowDragInEffect = true;
        _cursorPositionAtWindowDragStart = e.GetPosition(this);
    }

    private void WindowDragHandle_OnPointerReleased(object? sender, PointerReleasedEventArgs e)
    {
        _isWindowDragInEffect = false;
    }
}