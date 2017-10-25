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
        private byte[] user_agent = new byte[2] { 0, 0 }; //revisit this later, variable size but probably want a fun user-agent
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
            this.nonce = nonce;
            this.user_agent = user_agent;
            this.start_height = start_height;
            this.relay = relay;
        }
        public VersionMessage(byte[] bits)
        {
            int ptr = 0;
            //version
            for (int i = 3; i >= 0; i--) version += bits[ptr + i] << 8 * i;
            ptr += 4;
            //services
            for (int i = 7; i >= 0; i--) services += (UInt64)(bits[ptr + i] << 8 * i);
            ptr += 8;
            //timestamp
            for (int i = 7; i >= 0; i--) timestamp += (bits[ptr + i] << 8 * i);
            ptr += 8;
            //addr_recv
            addr_recv = new NetworkAddress(bits.Skip(ptr).Take(26).ToArray(), true);
            ptr += 26;
            //addr_from
            addr_from = new NetworkAddress(bits.Skip(ptr).Take(26).ToArray(), true);
            ptr += 26;
            //nonce
            for (int i = 7; i >= 0; i--) nonce += (UInt64)(bits[ptr + i] << 8 * i);
            //ignore user_agent for now
            ptr++;
            //start_height
            for (int i = 3; i >= 0; i--) start_height += (bits[ptr + i] << 8 * i);
            //ignore relay for now
            relay = true;
        }
    }
}
