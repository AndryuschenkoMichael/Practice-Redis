using System;
using StackExchange.Redis;

namespace TaskListSolution
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
            return _database.ListGetByIndex(key, -1);
        }

        public static bool Exist(string key)
        {
            return _database.KeyExists(key);
        }

        public static void Add(string key, string value)
        {
            _database.ListRightPush(key, value);
            while (_database.ListLength(key) > MaxCount)
            {
                _database.ListLeftPop(key);
            }
        }

        public static void Back(string key)
        {
            _database.ListRightPop(key);
            
            if (_database.ListLength(key) == 0)
            {
                _database.KeyDelete(key);
            }
        }
    }
}