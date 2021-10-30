using System;
using StackExchange.Redis;

namespace TaskStringSolution
{
    public static class RedisClient
    {
        private static ConnectionMultiplexer _redis;
        private static IDatabase _database;

        public static void Connect(string connectionString)
        {
            _redis = ConnectionMultiplexer.Connect(connectionString);
            _database = _redis.GetDatabase();
        }

        public static string GetSet(string key, string value)
        {
            return _database.StringGetSet(key, value);
        }

        public static bool Exist(string key)
        {
            return _database.KeyExists(key);
        }

        public static void Set(string key, string value)
        {
            _database.StringSet(key, value);
        }
    }
}