using System;

namespace CfmArt.ValueObject
{
    /// <summary>文字列</summary>
    public abstract class Text<T>
        : IEquatable<Text<T>>
        , IComparable<Text<T>>
        where T: Text<T>, new()
    {
        /// <summary>内部表現</summary>
        private string Value { get; set; }

        /// <summary>String型へ変換</summary>
        public override string ToString() => Value;

        /// <summary>String型から生成</summary>
        public static T FromString(string value)
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
        public int CompareTo(Text<T> other)
            => Value.CompareTo(other?.Value ?? "");

        /// <summary>Equatable</summary>
        public bool Equals(Text<T> other)
            => other is null
                ? false
                : other.Value == Value;
    }
}
