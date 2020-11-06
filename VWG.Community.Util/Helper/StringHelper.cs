using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Runtime.InteropServices;

namespace VWG.Community.Util.Helper
{
    public class OpenCCHelper
    {
        public static class String
        {
            public static int Strlen(IntPtr ptr)
            {
                //Represent 0x8080808080808080UL in long
                long himagic = -0x7F7F7F7F7F7F7F80L;
                long lomagic = 0x0101010101010101L;

                for (int i = 0; ; i += 8)
                {
                    long longword = Marshal.ReadInt64(ptr, i);
                    if (((longword - lomagic) & ~longword & himagic) != 0)
                    {
                        for (int j = 0; j < 8; j++)
                        {
                            if (Marshal.ReadByte(ptr, i + j) == 0)
                            {
                                return i + j;
                            }
                        }
                    }
                }
            }
        }

        public static byte[] ReadToEnd(System.IO.Stream stream)
        {
            long originalPosition = 0;

            if (stream.CanSeek)
            {
                originalPosition = stream.Position;
                stream.Position = 0;
            }

            try
            {
                byte[] readBuffer = new byte[4096];

                int totalBytesRead = 0;
                int bytesRead;

                while ((bytesRead = stream.Read(readBuffer, totalBytesRead, readBuffer.Length - totalBytesRead)) > 0)
                {
                    totalBytesRead += bytesRead;

                    if (totalBytesRead == readBuffer.Length)
                    {
                        int nextByte = stream.ReadByte();
                        if (nextByte != -1)
                        {
                            byte[] temp = new byte[readBuffer.Length * 2];
                            Buffer.BlockCopy(readBuffer, 0, temp, 0, readBuffer.Length);
                            Buffer.SetByte(temp, totalBytesRead, (byte)nextByte);
                            readBuffer = temp;
                            totalBytesRead++;
                        }
                    }
                }

                byte[] buffer = readBuffer;
                if (readBuffer.Length != totalBytesRead)
                {
                    buffer = new byte[totalBytesRead];
                    Buffer.BlockCopy(readBuffer, 0, buffer, 0, totalBytesRead);
                }
                return buffer;
            }
            finally
            {
                if (stream.CanSeek)
                {
                    stream.Position = originalPosition;
                }
            }
        }
    }
}
