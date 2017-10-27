using CryptoUtilities;
using Structures.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Structures
{
    public class MessageHeader : INodeSerializable
    {
        //testnet for now
        //LITTLE ENDIAN 
        public UInt32 magic = 0;
        public char[] command = new char[12]; // 12 bytes long
        public UInt32 length = 0;
        public UInt32 checksum = 0;
        public byte[] payload;

        public MessageHeader(char[] command, byte[] payload)
        {
            this.command = command;
            this.payload = payload;
            byte[] firstFour = new byte[4];
            Array.ConstrainedCopy(payload, 0, firstFour, 0, 4);
            var check = SHAHelper.DoubleSHA256(firstFour);
            checksum  = (uint)(check[0] << 24);
            checksum += (uint)(check[1] << 16);
            checksum += (uint)(check[2] << 8);
            checksum += (uint)(check[3] << 0);

        }
        public MessageHeader(byte[] bits, ref int ptr)
        {
            magic = Message.ReadUInt32(bits, ref ptr);
            for(int i = 0; i < 12; i++)
            {
                command[i] = (char)bits[ptr++];
            }

            length = Message.ReadUInt32(bits, ref ptr);
            
            var checksum = bits.Skip(ptr).Take(4).ToArray();

            var payload = bits.Skip(24).ToArray();
            var hash = SHAHelper.DoubleSHA256(payload);
            for (int i = 0; i < 4; i++)
            {
                if (hash[i] != checksum[i])
                {
                    throw new Exception();
                }
            }
            this.checksum = Message.ReadUInt32(bits, ref ptr);
            this.payload = payload;

        }
        public byte[] Serialize()
        {
            throw new NotImplementedException();
        }
    }
}
