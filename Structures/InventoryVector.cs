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
        private byte[] hash;
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
