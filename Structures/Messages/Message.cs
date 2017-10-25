using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Structures.Messages
{
    public class Message
    {
        public static Message CreateMessageFromByteArray(byte[] bits)
        {
            var header = new MessageHeader(bits.ToArray());
            Message m;
            if(new string(header.command) == new string(VersionMessage.command))
            {
                m = new VersionMessage(bits.Skip(24).ToArray());
            }
            else if (new string(header.command) == new string(AddrMessage.command))
            {
                m = new AddrMessage(bits.Skip(24).ToArray());
            }
            else if (new string (header.command) == new string (VerackMessage.command))
            {
                m = new VerackMessage(bits.Skip(24).ToArray());
            }
            else
            {
                throw new Exception();
            }
            return m;
        }
        public static UInt64 ReadVarInt(byte[] bits, ref int ptr)
        {
            UInt64 varint = 0;
            //could probably roll this into a single for loop
            if (bits[ptr] < 253)
            {
                ptr++;
                varint += bits[ptr];
            }
            if(bits[0] == 253)
            {

                varint = varint << 8;
                varint += bits[ptr];
                ptr++;
            }
            if (bits[0] == 254)
            {
                varint = varint << 8;
                varint += bits[ptr];
                ptr++;
            }
            if (bits[0] == 254)
            {
                varint = varint << 8;
                varint += bits[ptr];
                ptr++;
            }
            return varint;
        }
        public static UInt32 ReadUInt32(byte[] bits, ref int ptr)
        {
            UInt32 read = 0;
            for (int i = 3; i >= 0; i--) read += (UInt32)(bits[ptr + i] << 8 * i);
            ptr += 4;
            return read;
        }
    }
}
