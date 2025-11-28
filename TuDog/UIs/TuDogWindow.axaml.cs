using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Metadata;
using Avalonia.Controls.Primitives;
using Avalonia.Input;
using Avalonia.Markup.Xaml.Templates;
using DryIoc;
using FluentAvalonia.UI.Windowing;
using TuDog.Bootstrap;
using TuDog.Interfaces.MessageBarService;

namespace TuDog.UIs;

[TemplatePart("PART_InfoBox", typeof(InfoBox))]
[TemplatePart("PART_TitleBar", typeof(Border))]
public partial class TuDogWindow : AppWindow
{
    private InfoBox _infoBox;
    private Border _titleBar;

    private IMessageBarService _messageBarService;

    #region StyleProperties

    /// <summary>
    /// title bar template
    /// </summary>
    public static readonly StyledProperty<ControlTemplate?> TitleBarTemplateProperty =
        AvaloniaProperty.Register<TuDogWindow, ControlTemplate?>(nameof(TitleBarTemplate));

    public ControlTemplate? TitleBarTemplate
    {
        get => GetValue(TitleBarTemplateProperty);
        set => SetValue(TitleBarTemplateProperty, value);
    }

    /// <summary>
    /// close button visibility
    /// </summary>
    public static readonly StyledProperty<bool> ShowCloseButtonProperty = AvaloniaProperty.Register<TuDogWindow, bool>(
        nameof(ShowCloseButton), true);

    public bool ShowCloseButton
    {
        get => GetValue(ShowCloseButtonProperty);
        set => SetValue(ShowCloseButtonProperty, value);
    }

    /// <summary>
    ///  minimize button visibility
    /// </summary>
    public static readonly StyledProperty<bool> ShowMinButtonProperty = AvaloniaProperty.Register<TuDogWindow, bool>(
        nameof(ShowMinButton), true);

    public bool ShowMinButton
    {
        get => GetValue(ShowMinButtonProperty);
        set => SetValue(ShowMinButtonProperty, value);
    }

    /// <summary>
    /// maximize button visibility
    /// </summary>
    public static readonly StyledProperty<bool> ShowMaxButtonProperty = AvaloniaProperty.Register<TuDogWindow, bool>(
        nameof(ShowMaxButton), true);

    public bool ShowMaxButton
    {
        get => GetValue(ShowMaxButtonProperty);
        set => SetValue(ShowMaxButtonProperty, value);
    }

    /// <summary>
    /// title area template, the title template is to the center of the title bar
    /// </summary>
    public static readonly StyledProperty<ControlTemplate?> TitleTemplateProperty =
        AvaloniaProperty.Register<TuDogWindow, ControlTemplate?>(
            nameof(TitleTemplate));

    public ControlTemplate? TitleTemplate
    {
        get => GetValue(TitleTemplateProperty);
        set => SetValue(TitleTemplateProperty, value);
    }

    /// <summary>
    ///  left area template, the left area is to the left of the title bar,on the macOS platform,it is to the right of the system buttons that minimize,maximize and close
    /// </summary>
    public static readonly StyledProperty<ControlTemplate> TitleBarLeftAreaTemplateProperty =
        AvaloniaProperty.Register<TuDogWindow, ControlTemplate>(
            nameof(TitleBarLeftAreaTemplate));

    public ControlTemplate TitleBarLeftAreaTemplate
    {
        get => GetValue(TitleBarLeftAreaTemplateProperty);
        set => SetValue(TitleBarLeftAreaTemplateProperty, value);
    }

    /// <summary>
    /// right area template that is to the right of the title bar,on the windows platform,it is to the left of the system buttons that minimize,maximize and close
    /// </summary>
    public static readonly StyledProperty<ControlTemplate> TitleBarRightAreaTemplateProperty =
        AvaloniaProperty.Register<TuDogWindow, ControlTemplate>(
            nameof(TitleBarRightAreaTemplate));

    public ControlTemplate TitleBarRightAreaTemplate
    {
        get => GetValue(TitleBarRightAreaTemplateProperty);
        set => SetValue(TitleBarRightAreaTemplateProperty, value);
    }

    #endregion

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
        TuDogApplication.InfoBox = _infoBox;
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