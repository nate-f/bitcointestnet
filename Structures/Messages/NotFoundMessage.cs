using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Structures.Messages
{
    public class NotFoundMessage : Message
    {
        public int count;
        public List<InventoryVector> inventory = new List<InventoryVector>();
        public NotFoundMessage(byte[] bits, ref int ptr)
        {
            count = (int)ReadVarInt(bits, ref ptr);
            for (int i = 0; i < count; i++)
            {
                var inv = new InventoryVector(bits, ref ptr);
                inventory.Add(inv);
                ptr += 36;
            }

        }
    }
}
