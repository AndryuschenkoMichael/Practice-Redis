using System;
using System.Collections.Generic;
using System.Linq;
using StackExchange.Redis;

namespace TaskSetSolution
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

        public static void Add(string key, string value)
        {
            database.SetAdd(key, value);
        }

        public static bool Exist(string key)
        {
            return database.KeyExists(key);
        }

        public static bool ExistProduct(string key, string productName)
        {
            return database.SetContains(key, productName);
        }

        public static string[] GetProducts(string key)
        {
            return database.SetMembers(key)
                .Select(x => x.ToString())
                .ToArray();
        }
    }
}