using System;
using System.Collections.Generic;
using System.Linq;
using StackExchange.Redis;

namespace TaskSetSolution
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

        public static void Add(string key, string value)
        {
            _database.SetAdd(key, value);
        }

        public static bool Exist(string key)
        {
            return _database.KeyExists(key);
        }

        public static bool ExistProduct(string key, string productName)
        {
            return _database.SetContains(key, productName);
        }

        public static List<string> GetProducts(string key)
        {
            return _database.SetMembers(key)
                .Select(x => x.ToString())
                .ToList();
        }
    }
}