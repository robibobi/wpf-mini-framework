using RS.WPF.Framework.Util;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Rs.WPF.Framework.TestApp
{

public partial class App : Application
{
    public App()
    {
        new BindingErrorListener(msg => Debugger.Break());
    }
}
}
