using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RS.WPF.Framework.Composition.Types
{
    public abstract class Option
    {
        public static Some<T> Some<T>(T value)
        {
            return new Some<T>(value);
        }

        public static None None => None.Instance;

        public void Match<T>(Action none, Action<T> some)
        {
            if (this is Some<T> thisAsSome)
                some(thisAsSome.Vlaue);
            else
                none();
        }

        public bool IsSome => !(this is None);
    }

    public class Some<T> : Option
    {
        T _value;
        public Some(T value)
        {
            _value = value;
        }

        public T Vlaue => _value;
    }

    public class None : Option
    {
        private static None _instance = new None();
        public static None Instance => _instance; 
    }

    public static class OptionExtensions
    {

    }

}
