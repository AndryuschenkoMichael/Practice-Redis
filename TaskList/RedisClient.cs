using System;
using StackExchange.Redis;

namespace TaskList
{
    public static class RedisClient
    {
        private const uint MaxCount = 5;
        
        private static ConnectionMultiplexer _redis;
        private static IDatabase _database;

        public static void Connect(string connectionString)
        {
            _redis = ConnectionMultiplexer.Connect(connectionString);
            _database = _redis.GetDatabase();
        }

        public static string Get(string key)
        {
            throw new NotImplementedException();
        }

        public static bool Exist(string key)
        {
            throw new NotImplementedException();
        }

        public static void Add(string key, string value)
        {
            throw new NotImplementedException();
        }

        public static void Back(string key)
        {
            throw new NotImplementedException();

        }
    }
}