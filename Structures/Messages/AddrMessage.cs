using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Structures.Messages
{
    public class AddrMessage : Message
    {
        public static readonly char[] command = new char[12] { 'a', 'd', 'd', 'r', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0' };
        private readonly List<NetworkAddress> addresses = new List<NetworkAddress>();
        public AddrMessage(byte[] bits)
        {
            int ptr = 0;
            int numOfAddresses = bits[ptr]; //max 1000
            if(numOfAddresses > 254)
            {
                ptr++;
                numOfAddresses += bits[ptr] << 8;
            }
            ptr++;
            for(int i = 0; i < numOfAddresses; i++)
            {
                var ipaddr = new NetworkAddress(bits.Skip(ptr).Take(30).ToArray());
                addresses.Add(ipaddr);
            }
        }
    }
}
