using System.Windows;

namespace RS.WPF.Framework.Converter
{
    class BoolToVisibilityConverter : BoolToValueConverter<Visibility>
    {
       public BoolToVisibilityConverter() : base(Visibility.Visible, Visibility.Collapsed) {}
    }
}
