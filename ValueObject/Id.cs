using System;

namespace CfmArt.ValueObject
{
    internal class RandomHolder
    {
        public static Random Random { get; } = new Random();
    }

    /// <summary>ID</summary>
    public class Id<T>
        : IEquatable<Id<T>>
        , IComparable<Id<T>>
    {
        /// <summary>無し</summary>
        private Id() {}

        /// <summary></summary>
        public string Value { get; private set; }

        /// <summary>Guidで生成</summary>
        public static Id<T, Guid> New() => Id<T, Guid>.From(IdProvider.Uuid.NewUuid());

        /// <summary>乱数で生成</summary>
        public static Id<T, decimal> Random() => Id<T, decimal>.From((decimal) RandomHolder.Random.NextDouble());

        /// <summary>乱数で生成</summary>
        public static Id<T, IdProvider.ByteSequence256> Random256()
            => Id<T, IdProvider.ByteSequence256>.From(IdProvider.ByteSequence256.Generate());

        /// <summary>乱数で生成</summary>
        public static Id<T, IdProvider.ByteSequence512> Random512()
            => Id<T, IdProvider.ByteSequence512>.From(IdProvider.ByteSequence512.Generate());

        /// <summary>指定の型で生成</summary>
        public static Id<T> From<U>(U value)
            => new Id<T>() { Value = Converter<U>.I.From(value) };
        
        /// <summary>指定の型で空を生成</summary>
        public static Id<T> Empty<U>()
            => new Id<T>() { Value = Converter<U>.I.Empty() };

        /// <summary>指定の型へ変換</summary>
        public U To<U>() => Converter<U>.I.To(Value);

        /// <summary>元の型を追加する</summary>
        public Id<T, U> Bind<U>() => Id<T, U>.From(To<U>());

        /// <summary>等値</summary>
        public override bool Equals(object obj)
             => obj is Id<T> o
                ? o.Value == Value  // 型一致
                : obj is null
                    ? false         // 相手null
                    : obj.GetType().GetGenericTypeDefinition() == typeof(Id<,>) && obj.GetType().GetGenericArguments()[0] == typeof(T)
                        ? obj.Equals(this)  // 相手がId<T, ?>型
                        : false;    // 型不一致
        
        /// <summary>ハッシュ値</summary>
        public override int GetHashCode() => Value.GetHashCode();

        /// <summary>等値</summary>
        public bool Equals(Id<T> other) => other.Value == Value;
        /// <summary>比較</summary>
        public int CompareTo(Id<T> other) => Value.CompareTo(other.Value);
    }

    /// <summary>ID</summary>
    public class Id<T, U>
        : IEquatable<Id<T, U>>
        , IComparable<Id<T, U>>
    {
        /// <summary>無し</summary>
        private Id() {}

        /// <summary></summary>
        public string Value { get; private set; }

        /// <summary></summary>
        public static Id<T, U> From(U value)
            => new Id<T, U>() { Value = Converter<U>.I.From(value) };
        
        /// <summary></summary>
        public static Id<T, U> Empty()
            => new Id<T, U>() { Value = Converter<U>.I.Empty() };

        /// <summary></summary>
        public U To() => Converter<U>.I.To(Value);

        /// <summary>元の型を削る</summary>
        public Id<T> Curtail() => Id<T>.From<string>(Value);

        /// <summary>等値</summary>
        public override bool Equals(object obj)
             => obj is Id<T, U> o
                ? o.Value == Value
                : obj is Id<T> o2
                    ? o2.Value == Value
                    : false;
        
        public override int GetHashCode() => Value.GetHashCode();

        /// <summary>等値</summary>
        public bool Equals(Id<T, U> other) => other.Value == Value;
        /// <summary>比較</summary>
        public int CompareTo(Id<T, U> other) => Value.CompareTo(other.Value);
    }
}
