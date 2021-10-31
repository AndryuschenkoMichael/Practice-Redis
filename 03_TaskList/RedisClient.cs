using System;
using StackExchange.Redis;

namespace TaskList
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
    // разместите ваш код здесь
}

public static bool Exist(string key)
{
    // разместите ваш код здесь
}

public static void Add(string key, string value)
{
    // разместите ваш код здесь
}

public static string Back(string key)
{
    // разместите ваш код здесь
}
    }
}