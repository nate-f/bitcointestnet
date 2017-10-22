using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Structures
{
    public class NetworkAddress : INodeSerializable
    {
        private UInt32 time; //version > 31402. NOT PRESENT IN VERSION MESSAGE
        private UInt64 services;
        [BigEndian]
        private byte[] IPv6Address;
        [BigEndian]
        private UInt16 port;

        public NetworkAddress(UInt32 time, UInt64 services, byte[] address, UInt16 port)
        {
            this.time = time;
            this.services = services;
            this.port = port;
            if (address.Count() == 4)//ipv4 address needs to be converted to ipv6
            {
                IPv6Address = new byte[16];
                IPv6Address[10] = 255;
                IPv6Address[11] = 255;
                Array.ConstrainedCopy(address, 0, IPv6Address, 12, 4);
            }
            else
                this.IPv6Address = address;
        }


        public byte[] Serialize()
        {
            throw new NotImplementedException();
        }
    }
}
