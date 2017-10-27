using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Structures.Messages
{
    public class BlockMessage : Message
    {
        public int version;
        public byte[] prev_block;
        public byte[] merkle_root;
        public UInt32 timestamp;
        public UInt32 difficulty; //called 'bits' in docs
        public UInt32 nonce;
        public ulong txn_count;
        public List<TransactionMessage> txns = new List<TransactionMessage>();
        public BlockMessage(int version, byte[] prev_block, byte[] merkle_root, int timestamp, int difficulty, int nonce, int txn_count, List<TransactionMessage> txns)
        {
            this.version = version;
            this.prev_block = prev_block;
            this.merkle_root = merkle_root;
            this.timestamp = (UInt32)timestamp;
            this.difficulty = (UInt32)difficulty;
            this.nonce = (UInt32)nonce;
            this.txn_count = (ulong)txn_count;
            this.txns = txns;
        }

        public BlockMessage(byte[] bits, ref int ptr)
        {
            version = ReadInt32(bits, ref ptr);
            prev_block = new byte[32];
            for (int i = 0; i < 32; i++) prev_block[i] = bits[ptr + i];
            ptr += 32;
            merkle_root = new byte[32];
            for (int i = 0; i < 32; i++) merkle_root[i] = bits[ptr + i];
            ptr += 32;
            timestamp = ReadUInt32(bits, ref ptr);
            difficulty = ReadUInt32(bits, ref ptr);
            nonce = ReadUInt32(bits, ref ptr);
            txn_count = ReadVarInt(bits, ref ptr);
            for(ulong i = 0; i < txn_count; i++)
            {
                TransactionMessage m = new TransactionMessage(bits, ref ptr);
                txns.Add(m);
            }
        }
        public byte[] SerializeToByteArray()
        {
            return null;
        }
    }
}
