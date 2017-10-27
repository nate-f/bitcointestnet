using Microsoft.VisualStudio.TestTools.UnitTesting;
using Structures.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Structures.Messages.Tests
{
    [TestClass()]
    public class MessageTests
    {
        [TestMethod()]
        public void UInt64ToByteArrayTest()
        {
            UInt64 number = 1328971239;
            var bytes = Message.UInt64ToByteArray(number);
            int ptr = 0;
            var roundTrip = Message.ReadUInt64(bytes, ref ptr);
            Assert.AreEqual(number, roundTrip);
        }

        [TestMethod()]
        public void VarIntToByteArrayTest()
        {
            UInt64 number = 256;
            var bytes = Message.VarIntToByteArray(number);
            int ptr = 0;
            var roundTrip = Message.ReadVarInt(bytes, ref ptr);
            Assert.AreEqual(number, roundTrip);
        }
    }
}