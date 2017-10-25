using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Structures.Messages
{
    public class BlockHeaderMessage : Message
    {
        public int version;
        public byte[] prev_block;
        public byte[] merkle_root;
        public UInt32 timestamp;
        public UInt32 difficulty; //difficulty
        public UInt32 nonce;
        public int txn_count; //should always be zero
        public BlockHeaderMessage(byte[] bits)
        {
            int ptr = 0;
            version = (int) ReadUInt32(bits, ref ptr);
            prev_block = bits.Skip(ptr).Take(32).ToArray();
            ptr += 32;
            merkle_root = bits.Skip(ptr).Take(32).ToArray();
            ptr += 32;
            timestamp = ReadUInt32(bits, ref ptr);
            difficulty = ReadUInt32(bits, ref ptr);
            nonce = ReadUInt32(bits, ref ptr);
            txn_count = (int)ReadVarInt(bits, ref ptr); //always gonna be zero
        }
    }
}
