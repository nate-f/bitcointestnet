using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Structures.Messages
{
    public class VersionMessage
    {
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
            MessageHeader header = 
        }
    }
}
