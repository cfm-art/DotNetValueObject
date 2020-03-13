using System;
using CfmArt.Uuid;

namespace CfmArt.ValueObject.IdProvider
{
    /// <summary>
    /// UUIDの生成
    /// </summary>
    internal static class Uuid
    {
        /// <summary>UUIDv1の作成</summary>
        public static Guid NewUuidV1() => Uuid.NewUuidV1();

        /// <summary>UUIDv4相当を生成</summary>
        public static Guid NewUuidV4() => Uuid.NewUuidV4();

        /// <summary>UUIDv1相当を生成</summary>
        public static Guid NewUuid() => NewUuidV1();
    }
}
