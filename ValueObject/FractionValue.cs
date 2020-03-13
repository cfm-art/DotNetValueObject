using System;

namespace CfmArt.ValueObject
{
    /// <summary>小数</summary>
    public abstract class FractionValue<T>
        : IEquatable<FractionValue<T>>
        , IComparable<FractionValue<T>>
        where T: FractionValue<T>, new()
    {
        /// <summary>内部表現</summary>
        private decimal Value { get; set; }

        /// <summary>String型へ変換</summary>
        public override string ToString() => Value.ToString();

        /// <summary>decimal型へ変換</summary>
        public decimal ToDecimal() => Value;

        /// <summary>decimal型から生成</summary>
        public static T FromDecimal(decimal value)
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
        public int CompareTo(FractionValue<T> other)
            => Value.CompareTo(other?.Value ?? 0);

        /// <summary>Equatable</summary>
        public bool Equals(FractionValue<T> other)
            => other is null
                ? false
                : other.Value == Value;
    }
}
