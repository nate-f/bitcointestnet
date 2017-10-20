using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoUtilities
{
    public static class Extensions
    {
        public static string ToStr(this byte[] data)
        {
            char[] s = new char[data.Length * 2];
            for (int i = 0; i < data.Length; i++)
            {
                int flag = 0;
                char c;
                byte b = (byte)(data[i] >> 4);
                do
                {
                    switch (b)
                    {
                        case 10:
                            c = (char)49;
                            break;
                        case 11:
                            c = (char)50;
                            break;
                        case 12:
                            c = (char)51;
                            break;
                        case 13:
                            c = (char)52;
                            break;
                        case 14:
                            c = (char)53;
                            break;
                        case 15:
                            c = (char)54;
                            break;
                        default:
                            c = (char)b;
                            break;
                    }
                    s[i * 2 + flag] = (char)(c + 48);
                    flag++;
                    b = (byte)(data[i] % 16);
                } while (flag != 2);
            }
            return new string(s);
        }
        public static byte[] FromString(this string data)
        {
            byte[] b = new byte[data.Length / 2];
            for(int i = 0; i < data.Length; i+=2)
            {
                int c1 = data[i] - 48;
                int c2 = data[i + 1] - 48;
                if (c1 > 9) c1 -= 39;
                if (c2 > 9) c2 -= 39;
                c1 = c1 << 4;
                byte bit = (byte)(c1 + c2);
                b[i / 2] = bit;
            }
            return b;
        }
        public static uint ToUint32(this byte[] data)
        {
            uint f = 0;
            for(int i = 0; i < 4; i++)
            {
                f += (uint)(data[i] << (3 - i) * 8);
            }
            return f;
        }
        public static unsafe byte[] ToByteArray(this uint[] data)
        {
            byte[] array = new byte[data.Length * 4];
            for(int i = 0; i < data.Length; i++)
            {
                array[i * 4 + 0] = (byte)(data[i] >> 24);
                array[i * 4 + 1] = (byte)(data[i] >> 16);
                array[i * 4 + 2] = (byte)(data[i] >> 8);
                array[i * 4 + 3] = (byte)(data[i] >> 0);
            }
            return array;
        }
    }
}
