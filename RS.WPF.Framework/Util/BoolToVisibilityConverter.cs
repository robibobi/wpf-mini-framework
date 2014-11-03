using System.Windows;

namespace RS.WPF.Framework.Util
{
    class BoolToVisibilityConverter : BoolToValueConverter<Visibility>
    {
       public BoolToVisibilityConverter() : base(Visibility.Visible, Visibility.Collapsed) {}
    }
}
