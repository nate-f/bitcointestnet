using Microsoft.VisualStudio.TestTools.UnitTesting;
using CryptoUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoUtilities.Tests
{
    [TestClass()]
    public class ExtensionsTests
    {
        [TestMethod()]
        public void ByteArrayToStringTest()
        {
            byte[] array = new byte[256];
            for (int i = 0; i < 256; i++)
            {
                array[i] = (byte)(255);
            }
            string s = array.ToString();
            Assert.IsNotNull(s);
        }
        [TestMethod()]
        public void StringToByteArrayTest()
        {
            string s = "6a09e667";
            byte[] b = s.FromString();
            Assert.IsNotNull(b);
        }

        [TestMethod()]
        public void FromByteArrayTest()
        {
            string s = "000f0000";
            byte[] b = s.FromString();
            uint a = b.ToUint32();
            Assert.IsNotNull(a);
        }
    }
}