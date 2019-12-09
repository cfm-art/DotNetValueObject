using System;

namespace Cfm.Art.ValueObject
{
    /// <summary>ID</summary>
    public class Code<T>
        : IEquatable<Code<T>>
        , IComparable<Code<T>>
    {
        /// <summary>無し</summary>
        private Code() {}

        /// <summary></summary>
        public string Value { get; private set; }

        /// <summary></summary>
        public static Code<T> From<U>(U value)
            => new Code<T>() { Value = Converter<U>.I.From(value) };
        
        /// <summary></summary>
        public static Code<T> Empty<U>()
            => new Code<T>() { Value = Converter<U>.I.Empty() };

        /// <summary></summary>
        public U To<U>() => Converter<U>.I.To(Value);

        /// <summary>元の型を追加する</summary>
        public Code<T, U> Bind<U>() => Code<T, U>.From(To<U>());

        public override bool Equals(object obj)
             => obj is Code<T> o
                ? o.Value == Value  // 型一致
                : obj is null
                    ? false         // 相手null
                    : obj.GetType().GetGenericTypeDefinition() == typeof(Code<,>) && obj.GetType().GetGenericArguments()[0] == typeof(T)
                        ? obj.Equals(this)  // 相手がId<T, ?>型
                        : false;    // 型不一致
        
        public override int GetHashCode() => Value.GetHashCode();

        public bool Equals(Code<T> other) => other.Value == Value;
        public int CompareTo(Code<T> other) => Value.CompareTo(other.Value);
    }

    public class Code<T, U>
        : IEquatable<Code<T, U>>
        , IComparable<Code<T, U>>
    {
        /// <summary>無し</summary>
        private Code() {}

        /// <summary></summary>
        public string Value { get; private set; }

        /// <summary></summary>
        public static Code<T, U> From(U value)
            => new Code<T, U>() { Value = Converter<U>.I.From(value) };
        
        /// <summary></summary>
        public static Code<T, U> Empty()
            => new Code<T, U>() { Value = Converter<U>.I.Empty() };

        /// <summary></summary>
        public U To() => Converter<U>.I.To(Value);

        /// <summary>元の型を削る</summary>
        public Code<T> Curtail() => Code<T>.From<string>(Value);

        public override bool Equals(object obj)
             => obj is Code<T, U> o
                ? o.Value == Value
                : obj is Code<T> o2
                    ? o2.Value == Value
                    : false;
        
        public override int GetHashCode() => Value.GetHashCode();

        public bool Equals(Code<T, U> other) => other.Value == Value;
        public int CompareTo(Code<T, U> other) => Value.CompareTo(other.Value);
    }
}
