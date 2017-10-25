using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Structures.Messages
{
    public class GetDataMessage : Message
    {
        public int count;
        public List<InventoryVector> inventory = new List<InventoryVector>();
        public GetDataMessage(byte[] bits)
        {
            int ptr = 0;
            count = (int) ReadVarInt(bits, ref ptr);
            for(int i = 0; i < count; i++)
            {
                var inv = new InventoryVector(bits);
                inventory.Add(inv);
                ptr += 36;
            }

        }
    }
}
