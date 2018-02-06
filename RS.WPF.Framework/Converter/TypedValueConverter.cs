using RS.WPF.Framework.Util;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace RS.WPF.Framework.Converter
{
    public abstract class TypedValueConverter<TArg, TResult> : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            TArg typedArgument = SafeConvert.Cast<TArg>(value, fallback: default(TArg));
            return Convert(typedArgument, parameter, culture);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            TResult typedArgument = SafeConvert.Cast<TResult>(value, fallback: default(TResult));
            return ConvertBack(typedArgument, parameter, culture);
        }

        public abstract TResult Convert(TArg value, object parameter, CultureInfo ci);
  
       
        public virtual TArg ConvertBack(TResult result, object parameter, CultureInfo ci)
        {
            throw new NotImplementedException();
        }
    }
}
