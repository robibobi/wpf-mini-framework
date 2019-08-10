using System;
using System.Runtime.ExceptionServices;
using System.Windows;

namespace RS.WPF.Framework.Extensions
{
    public static class ExceptionExtensions
    {
        public static void ThrowOnDispatcher(this Exception exc)
        {
            Application.Current.Dispatcher.BeginInvoke(new Action(() => {
                ExceptionDispatchInfo.Capture(exc).Throw();
            }));
        }
    }
}
