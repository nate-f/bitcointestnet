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
    public class ShaHelperTests
    {
        [TestMethod()]
        public void SHA256Test()
        {
            var bytes = new byte[64];
            bytes[0] = 128;
            var a = ShaHelper.SHA256(bytes);
            Assert.IsNotNull(a);
            var s = (a.ToByteArray()).ToStr();
        }

        [TestMethod()]
        public void SHA256Test1()
        {
            var bytes = new byte[64];
            for (int i = 0; i < bytes.Length; i++)
                bytes[i] = (byte)(i % 64);
            ShaHelper.SHA256(bytes);
        }
    }
}