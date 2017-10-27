using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Structures.Messages
{
    public class GetHeadersMessage : Message
    {
        public int version;
        public ulong hash_count;
        public List<byte[]> blk_lctr_hashes = new List<byte[]>();
        public byte[] hash_stop;

        public GetHeadersMessage(byte[] bits, ref int ptr)
        {
            version = (int)ReadUInt32(bits, ref ptr);
            hash_count = ReadVarInt(bits, ref ptr);
            for(ulong i = 0; i < hash_count; i++)
            {
                var blk_locator_hash = bits.Skip(ptr).Take(32).ToArray();
                blk_lctr_hashes.Add(blk_locator_hash);
                ptr += 32;
            }
            hash_stop = bits.Skip(ptr).ToArray();
        }
    }
}
