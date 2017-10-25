using Structures.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Structures
{
    public class InventoryVector : INodeSerializable
    {
        private UInt32 type;
        private byte[] hash; //32 bits wide

        public InventoryVector(byte[] bits)
        {
            int ptr = 0;
            type = Message.ReadUInt32(bits, ref ptr);
            hash = bits.Skip(4).ToArray();
        }

        public InventoryVector(UInt32 type, byte[] hash)
        {
            this.type = type;
            this.hash = hash;
        }
        public byte[] Serialize()
        {
            throw new NotImplementedException();
        }
    }
}
