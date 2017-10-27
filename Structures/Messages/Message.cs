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
            int ptr = 0;
            var header = new MessageHeader(bits, ref ptr);
            Message m;
            if(new string(header.command) == new string(VersionMessage.command))
            {
                m = new VersionMessage(bits, ref ptr);
            }
            else if (new string(header.command) == new string(AddrMessage.command))
            {
                m = new AddrMessage(bits, ref ptr);
            }
            else if (new string (header.command) == new string (VerackMessage.command))
            {
                m = new VerackMessage(bits, ref ptr);
            }
            else if (new string(header.command) == new string(TransactionMessage.command))
            {
                m = new TransactionMessage(bits, ref ptr);
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
            if (bits[ptr] < 253)
            {
                varint += bits[ptr];
                ptr++;
            }
            if (bits[0] == 255)
            {
                varint = varint << 8;
                varint += bits[ptr];
                ptr++;
            }
            if(bits[1] == 255)
            {

                varint = varint << 8;
                varint += bits[ptr];
                ptr++;
            }
            if (bits[2] == 255)
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
        public static int ReadInt32(byte[] bits, ref int ptr)
        {
            int read = 0;
            for (int i = 3; i >= 0; i--) read += (bits[ptr + i] << 8 * i);
            ptr += 4;
            return read;
        }
        public static UInt64 ReadUInt64(byte[] bits, ref int ptr)
        {
            UInt32 read = 0;
            for (int i = 7; i >= 0; i--) read += (UInt32)(bits[ptr + i] << 8 * i);
            ptr += 8;
            return read;
        }
        public static byte[] UInt64ToByteArray(UInt64 number)
        {
            byte[] bits = new byte[8];
            for(int i = 0; i < 8; i++)
            {
                bits[i] = (byte)(number >> 8 * i);
            }
            return bits;
        }
        public static byte[] UInt32ToByteArray(UInt32 number)
        {
            byte[] bits = new byte[4];
            for (int i = 0; i < 4; i++)
            {
                bits[i] = (byte)(number >> 8 * i);
            }
            return bits;
        }
        public static byte[] VarIntToByteArray(UInt64 number)
        {
            if(number < 253)
            {
                return new byte[] { (byte)number };
            }
            else if(number <= ushort.MaxValue)
            {
                return new byte[] { 0xFF, (byte)(number >> 8), (byte)(number % 256)};
            }
            else if(number <= UInt32.MaxValue)
            {
                return new byte[] { (byte)(number >> 24), (byte)(number >> 16), (byte)(number >> 8), (byte)number };
            }
            else
            {
                return new byte[] { (byte)(number >> 56), (byte)(number >> 48), (byte)(number >> 40), (byte)(number >> 32), (byte)(number >> 24), (byte)(number >> 16), (byte)(number >> 8), (byte)number };
            }
        }
    }
}
