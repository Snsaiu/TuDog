using System.ComponentModel.DataAnnotations;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Markup.Xaml;
using CommunityToolkit.Mvvm.Input;
using DryIoc;
using FluentAvalonia.UI.Windowing;
using TuDog.Bootstrap;
using TuDog.Interfaces;
using TuDog.Interfaces.MessageBarService;
using TuDog.Interfaces.MessageBarService.Impl;
using TuDog.Models;

namespace TuDog.UIs;

public partial class DialogWindow : Window
{
    public static readonly StyledProperty<string> PrimaryButtonTextProperty =
        AvaloniaProperty.Register<DialogWindow, string>(
            nameof(PrimaryButtonText));

    private Button _primaryButton;
    private Button _secondaryButton;
    private InfoBox _infoBox;

    private IMessageBarService _messageBarService =
        TuDogApplication.ServiceProvider.Resolve<IMessageBarService>();


    public IViewModelResultAsync DialogViewModel { get; set; }

    public string PrimaryButtonText
    {
        get => GetValue(PrimaryButtonTextProperty);
        set => SetValue(PrimaryButtonTextProperty, value);
    }

    public static readonly StyledProperty<string> SecondaryButtonTextProperty =
        AvaloniaProperty.Register<DialogWindow, string>(
            nameof(SecondaryButtonText));

    public string SecondaryButtonText
    {
        get => GetValue(SecondaryButtonTextProperty);
        set => SetValue(SecondaryButtonTextProperty, value);
    }

    [RelayCommand]
    private async Task Confirm()
    {
        if (!await DialogViewModel.CanConfirmAsync()) return;
        var result = await DialogViewModel.ConfirmAsync();
        DialogResultData data = new(true, result);
        Close(data);
    }

    [RelayCommand]
    private async Task Cancel()
    {
        var result = await DialogViewModel.CancelAsync();
        DialogResultData data = new(false, result);
        if (WhenCancelCloseWindow)
            Close(data);

        if (CancellationTokenSource is not null and var source)
            await source.CancelAsync();
    }

    internal CancellationTokenSource? CancellationTokenSource { get; set; }

    public bool WhenCancelCloseWindow { get; set; } = true;

    protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
    {
        base.OnApplyTemplate(e);
        _primaryButton = e.NameScope.Find<Button>("PrimaryButton") ?? throw new NullReferenceException();
        _secondaryButton = e.NameScope.Find<Button>("SecondaryButton") ?? throw new NullReferenceException();
        _infoBox = e.NameScope.Find<InfoBox>("PART_InfoBox") ?? throw new NullReferenceException();

        this.GetObservable(PrimaryButtonTextProperty)
            .Subscribe(x => _primaryButton.IsVisible = !string.IsNullOrEmpty(x));

        this.GetObservable(SecondaryButtonTextProperty)
            .Subscribe(x => _secondaryButton.IsVisible = !string.IsNullOrEmpty(x));

        if (DialogViewModel is not null)
            DialogViewModel.ErrorMessageAction += (message, title, state) =>
            {
                _infoBox.AddNewMessage(InfoModel.Create(message, title, true, state));
            };
    }

    public DialogWindow()
    {
        InitializeComponent();
    }
}