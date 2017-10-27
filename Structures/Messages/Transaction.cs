using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Structures.Messages
{
    public class TransactionMessage : Message
    {
        public static readonly char[] command = new char[12] { 't', 'x', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0' };

        public int version;
        public bool flag; //signals witness data
        public int txin_count;
        public List<TxIn> tx_ins = new List<TxIn>();
        public int txout_count;
        public List<TxOut> tx_outs = new List<TxOut>();
        public List<TxWitnesses> tx_witnesses = new List<TxWitnesses>();
        public UInt32 locktime;

        public TransactionMessage(byte[] bits, ref int ptr)
        {
            version = ReadInt32(bits, ref ptr);
            if(bits[ptr] == 0) //block cannot have zero transactions, so this is the witness flag
            {
                ptr += 2;
                flag = true;
            }
            txin_count = (int)ReadVarInt(bits, ref ptr);
            for(int i = 0; i < txin_count; i++)
            {
                TxIn newTx = new TxIn(bits, ref ptr);
                tx_ins.Add(newTx);
            }
            txout_count = (int)ReadVarInt(bits, ref ptr);
            for (int i = 0; i < txout_count; i++)
            {
                TxOut newTx = new TxOut(bits, ref ptr);
                tx_outs.Add(newTx);
            }
            if(flag)
            {
                //handle witness here
            }
            locktime = ReadUInt32(bits, ref ptr);
        }
    }
    
    public class TxIn : Message
    {
        public OutPoint prev_out;
        public int script_length;
        public byte[] sig_script;
        public UInt64 sequence;

        public TxIn(byte[] bits, ref int ptr)
        {
            prev_out = new OutPoint(bits, ref ptr);
            script_length = (int)ReadVarInt(bits, ref ptr);
            sig_script = new byte[script_length];
            for (int i = 0; i < script_length; i++)
                sig_script[i] = bits[ptr++];
            sequence = ReadUInt32(bits, ref ptr);
        }
    }

    public class OutPoint : Message
    {
        public byte[] hash;
        public UInt32 index;
        public OutPoint(byte[] bits, ref int ptr)
        {
            hash = new byte[32];
            for(int i = 0; i < 32; i++)
            {
                hash[i] = bits[ptr++];
            }
            index = ReadUInt32(bits, ref ptr);
        }
    }
    public class TxOut : Message
    {
        public ulong value;
        public ulong pk_script_length;
        public byte[] pk_script;
        public TxOut(byte[] bits, ref int ptr)
        {
            value = ReadUInt64(bits, ref ptr);
            pk_script_length = ReadVarInt(bits, ref ptr);
            pk_script = new byte[pk_script_length];
            for (ulong i = 0; i < pk_script_length; i++) pk_script[i] = bits[ptr++];
        }
    }
    public class Script : Message
    {
        
        public Script(byte[] bits, ref int ptr)
        {
            
        }
    }
}
