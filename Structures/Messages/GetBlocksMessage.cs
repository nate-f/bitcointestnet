using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Structures.Messages
{
    public class GetBlocksMessage : Message
    {
        public UInt32 version;
        public int hash_count;
        public List<byte[]> block_loc_hash = new List<byte[]>(); //32 bytes wide
        public byte[] hash_stop; //zero to get 500 blocks

        public GetBlocksMessage(byte[] bits, ref int ptr)
        {
            version = ReadUInt32(bits, ref ptr);
            hash_count = (int)ReadVarInt(bits, ref ptr);
            for (int i = 0; i < hash_count; i++)
            {
                var hash = bits.Skip(ptr).Take(32).ToArray();
                block_loc_hash.Add(hash);
                ptr += 32;
            }
            hash_stop = bits.Skip(ptr).Take(32).ToArray();
        }
        public GetBlocksMessage()
        {
            //this is gonna need work
        }
    }
}
