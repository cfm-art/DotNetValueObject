using System;
using System.Collections;
using System.Linq;
using System.Security.Cryptography;

namespace CfmArt.ValueObject.IdProvider
{
    /// <summary>ID用バイト配列</summary>
    public abstract class ByteSequence
    {
        /// <summary>内部データ</summary>
        public byte[] Sequence { get; protected set; }

        /// <summary>データ長</summary>
        protected abstract int Size { get; }

        /// <summary>乱数でバイト列を生成する</summary>
        protected static byte[] Generate(int size)
        {
            using var random = new RNGCryptoServiceProvider();
            var seq = new byte[size];
            random.GetBytes(seq);
            return seq;
        }

        /// <summary>等価判定補助</summary>
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

        /// <summary>等価判定</summary>
        public override bool Equals(object other)
            => other is ByteSequence o
                ? o.Size == Size && StructuralComparisons.StructuralEqualityComparer.Equals(Sequence, o.Sequence)
                : false;
        
        /// <summary>文字列表現の取得</summary>
        public override string ToString()
            => BitConverter.ToString(Sequence).ToLower();

        /// <summary>文字列からバイト列を生成</summary>
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

    /// <summary>64バイト</summary>
    public class ByteSequence64
        : ByteSequence
    {
        private ByteSequence64() {}
        /// <summary>データ長</summary>
        protected override int Size { get => 64; }

        /// <summary>乱数生成</summary>
        public static ByteSequence64 Generate() => new ByteSequence64() { Sequence = Generate(64) };
        /// <summary>0埋め</summary>
        public static ByteSequence64 Empty() => new ByteSequence64() { Sequence = new byte[64] };
        /// <summary>文字列から生成</summary>
        public static ByteSequence64 From(string value) => new ByteSequence64().FromString(value) as ByteSequence64;
    }

    /// <summary>128バイト</summary>
    public class ByteSequence128
        : ByteSequence
    {
        private ByteSequence128() {}
        /// <summary>データ長</summary>
        protected override int Size { get => 128; }

        /// <summary>乱数生成</summary>
        public static ByteSequence128 Generate() => new ByteSequence128() { Sequence = Generate(128) };
        /// <summary>0埋め</summary>
        public static ByteSequence128 Empty() => new ByteSequence128() { Sequence = new byte[128] };
        /// <summary>文字列から生成</summary>
        public static ByteSequence128 From(string value) => new ByteSequence128().FromString(value) as ByteSequence128;
    }

    /// <summary>256バイト</summary>
    public class ByteSequence256
        : ByteSequence
    {
        private ByteSequence256() {}
        /// <summary>データ長</summary>
        protected override int Size { get => 256; }

        /// <summary>乱数生成</summary>
        public static ByteSequence256 Generate() => new ByteSequence256() { Sequence = Generate(256) };
        /// <summary>0埋め</summary>
        public static ByteSequence256 Empty() => new ByteSequence256() { Sequence = new byte[256] };
        /// <summary>文字列から生成</summary>
        public static ByteSequence256 From(string value) => new ByteSequence256().FromString(value) as ByteSequence256;
    }

    /// <summary>512バイト</summary>
    public class ByteSequence512
        : ByteSequence
    {
        private ByteSequence512() {}
        /// <summary>データ長</summary>
        protected override int Size { get => 512; }

        /// <summary>乱数生成</summary>
        public static ByteSequence512 Generate() => new ByteSequence512() { Sequence = Generate(512) };
        /// <summary>0埋め</summary>
        public static ByteSequence512 Empty() => new ByteSequence512() { Sequence = new byte[512] };
        /// <summary>文字列から生成</summary>
        public static ByteSequence512 From(string value) => new ByteSequence512().FromString(value) as ByteSequence512;
    }

    /// <summary>1024バイト</summary>
    public class ByteSequence1024
        : ByteSequence
    {
        private ByteSequence1024() {}
        /// <summary>データ長</summary>
        protected override int Size { get => 1024; }

        /// <summary>乱数生成</summary>
        public static ByteSequence1024 Generate() => new ByteSequence1024() { Sequence = Generate(1024) };
        /// <summary>0埋め</summary>
        public static ByteSequence1024 Empty() => new ByteSequence1024() { Sequence = new byte[1024] };
        /// <summary>文字列から生成</summary>
        public static ByteSequence1024 From(string value) => new ByteSequence1024().FromString(value) as ByteSequence1024;
    }
}
