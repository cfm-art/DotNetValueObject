using System;

namespace CfmArt.ValueObject
{
    /// <summary>整数</summary>
    public abstract class IntegralValue<T>
        : IEquatable<IntegralValue<T>>
        , IComparable<IntegralValue<T>>
        where T: IntegralValue<T>, new()
    {
        /// <summary>内部表現</summary>
        private long Value { get; set; }

        /// <summary>String型へ変換</summary>
        public override string ToString() => Value.ToString();

        /// <summary>Long型へ変換</summary>
        public long ToLong() => Value;

        /// <summary>long型から生成</summary>
        public static T FromLong(long value)
        {
            var text = new T();
            text.Value = value;
            return text;
        }

        /// <summary>Equatable</summary>
        public override int GetHashCode() => Value.GetHashCode();

        /// <summary>Equatable</summary>
        public override bool Equals(object obj)
            => obj is T v
                ? v.Value == Value
                : false;

        /// <summary>Comparable</summary>
        public int CompareTo(IntegralValue<T> other)
            => Value.CompareTo(other?.Value ?? 0);

        /// <summary>Equatable</summary>
        public bool Equals(IntegralValue<T> other)
            => other is null
                ? false
                : other.Value == Value;
    }
}
