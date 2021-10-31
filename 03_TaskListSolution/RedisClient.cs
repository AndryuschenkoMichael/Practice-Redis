using System;
using StackExchange.Redis;

namespace TaskListSolution
{
    public static class RedisClient
    {
        /// <summary>
        /// Maximum number of versions to store
        /// </summary>
        public const uint MaxCount = 5;

        private static ConnectionMultiplexer redis;
        private static IDatabase database;

        public static void Connect(string connectionString = "localhost")
        {
            redis = ConnectionMultiplexer.Connect(connectionString);
            database = redis.GetDatabase();
        }

        public static string Get(string key)
        {
            return database.ListGetByIndex(key, -1);
        }

        public static bool Exist(string key)
        {
            return database.KeyExists(key);
        }

        public static void Add(string key, string value)
        {
            database.ListRightPush(key, value);
            while (database.ListLength(key) > MaxCount)
            {
                database.ListLeftPop(key);
            }
        }

        public static string Back(string key)
        {
            string value = database.ListRightPop(key);
            
            if (database.ListLength(key) == 0)
            {
                database.KeyDelete(key);
            }
            return value;
        }
    }
}