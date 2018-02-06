using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RS.WPF.Framework.Util
{
    public class SafeConvert
    {
        public static T Cast<T>(object value, T fallback)
        {
            if(value is T)
            {
                return (T)value;
            } else
            {
                return fallback;
            }
        }

        public static int ToInt32(string value, int fallback)
        {
            int res = fallback;
            Int32.TryParse(value, out res);
            return res;
        }
    }
}
