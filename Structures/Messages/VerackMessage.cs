using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Structures.Messages
{
    public class VerackMessage : Message
    {
        public static readonly char[] command = new char[12] { 'v', 'e', 'r', 'a', 'c', 'k', '\0', '\0', '\0', '\0', '\0', '\0' };
        public VerackMessage(byte[] b)
        {
            if (b.Length > 0) throw new Exception();
        }
    }
}
