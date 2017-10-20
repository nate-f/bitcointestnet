using System;
using NUnit.Framework;
using CryptoUtilities;

namespace Test
{
    [TestFixture]
    public class CryptoTests
    {
        [Test]
        public void HashToStringTest()
        {
            byte[] array = new byte[256];
            for (int i = 0; i < 256; i++)
            {
                array[i] = (byte) (i % 256);
            }
            Hash h = new Hash();
            h.data = array;
            Assert.IsNotNull(h.ToString());
        }
    }
}