using System;
using System.Collections.Generic;
using System.Linq;
using StackExchange.Redis;

namespace TaskIntSolution
{
    public static class RedisClient
    {
        private static ConnectionMultiplexer _redis;
        private static IDatabase _database;
        private static IServer _server;

        public static void Connect(string connectionString)
        {
            _redis = ConnectionMultiplexer.Connect($"{connectionString},allowAdmin=true");
            _database = _redis.GetDatabase();
            _server = _redis.GetServer(connectionString, 6379);
        }

        public static void Add(string key)
        {
            if (Exist(key))
            {
                _database.StringIncrement(key);
            }
            else
            {
                _database.StringSet(key, 1);
            }
        }

        public static void Remove(string key)
        {
            if (_database.StringDecrement(key) <= 0)
            {
                _database.KeyDelete(key);
            }
        }

        public static bool Exist(string key)
        {
            return _database.KeyExists(key);
        }

        public static long Get(string key)
        {
            return (long) _database.StringGet(key);
        }

        /// <summary>
        /// Get keys in Redis server with special beginning.
        /// </summary>
        /// <param name="keyBeginning"> Special beginning. </param>
        public static List<string> GetKeys(string keyBeginning = "")
        {
            return _server.Keys(pattern: $"{keyBeginning}*")
                .Select(x => x.ToString())
                .ToList();
        }
    }
}