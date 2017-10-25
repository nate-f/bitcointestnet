using Microsoft.VisualStudio.TestTools.UnitTesting;
using Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Structures.Tests
{
    [TestClass()]
    public class MessageHeaderTests
    {
        [TestMethod()]
        public void MessageHeaderTest()
        {
            var bits = new byte[24] { 249, 190, 180, 217, 118, 101, 114, 115, 105, 111, 110,
                000, 000, 000, 000, 000, 000, 000, 000, 000, 93, 246, 224, 226 };

            var message = new MessageHeader(bits);
            var command = "version\0\0\0\0\0".ToCharArray();
            Assert.AreEqual(message.command.ToString(), command.ToString());
            Assert.AreEqual(message.length, (UInt32)0);
            Assert.AreEqual(message.magic, (UInt32)118034699);
        }
    }
}