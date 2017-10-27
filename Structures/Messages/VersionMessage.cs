using CryptoUtilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Structures.Messages
{
    public class VersionMessage : Message
    {
        public static readonly char[] command = new char[12] { 'v', 'e', 'r', 's', 'i', 'o', 'n', '\0', '\0', '\0', '\0', '\0' };
        private int version;
        private UInt64 services;
        private Int64 timestamp;
        private NetworkAddress addr_recv;

        //fields below require version >= 106
        private NetworkAddress addr_from;
        private UInt64 nonce;
        private byte[] user_agent;
        private int start_height;
        //version > 70000
        bool relay;

        public VersionMessage(int version, UInt64 services, Int64 timestamp, NetworkAddress recv, NetworkAddress from, UInt64 nonce, byte[] user_agent, int start_height, bool relay)
        {
            this.version = version;
            this.services = services;
            this.timestamp = timestamp;
            this.addr_recv = recv;
            this.addr_from = from;
            this.nonce = nonce; //also called 'node id' in documentation
            this.user_agent = user_agent;
            this.start_height = start_height;
            this.relay = relay;
        }
        public VersionMessage(byte[] bits, ref int ptr)
        {
            version = (int)ReadUInt32(bits, ref ptr);
            services = ReadUInt64(bits, ref ptr);
            timestamp = (long)ReadUInt64(bits, ref ptr);
            addr_recv = new NetworkAddress(bits, ref ptr, true);
            addr_from = new NetworkAddress(bits, ref ptr, true);
            nonce += ReadUInt64(bits, ref ptr);
            var user_agent_length = (int) ReadVarInt(bits, ref ptr); //get the length of the user agent string
            user_agent = new byte[user_agent_length];
            for(int i = 0; i < user_agent_length; i++)user_agent[i] = bits[ptr++];
            start_height = (int)ReadUInt32(bits, ref ptr);
            //ignore relay for now
            relay = true;
        }
    }
}
