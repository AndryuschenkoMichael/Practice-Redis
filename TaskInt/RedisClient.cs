using System;
using System.Collections.Generic;
using System.Linq;
using StackExchange.Redis;

namespace TaskInt
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
            throw new NotImplementedException();
        }

        public static void Remove(string key)
        {
            throw new NotImplementedException();
        }

        public static bool Exist(string key)
        {
            throw new NotImplementedException();
        }

        public static void Get(string key)
        {
            throw new NotImplementedException();
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