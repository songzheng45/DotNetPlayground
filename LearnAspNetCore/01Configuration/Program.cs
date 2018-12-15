using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;

namespace _01Configuration
{
    class Program
    {
        /// <summary>
        /// 从 args 参数获取命令行传入的参数
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            var defaultArgs = new Dictionary<string, string>();
            defaultArgs.Add("name", "ROBIN");
            defaultArgs.Add("age", "18");

            var builder = new ConfigurationBuilder()
                .AddInMemoryCollection(defaultArgs)    // 当命令行中没有传入参数时，作为默认值
                .AddCommandLine(args);

            var config = builder.Build();   

            Console.WriteLine($"name:{config["name"]}");
            Console.WriteLine($"age:{config["age"]}");
        }
    }
}
