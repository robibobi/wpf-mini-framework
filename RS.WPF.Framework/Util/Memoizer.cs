using System;
using System.Collections.Generic;

namespace RS.WPF.Framework.Util
{
    class Memoizer<TArg, TResult>
    {
        private Func<TArg, TResult> _f;
        private Dictionary<TArg, TResult> _resultTable;

        public Memoizer(Func<TArg, TResult> f)
        {
            _f = f;
            _resultTable = new Dictionary<TArg, TResult>();
        }

        public TResult CallWith(TArg arg)
        {
            if (_resultTable.ContainsKey(arg))
                return _resultTable[arg];
            var value = _f(arg);
            _resultTable.Add(arg, value);
            return value;
        }

        public void Reset()
        {
            _resultTable.Clear();
        }
    }
}
