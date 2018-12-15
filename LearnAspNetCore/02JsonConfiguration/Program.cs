using System;
using Microsoft.Extensions.Configuration;

namespace _02JsonConfiguration
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("config.json");

            var config = builder.Build();

            Console.WriteLine($"姓名: {config["Name"]}");
            Console.WriteLine($"职位: {config["Position"]}");
            Console.WriteLine($"年龄: {config["Age"]}");
            Console.WriteLine($"工作: {config["Job:0"]},{config["Job:1"]},{config["Job:2"]}");
        }
    }
}
