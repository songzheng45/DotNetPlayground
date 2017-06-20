using System;
using System.Text;
using System.Threading;
using System.IO;

namespace UbuntuDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            Console.WriteLine(".NET Core App is runing...");

            //string filename = "log.txt";
            //if (!File.Exists(filename))
            //{
            //    File.Create(filename);
            //}



            for (int index = 0; index < 300; index++)
            {
                Thread.Sleep(1000);

                string msg = $"当前时间：{DateTime.Now}";

                Console.WriteLine(msg);

                //using (StreamWriter writer = new StreamWriter(File.Open(filename, FileMode.Append)))
                //{
                //    writer.WriteLine(msg);
                //    writer.Flush();
                //}
            }
        }
    }
}
