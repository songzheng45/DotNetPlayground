
using System;
using System.Text;
using System.IO;

namespace ConsoleApp
{
    // 将 UTF-8 Bom 转换为 UTF-8 的几种方法
    class Program
    {
        static string fileName = "TextUTF8WithBom.txt";

        static void Main(string[] args)
        {
            byte[] buffer = File.ReadAllBytes(fileName);
            PrintIfHasBom(buffer);
            PrintBytesLength(buffer);

            Print();

            byte[] newBuffer;
            Print("方法1:");
            newBuffer = Method1(buffer);
            PrintIfHasBom(newBuffer);
            PrintBytesLength(newBuffer);

            Print();

            Print("方法2:");
            newBuffer = Method2(buffer);
            PrintIfHasBom(newBuffer);
            PrintBytesLength(newBuffer);

            Print();

            Print("方法3:");
            newBuffer = Method3(buffer);
            PrintIfHasBom(newBuffer);
            PrintBytesLength(newBuffer);
        }

        static byte[] Method3(byte[] buffer)
        {
            if (buffer == null) return null;

            string str;

            if (buffer.Length <= 3) return buffer;

            byte[] bomBuffer = new byte[] { 0xef, 0xbb, 0xbf };

            if (buffer[0] == bomBuffer[0]
                && buffer[1] == bomBuffer[1]
                && buffer[2] == bomBuffer[2])
            {
                str = new UTF8Encoding(false).GetString(buffer, 3, buffer.Length - 3);
            }
            else
            {
                str = Encoding.UTF8.GetString(buffer);
            }

            byte[] newBuffer = Encoding.UTF8.GetBytes(str);
            return newBuffer;
        }

        private static byte[] Method2(byte[] buffer)
        {
            string str = Encoding.UTF8.GetString(buffer)?.Trim('\uFEFF', '\u200B');

            byte[] newBuffer = Encoding.UTF8.GetBytes(str);
            return newBuffer;
        }

        static byte[] Method1(byte[] buffer)
        {
            byte[] withBom = { 0xef, 0xbb, 0xbf, 0x41 };

            string viaStreamReader;
            using (StreamReader reader = new StreamReader
                   (new MemoryStream(buffer), Encoding.UTF8))
            {
                viaStreamReader = reader.ReadToEnd();
            }

            byte[] newBuffer = Encoding.UTF8.GetBytes(viaStreamReader);
            return newBuffer;
        }

        static bool ContainBom(byte[] buffer)
        {
            if (buffer == null || buffer.Length <= 3) return false;

            byte[] bomBuffer = new byte[] { 0xef, 0xbb, 0xbf };

            if (buffer[0] == bomBuffer[0]
                && buffer[1] == bomBuffer[1]
                && buffer[2] == bomBuffer[2])
            {
                return true;
            }
            return false;
        }

        static void PrintBytesLength(byte[] buffer)
        {
            Print("字节数组长度: " + buffer.Length);
        }

        static void PrintIfHasBom(byte[] buffer)
        {
            bool containBom = ContainBom(buffer);
            Print($"是否包含 BOM : {containBom}");
        }

        static void Print(string str = null)
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                System.Console.WriteLine();
            }
            else
            {
                System.Console.WriteLine(str);
            }
        }
    }
}
