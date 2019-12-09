using System;
using System.Collections;
using System.Linq;
using System.Security.Cryptography;

namespace CfmArt.ValueObject.IdProvider
{
    /// <summary>ID用バイト配列</summary>
    public abstract class ByteSequence
    {
        public byte[] Sequence { get; protected set; }

        protected abstract int Size { get; }

        protected static byte[] Generate(int size)
        {
            using var random = new RNGCryptoServiceProvider();
            var seq = new byte[size];
            random.GetBytes(seq);
            return seq;
        }

        public override int GetHashCode()
        {
            int size = Sequence.Length;
            int hash = size | ((~size & 0xffff) << 0x08);
            for (int i = 0; i < size; i += 4)
            {
                int v = (Sequence[i + 0] << 0x00) |
                        (Sequence[i + 1] << 0x04) |
                        (Sequence[i + 2] << 0x08) |
                        (Sequence[i + 3] << 0x10);
                hash ^= v;
            }
            return hash;
        }

        public override bool Equals(object other)
            => other is ByteSequence o
                ? o.Size == Size && StructuralComparisons.StructuralEqualityComparer.Equals(Sequence, o.Sequence)
                : false;
        

        public override string ToString()
            => BitConverter.ToString(Sequence).ToLower();

        protected ByteSequence FromString(string value)
        {
            var result = value.Split('-').Select(v => Convert.ToByte(v, 16)).ToArray();
            if (result.Length != Size)
            {
                throw new ArgumentException("The size is invalid. expected=" + Size + ", actual=" + result.Length);
            }
            Sequence = result;
            return this;
        }
    }

    public class ByteSequence64
        : ByteSequence
    {
        private ByteSequence64() {}
        protected override int Size { get => 64; }

        public static ByteSequence64 Generate() => new ByteSequence64() { Sequence = Generate(64) };
        public static ByteSequence64 Empty() => new ByteSequence64() { Sequence = new byte[64] };
        public static ByteSequence64 From(string value) => new ByteSequence64().FromString(value) as ByteSequence64;
    }

    public class ByteSequence128
        : ByteSequence
    {
        private ByteSequence128() {}
        protected override int Size { get => 128; }

        public static ByteSequence128 Generate() => new ByteSequence128() { Sequence = Generate(128) };
        public static ByteSequence128 Empty() => new ByteSequence128() { Sequence = new byte[128] };
        public static ByteSequence128 From(string value) => new ByteSequence128().FromString(value) as ByteSequence128;
    }

    public class ByteSequence256
        : ByteSequence
    {
        private ByteSequence256() {}
        protected override int Size { get => 256; }

        public static ByteSequence256 Generate() => new ByteSequence256() { Sequence = Generate(256) };
        public static ByteSequence256 Empty() => new ByteSequence256() { Sequence = new byte[256] };
        public static ByteSequence256 From(string value) => new ByteSequence256().FromString(value) as ByteSequence256;
    }

    public class ByteSequence512
        : ByteSequence
    {
        private ByteSequence512() {}
        protected override int Size { get => 512; }

        public static ByteSequence512 Generate() => new ByteSequence512() { Sequence = Generate(512) };
        public static ByteSequence512 Empty() => new ByteSequence512() { Sequence = new byte[512] };
        public static ByteSequence512 From(string value) => new ByteSequence512().FromString(value) as ByteSequence512;
    }

    public class ByteSequence1024
        : ByteSequence
    {
        private ByteSequence1024() {}
        protected override int Size { get => 1024; }

        public static ByteSequence1024 Generate() => new ByteSequence1024() { Sequence = Generate(1024) };
        public static ByteSequence1024 Empty() => new ByteSequence1024() { Sequence = new byte[1024] };
        public static ByteSequence1024 From(string value) => new ByteSequence1024().FromString(value) as ByteSequence1024;
    }
}
