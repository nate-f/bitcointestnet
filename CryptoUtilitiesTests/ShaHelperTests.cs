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
            var bytes = new byte[0];
          //  bytes[0] = 128;
            var a = SHAHelper.ComputeSHA256(bytes);
            Assert.IsNotNull(a);
            var s = (a.ToStr());
            Assert.AreEqual(s, "e3b0c44298fc1c149afbf4c8996fb92427ae41e4649b934ca495991b7852b855");
        }

        [TestMethod()]
        public void SHA256Test1()
        {
            var bytes = new byte[64];
            for (int i = 0; i < bytes.Length; i++)
                bytes[i] = (byte)(i % 64);
            SHAHelper.SHA256(bytes);
        }
        [TestMethod()]
        public void MerkleTest()
        {
            var transactions = new List<Transaction>();
            for(int i = 0; i < 1; i++)
            {
                transactions.Add(new Transaction() { Data = new byte[16] });
            }
            var result = SHAHelper.ComputeMerkleTree(transactions);

        }
    }
}