using System;
using CfmArt.ValueObject.IdProvider;

namespace CfmArt.ValueObject
{
    internal interface IConverter<T>
    {
        T To(string value);
        string From(T value);
        string Empty();
    }

    internal class Converter<T>
    {
        public static IConverter<T> I { get; internal set; }

        static Converter()
        {
            Converter.Generate();
            if (typeof(T) == typeof(Guid) ||
                typeof(T) == typeof(long) ||
                typeof(T) == typeof(ulong) ||
                typeof(T) == typeof(decimal) ||
                typeof(T) == typeof(string) ||
                typeof(T) == typeof(ByteSequence64) ||
                typeof(T) == typeof(ByteSequence128) ||
                typeof(T) == typeof(ByteSequence256) ||
                typeof(T) == typeof(ByteSequence512) ||
                typeof(T) == typeof(ByteSequence1024))
            {
                return;
            }

            // ダメな奴
            if (typeof(T) == typeof(int) ||
                typeof(T) == typeof(short) ||
                typeof(T) == typeof(uint) ||
                typeof(T) == typeof(ushort) ||
                typeof(T) == typeof(byte) ||
                typeof(T) == typeof(sbyte))
            {
                throw new System.ArgumentException("use Conveter<long> instead of Conveter<" + typeof(T).Name + ">");
            }
            if (typeof(T) == typeof(float) ||
                typeof(T) == typeof(double))
            {
                throw new System.ArgumentException("use Conveter<decimal> instead of Conveter<" + typeof(T).Name + ">");
            }
            throw new System.ArgumentException("unknown type");
        }

        private Converter() {}
    }

    internal class Converter
        : IConverter<Guid>
        , IConverter<long>
        , IConverter<ulong>
        , IConverter<string>
        , IConverter<decimal>
        , IConverter<ByteSequence64>
        , IConverter<ByteSequence128>
        , IConverter<ByteSequence256>
        , IConverter<ByteSequence512>
        , IConverter<ByteSequence1024>
    {
        private static Converter Self { get; }

        static Converter()
        {
            Self = new Converter();
        }

        private Converter() {}

        internal static void Generate()
        {
            Converter<Guid>.I = Self;
            Converter<long>.I = Self;
            Converter<ulong>.I = Self;
            Converter<decimal>.I = Self;
            Converter<ByteSequence64>.I = Self;
            Converter<ByteSequence128>.I = Self;
            Converter<ByteSequence256>.I = Self;
            Converter<ByteSequence512>.I = Self;
            Converter<ByteSequence1024>.I = Self;
        }

        // --
        string IConverter<Guid>.Empty() => From(Guid.Empty);
        Guid IConverter<Guid>.To(string value) => Guid.Parse(value);
        public string From(Guid value) => value.ToString().ToLower();

        // --
        string IConverter<long>.Empty() => From(0L);
        long IConverter<long>.To(string value) => long.Parse(value);
        public string From(long value) => value.ToString();

        // --
        string IConverter<ulong>.Empty() => From(0UL);
        ulong IConverter<ulong>.To(string value) => ulong.Parse(value);
        public string From(ulong value) => value.ToString();

        // --
        string IConverter<decimal>.Empty() => From(0M);
        decimal IConverter<decimal>.To(string value) => decimal.Parse(value);
        public string From(decimal value) => value.ToString("0.########");

        // --
        string IConverter<string>.Empty() => From(string.Empty);
        string IConverter<string>.To(string value) => value;
        public string From(string value) => value;

        // --
        string IConverter<ByteSequence64>.Empty() => From(ByteSequence64.Empty());
        ByteSequence64 IConverter<ByteSequence64>.To(string value) => ByteSequence64.From(value);
        public string From(ByteSequence64 value) => value.ToString();

        // --
        string IConverter<ByteSequence128>.Empty() => From(ByteSequence128.Empty());
        ByteSequence128 IConverter<ByteSequence128>.To(string value) => ByteSequence128.From(value);
        public string From(ByteSequence128 value) => value.ToString();

        // --
        string IConverter<ByteSequence256>.Empty() => From(ByteSequence256.Empty());
        ByteSequence256 IConverter<ByteSequence256>.To(string value) => ByteSequence256.From(value);
        public string From(ByteSequence256 value) => value.ToString();

        // --
        string IConverter<ByteSequence512>.Empty() => From(ByteSequence512.Empty());
        ByteSequence512 IConverter<ByteSequence512>.To(string value) => ByteSequence512.From(value);
        public string From(ByteSequence512 value) => value.ToString();

        // --
        string IConverter<ByteSequence1024>.Empty() => From(ByteSequence1024.Empty());
        ByteSequence1024 IConverter<ByteSequence1024>.To(string value) => ByteSequence1024.From(value);
        public string From(ByteSequence1024 value) => value.ToString();
    }
}
