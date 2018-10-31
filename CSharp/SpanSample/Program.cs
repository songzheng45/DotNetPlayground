using System;
using System.Diagnostics;

namespace SpanSample
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            TryReadOnlySpan();
        }

        static void TryReadOnlySpan()
        {
            string str = "Hello, World!";
            string worldStr
                = str.Substring(startIndex: 7, length: 5);    // 分配内存

            ReadOnlySpan<char> worldSpan
                = str.AsSpan().Slice(start: 7, length: 5);

            Debug.Assert('W' == worldSpan[0]);

            // 编译不通过：
            // 不能给属性 ReadOnlySpan<char>.this[int] 赋值，因为它是只读变量
            //worldSpan[0] = 'a';     
        }

        static void TryStackalloc()
        {
            //Span<byte> bytes;
            //unsafe
            //{
            //    byte* tmp = stackalloc byte[length];
            //    bytes = new Span<byte>(tmp, length);
            //}

            //
            // 现在可以使用关键字 stackalloc 来分配内存,而不必使用 unsafe 关键字
            //
            Span<byte> bytes = stackalloc byte[3];

        }
    }
}