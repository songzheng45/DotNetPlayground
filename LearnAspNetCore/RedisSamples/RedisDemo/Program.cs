using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using System;
using System.Linq;
using System.Reflection;

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
            sc.AddSingleton(provider => ConnectionMultiplexer.Connect("localhost"));

            var serviceProvider = sc.BuildServiceProvider();

            IDatabase database = serviceProvider.GetRequiredService<ConnectionMultiplexer>().GetDatabase();

            HashOperations(database);
        }

        /// <summary>
        /// Hash 操作
        /// </summary>
        /// <param name="database"></param>
        private static void HashOperations(IDatabase database)
        {
            string key = "employee_001";

            Person p = new Person
            {
                Name = "robin",
                Age = 28,
                Address = "Nearby"
            };

//            database.HashSet(key, p.GetType().GetProperties(BindingFlags.Public));
            database.KeyExpire(key, DateTime.Now.AddSeconds(20));

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