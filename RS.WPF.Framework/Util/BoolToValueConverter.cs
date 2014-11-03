using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace RS.WPF.Framework.Util
{
    public abstract class BoolToValueConverter<T> : IValueConverter
    {
        private T mTrueValue, mFalseValue;

        protected BoolToValueConverter(T trueValue, T falseValue)
        {
            mTrueValue = trueValue; mFalseValue = falseValue;
        }

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
                return mFalseValue;
            return (bool)value ? mTrueValue : mFalseValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
                return mFalseValue;
            return value.Equals(mTrueValue) ? true : false;
        }
    }
}
