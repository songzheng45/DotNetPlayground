using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace MyChatApp.Models
{
    public class UserMapping
    {
        ConnectionMultiplexer redis;
        IDatabase db;
        const string Key = "Chat:OnlineUsers";

        public UserMapping()
        {

            redis = ConnectionMultiplexer.Connect(ConfigurationManager.AppSettings["redis"]);

            db = redis.GetDatabase();
        }

        public void AddUser(string nickName)
        {
            HashSet<string> users = null;
            if (db.KeyExists(Key))
            {
                // Key存在, 则取出值, 反序列化
                users = JsonConvert.DeserializeObject<HashSet<string>>(db.StringGet(Key));

                if (users == null)
                {
                    users = new HashSet<string>();
                }
            }
            else
            {
                // 没有值, 初始化一个实例
                users = new HashSet<string>();
            }

            // 将用户添加进去
            users.Add(nickName);

            // 存到 Redis 中
            db.StringSet(Key, JsonConvert.SerializeObject(users));
        }

        public void RemoveUser(string nickName)
        {
            if (db.KeyExists(Key))
            {
                HashSet<string> users = JsonConvert.DeserializeObject<HashSet<string>>(db.StringGet(Key));
                if (users != null)
                {
                    users.Remove(nickName);
                }

                db.StringSet(Key, JsonConvert.SerializeObject(users));
            }
        }

        public IEnumerable<string> GetAllUsers()
        {
            HashSet<string> users = null;
            if (db.KeyExists(Key))
            {
                users = JsonConvert.DeserializeObject<HashSet<string>>(db.StringGet(Key));
                return users;
            }

            return Enumerable.Empty<string>();
        }
    }
}
