using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RawNotification.ServerClient.ServerInterface.Liblary
{
    public static class CryptoGraphyRandom
    {
        static System.Security.Cryptography.RandomNumberGenerator Generator = System.Security.Cryptography.RandomNumberGenerator.Create();
        static byte[] Int32Buffer = new byte[4];
        static byte[] Int64Buffer = new byte[8];
        static byte[] Int64DoubleBuffer = new byte[8];
        public static int GetInt()
        {
            lock (Int32Buffer)
            {
                Generator.GetBytes(Int32Buffer);
                return BitConverter.ToInt32(Int32Buffer, 0);
            }
        }

        
        public static long GetLong()
        {
            lock(Int64Buffer)
            {
                Generator.GetBytes(Int64Buffer);
                return BitConverter.ToInt64(Int64Buffer,0);
            }
        }

        public static double GetDouble()
        {
            lock(Int64DoubleBuffer)
            {
                Generator.GetBytes(Int64DoubleBuffer);
                return BitConverter.ToDouble(Int64DoubleBuffer, 0);
            }
        }
    }
}
