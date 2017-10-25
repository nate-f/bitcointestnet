using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Structures.Messages
{
    public class InventoryMessage : Message
    {
        private int count = 0;
        public InventoryMessage(byte[] bits)
        {
            int ptr = 0;
            ulong length = ReadVarInt(bits, ref ptr);
            for(ulong i = 0; i < length; i++)
            {
                var inventoryVector = new InventoryVector(bits);
            }
        }
    }
}
