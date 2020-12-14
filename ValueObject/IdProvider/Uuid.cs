using System;

namespace CfmArt.ValueObject.IdProvider
{
    /// <summary>
    /// UUIDの生成
    /// </summary>
    internal static class Uuid
    {
        /// <summary>UUIDv1の作成</summary>
        public static Guid NewUuidV1() => CfmArt.Uuid.Uuid.V1.Generate();

        /// <summary>UUIDv4相当を生成</summary>
        public static Guid NewUuidV4() => CfmArt.Uuid.Uuid.V4.Generate();

        /// <summary>UUIDv1相当を生成</summary>
        public static Guid NewUuid() => NewUuidV1();
    }
}
