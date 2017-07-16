//using System;
//using System.Collections.Generic;
//using System.Configuration;
//using System.Linq;
//using Newtonsoft.Json;
//using StackExchange.Redis;

//namespace MyChatApp.Models
//{
//    public class ConnectionMapping
//    {
//        IDatabase db;

//        public ConnectionMapping()
//        {
//            ConnectionMultiplexer redis = ConnectionMultiplexer.Connect(ConfigurationManager.AppSettings["redis"]);

//            db = redis.GetDatabase();
//        }

//        public string GetKey(string nickName)
//        {
//            return $"Chat:user:{nickName}";
//        }

//        public void Add(string nickName, string connectionId)
//        {
//            string key = GetKey(nickName);

//            HashSet<string> connections = JsonConvert.DeserializeObject<HashSet<string>>(db.StringGet(key));

//            if (connections == null)
//            {
//                connections = new HashSet<string>();
//            }

//            connections.Add(connectionId);

//            db.StringSet(key, JsonConvert.SerializeObject(connections));
//        }

//        public void Remove(string nickName, string connectionId)
//        {
//            string key = GetKey(nickName);

//            HashSet<string> connections = JsonConvert.DeserializeObject<HashSet<string>>(db.StringGet(key));

//            if (connections == null)
//            {
//                return;
//            }

//            connections.Remove(connectionId);

//            if (connections.Count == 0)
//            {
//                db.KeyDelete(key);
//            }
//            else
//            {
//                db.StringSet(key, JsonConvert.SerializeObject(connections));
//            }
//        }

//        public IEnumerable<string> GetConnection(string nickName)
//        {
//            string key = GetKey(nickName);

//            HashSet<string> connections = JsonConvert.DeserializeObject<HashSet<string>>(db.StringGet(key));

//            if (connections == null)
//            {
//                return Enumerable.Empty<string>();
//            }

//            return connections;
//        }
//    }
//}