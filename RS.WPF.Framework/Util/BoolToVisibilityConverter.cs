using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RS.WPF.Framework.Util
{
    class BoolToVisibilityConverter : BoolToValueConverter<Visibility>
    {
       public BoolToVisibilityConverter() : base(Visibility.Visible, Visibility.Collapsed) {}
    }
}
