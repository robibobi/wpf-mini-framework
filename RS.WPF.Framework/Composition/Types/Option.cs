using System;

namespace RS.WPF.Framework.Composition.Types
{

    public static class ObjectExtensions
    {
        /// <summary>
        /// Wraps the given value as Option<typeparamref name="T"/>.
        /// If the value is Nullm the underlying type is
        /// None<typeparamref name="T"/> otherwise Some<typeparamref name="T"/>
        /// </summary>
        public static Option<T> AsOption<T>(this T value)
        {
            if (value == null)
                return Option.None<T>();
            return Option.Some(value);
        }
    }

    public static class OptionExtensions
    {

        public static TResult Match<TOption, TResult>(this Option<TOption> option,
            Func<TOption, TResult> someF, TResult none)
        {
            if (option is Some<TOption> some)
                return someF(some.Value);
            else
                return none;
        }

        public static Option<B> Map<A, B>(this Option<A> optionA, Func<A, B> func)
        {
            if (optionA is Some<A> someA)
                return Option.From(func(someA.Value));
            else
                return Option.None<B>();
        }

        
    }

    public static class Option
    {
        public static Option<T> Some<T>(T value)
        {
            if(value is null)
                throw new ArgumentNullException(nameof(value));
            return new Some<T>(value);
        }

        public static Option<T> None<T>()
        {
            return new None<T>();
        }

        public static Option<T> From<T>(T value)
        {
            if (value != null)
                return new Some<T>(value);
            else
                return new None<T>();
        }
    }


    public interface Option<T>
    {
        bool IsSome { get; }
    }


    public struct Some<T> : Option<T>
    {
        public T Value { get; }

        public bool IsSome => true;

        public Some(T value)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));
            Value = value;
        }
    }

    public struct None<T> : Option<T>
    {
        public bool IsSome => false;
    }


}
