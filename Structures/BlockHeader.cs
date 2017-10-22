using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Structures
{
    public class BlockHeader : INodeSerializable
    {
        private int version;
        private byte[] prev_block; //32
        private byte[] merkle_root; // 32
        private UInt32 timestamp;
        private UInt32 bits; //difficulty target
        private UInt32 nonce;
        private int txn_count = 0; //always 0, variable width so 1 byte wide

        public BlockHeader(int version, byte[] prev_block, byte[] merkle_root, UInt32 timestamp, UInt32 bits, UInt32 nonce)
        {
            this.version = version;
            this.prev_block = prev_block;
            this.merkle_root = merkle_root;
            this.timestamp = timestamp;
            this.bits = bits;
            this.nonce = nonce;
        }
        public BlockHeader(byte[] bits)
        {

        }
        public byte[] Serialize()
        {
            throw new NotImplementedException();
        }

    }
}
