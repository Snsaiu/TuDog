using System.Globalization;

namespace TuDog.Extensions.Converters;

internal sealed class DialogHeightConverter : ValueConvertBase
{
    public override object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return (double)value + 168;
    }
}