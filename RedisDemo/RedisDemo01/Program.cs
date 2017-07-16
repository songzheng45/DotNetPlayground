using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace RedisDemo01
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            // Connect 或 ConnectAsync
            // ConnectionMultiplexer 实现了 IDisposable 接口，当不再需要时会自动释放，并且为了简洁，不必使用 using 包含。
            ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("192.168.1.11:6379");

            // 如果涉及 master/slave (主从)设置, 尽管将所有节点包含进来, Redis 会自动标志出 master.
            // ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("server1:6379,server2:6379");


            IDatabase db = redis.GetDatabase();
            db.StringSet("key1", DateTime.Now.ToString());
            string value = db.StringGet("key1");
            Console.WriteLine($"value={value}");


            var hs = new HashSet<string>();
            hs.Add(Guid.NewGuid().ToString());
            hs.Add(Guid.NewGuid().ToString());
            value = JsonConvert.SerializeObject(hs);
            db.StringSet("www", value);

            var jsonValue = db.StringGet("www");
            Console.WriteLine(jsonValue);
            var hsValue = JsonConvert.DeserializeObject<HashSet<string>>(jsonValue);

            foreach (var item in hsValue)
            {
                Console.WriteLine(item);
            }
        }
    }
}
