using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using System;
using System.Linq;

namespace RedisDemo
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var sc = new ServiceCollection();

            //
            // 将 ConnectionMultiplexer 实例注册到DI (以下两种方式等价)
            //
            //sc.AddSingleton<ConnectionMultiplexer>(provider => ConnectionMultiplexer.Connect(ConfigurationOptions.Parse("localhost")));
            sc.AddSingleton<ConnectionMultiplexer>(provider => ConnectionMultiplexer.Connect("localhost"));

            var serviceProvider = sc.BuildServiceProvider();

            IDatabase database = serviceProvider.GetRequiredService<ConnectionMultiplexer>().GetDatabase();
        }

        /// <summary>
        /// Hash 操作
        /// </summary>
        /// <param name="database"></param>
        private static void HashOperations(IDatabase database)
        {
            string key = "employee";

            database.HashSet(key, "name", "robin");
            database.HashSet(key, "age", "28");
            database.KeyExpire(key, DateTime.Now.AddSeconds(20));
            database.HashSet(key, "address", "Nearby");

            Console.WriteLine(string.Join(",", database.HashGetAll(key).Select(x => x.Name + "=" + x.Value)));
        }

        /// <summary>
        /// Set 操作
        /// </summary>
        /// <param name="database"></param>
        private static void SetOperations(IDatabase database)
        {
        }
    }
}