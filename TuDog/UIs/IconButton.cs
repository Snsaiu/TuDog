using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;

namespace TuDog.UIs;

public class IconButton : Button
{
    public static readonly StyledProperty<string> IconFontProperty = AvaloniaProperty.Register<IconButton, string>(
        nameof(IconFont));

    protected override Type StyleKeyOverride { get; } = typeof(IconButton);

    public string IconFont
    {
        get => GetValue(IconFontProperty);
        set => SetValue(IconFontProperty, value);
    }

    public static readonly StyledProperty<FontFamily> FontFamilyProperty =
        AvaloniaProperty.Register<IconButton, FontFamily>(
            nameof(FontFamily));

    public FontFamily FontFamily
    {
        get => GetValue(FontFamilyProperty);
        set => SetValue(FontFamilyProperty, value);
    }
}