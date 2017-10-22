using System;
using System.Reflection.Emit;
using System.Security.Policy;
using System.Security.Cryptography;
using System.Collections.Generic;
using System.Linq;

namespace CryptoUtilities
{
    public static class SHAHelper
    {
        public static byte[] ComputeSHA256(byte[] input)
        {
            return sha.ComputeHash(input);
        }
        public static byte[] DoubleSHA256(byte[] input)
        {
            return ComputeSHA256(ComputeSHA256(input));
        }
        public static byte[] ComputeMerkleTree(List<Transaction> transactions)
        {
            var hashes = new List<byte[]>();
            hashes.AddRange(transactions.Select(q => q.Data));
            do
            {
                var newHashes = new List<byte[]>();
                for (int i = 0; i < hashes.Count; i += 2)
                {
                    byte[] xa1 = DoubleSHA256(hashes[i]);
                    byte[] xa2 = i + 1 < transactions.Count ? DoubleSHA256(hashes[i + 1]) : xa1;
                    var concat = new byte[64];
                    xa1.CopyTo(concat, 0);
                    xa2.CopyTo(concat, 32);
                    var hash = DoubleSHA256(concat);
                    newHashes.Add(hash);
                }
                hashes = newHashes;
            } while (hashes.Count != 1);
            return hashes.First();
        }
        #region homebrew

        private static uint h0;
        private static uint h1;
        private static uint h2;
        private static uint h3;
        private static uint h4;
        private static uint h5;
        private static uint h6;
        private static uint h7;

        private static uint[] k = new uint[64];

        private static HashAlgorithm sha = new SHA256CryptoServiceProvider();
        static SHAHelper()
        {
            h0 = h0s.FromString().ToUint32();
            h1 = h1s.FromString().ToUint32();
            h2 = h2s.FromString().ToUint32();
            h3 = h3s.FromString().ToUint32();
            h4 = h4s.FromString().ToUint32();
            h5 = h5s.FromString().ToUint32();
            h6 = h6s.FromString().ToUint32();
            h7 = h7s.FromString().ToUint32();

            for (int i = 0; i < 64; i++)
            {
                k[i] = ks[i].FromString().ToUint32();
            }
        }
       

        public unsafe static uint[] SHA256(byte[] data)
        {
            var paddedData = PadData(data);
            ////Process the message in successive 512-bit chunks
            for (int reps = 0; reps < paddedData.Length / 64; reps++)
            {
                //copy chunk into first 16 words w[0..15] of the message schedule array:
                uint* w = stackalloc uint[64];
                for(int i = 0; i < 16; i++)
                {
                    
                    uint k = paddedData[i * 4] << 24;
                    k += paddedData[i * 4 + 1] << 16;
                    k += paddedData[i * 4 + 2] << 8;
                    k += paddedData[i * 4 + 3];
                    w[i] = (uint)k;
                }
                //Extend the first 16 words into the remaining 48 words w[16..63] of the message schedule array:
                for (int i = 16; i < 64; i++)
                {
                    uint s0 = (RightRotate(w[i - 15], 7)) ^ (RightRotate(w[i - 15], 18)) ^ (w[i - 15] >> 3);
                    uint s1 = (RightRotate(w[i - 2], 17)) ^ (RightRotate(w[i - 2], 19)) ^ (w[i - 2] >> 10);
                    w[i] = w[i - 16] + s0 + w[i - 7] + s1;
                }
                //Initialize working variables to current hash value:
                uint a = h0, b = h1, c = h2, d = h3, e = h4, f = h5, g = h6, h = h7;

                //Compression function main loop:
                for(int i = 0; i < 64; i++)
                {
                    var S1 = RightRotate(e, 6) ^ RightRotate(e, 11) ^ RightRotate(e, 25);
                    var ch = (e & f) ^ ((~e) & g);
                    var temp1 = h + S1 + ch + k[i] + w[i];
                    var S0 = (RightRotate(a, 2) ^ (RightRotate(a, 13) ^ (RightRotate(a, 22))));
                    var maj = (a & b) ^ (a & c) ^ (b & c);
                    var temp2 = S0 + maj;
                }
                h0 += a;
                h1 += b;
                h2 += c;
                h3 += d;
                h4 += e;
                h5 += f;
                h6 += g;
                h7 += h;
            }
            var digest = new uint[8];
            digest[0] = h0;
            digest[1] = h1;
            digest[2] = h2;
            digest[3] = h3;
            digest[4] = h4;
            digest[5] = h5;
            digest[6] = h6;
            digest[7] = h7;
            return digest;
        }
        private static uint RightRotate(uint original, int bits) => (original << bits) | (original >> (32 - bits));
        private static uint LeftRotate(uint original, int bits) => (original >> bits) | (original << (32 - bits));
        private static unsafe uint[] PadData(byte[] data)
        {
            
            var totalsize = data.Length + (64 - (data.Length % 64 + 1)); //in bytes
            uint[] d = new uint[totalsize / 4];
            //fixed (byte* dataPtr = d)
            //{
            //    for (int i = 0; i < data.Length; i++)
            //    {
            //        dataPtr[i] = data[i];
            //    }
            //    dataPtr[data.Length + 1] = 128;
            //    var intptr = (int*)(dataPtr + totalsize);
            //    *intptr = totalsize;
            //}
            return d;
        }

        private static string h0s = "6a09e667";
        private static string h1s = "bb67ae85";
        private static string h2s = "3c6ef372";
        private static string h3s = "a54ff53a";
        private static string h4s = "510e527f";
        private static string h5s = "9b05688c";
        private static string h6s = "1f83d9ab";
        private static string h7s = "5be0cd19";

        private static string[] ks = new string[64] {
            "428a2f98", "71374491", "b5c0fbcf", "e9b5dba5", "3956c25b", "59f111f1", "923f82a4", "ab1c5ed5",
            "d807aa98", "12835b01", "243185be", "550c7dc3", "72be5d74", "80deb1fe", "9bdc06a7", "c19bf174",
            "e49b69c1", "efbe4786", "0fc19dc6", "240ca1cc", "2de92c6f", "4a7484aa", "5cb0a9dc", "76f988da",
            "983e5152", "a831c66d", "b00327c8", "bf597fc7", "c6e00bf3", "d5a79147", "06ca6351", "14292967",
            "27b70a85", "2e1b2138", "4d2c6dfc", "53380d13", "650a7354", "766a0abb", "81c2c92e", "92722c85",
            "a2bfe8a1", "a81a664b", "c24b8b70", "c76c51a3", "d192e819", "d6990624", "f40e3585", "106aa070",
            "19a4c116", "1e376c08", "2748774c", "34b0bcb5", "391c0cb3", "4ed8aa4a", "5b9cca4f", "682e6ff3",
            "748f82ee", "78a5636f", "84c87814", "8cc70208", "90befffa", "a4506ceb", "bef9a3f7", "c67178f2" };

        #endregion
    }
}