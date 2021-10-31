using System;
using System.Collections.Generic;
using System.Linq;
using StackExchange.Redis;

namespace TaskSet
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
            throw new NotImplementedException();
        }

        public static bool Exist(string key)
        {
            throw new NotImplementedException();
        }

        public static bool ExistProduct(string key, string productName)
        {
            throw new NotImplementedException();
        }

        public static List<string> GetProducts(string key)
        {
            throw new NotImplementedException();
        }
    }
}