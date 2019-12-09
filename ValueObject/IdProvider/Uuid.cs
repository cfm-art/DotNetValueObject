using System;
using System.Runtime.InteropServices;

namespace CfmArt.ValueObject.IdProvider
{
    /// <summary>
    /// UUIDの生成
    /// </summary>
    internal static class Uuid
    {
        /// <summary>
        /// Windows APIでuuidv1を取得
        /// </summary>
        /// <param name="uuid"></param>
        /// <returns></returns>
        [DllImport("Rpcrt4.dll", SetLastError = true)]
        private extern static int UuidCreateSequential(out Guid uuid);

        /// <summary>
        /// UUIDv1の作成
        /// </summary>
        public static Guid NewUuidV1()
        {
            const int RPC_S_OK = 0;
            int hresult = UuidCreateSequential(out Guid result);

            if (hresult != RPC_S_OK)
            {
                throw new ApplicationException("UuidCreateSequential is failed: " + hresult);
            }

            return result;
        }

        /// <summary>UUIDv4相当を生成</summary>
        public static Guid NewUuidV4() => Guid.NewGuid();

        /// <summary>UUIDv1相当を生成</summary>
        public static Guid NewUuid() => NewUuidV1();
    }
}
