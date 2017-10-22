using CryptoUtilities;
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
        private UInt32 magic = 118034699;
        private char[] command; // 12 bytes long
        private UInt32 length = 0;
        private UInt32 checksum = 0;
        private byte[] payload;

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
        public MessageHeader(byte[] bits) //still gonna have to worry about endianness
        {
            unsafe
            {
                fixed (byte* dataPtr = bits)
                {
                    magic = *((UInt32*)dataPtr);
                }
            }
        }
        public byte[] Serialize()
        {
            throw new NotImplementedException();
        }
    }
}
