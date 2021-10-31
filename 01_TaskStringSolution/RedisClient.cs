using System;
using StackExchange.Redis;

namespace TaskStringSolution
{
    public static class RedisClient
    {
        private static ConnectionMultiplexer redis;
        private static IDatabase database;

        public static void Connect(string connectionString = "localhost")
        {
            redis = ConnectionMultiplexer.Connect(connectionString);
            database = redis.GetDatabase();
        }

        public static string GetSet(string key, string value)
        {
            return database.StringGetSet(key, value);
        }

        public static bool Exist(string key)
        {
            return database.KeyExists(key);
        }

        public static void Set(string key, string value)
        {
            database.StringSet(key, value);
        }
    }
}