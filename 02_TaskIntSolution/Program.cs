using System;
using System.Collections.Generic;

namespace TaskIntSolution
{
    // Компания Склад.LIFE занимается грузоперевозками.
    // Напишите программу, которая отвечает за учет товаров на складе, т.е. сколько штук каждого товара хранится на складе.

    // Существует 4 типа запросов:
    // 1) add <product_name> - добавить на склад один товар с названием product_name.
    // 2) remove <product_name> - удалить со склада один товар с названием product_name
    //  (Если товаров с таким названием на складе нет, то уведомить пользователя об этом)
    // 3) show - вывести содержимое склада (все товары и их количества).
    // 4) exit - завершить программу.

    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                RedisClient.Connect();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return;
            }

            string command;
            Console.WriteLine(@"Input command (add/remove/show)
 Or input empty line to exit");
            while ((command = Console.ReadLine()) != "exit")
            {
                string productName;
                switch (command)
                {
                    case "add":
                        Console.Write("Input product name: ");
                        productName = Console.ReadLine();
                        
                        RedisClient.AddOne($"TaskInt_{productName}");
                        Console.WriteLine("Ok.");
                        break;
                    
                    case "remove":
                        Console.Write("Input product name: ");
                        productName = Console.ReadLine();
                        
                        if (RedisClient.Exist($"TaskInt_{productName}"))
                        {
                            RedisClient.RemoveOne($"TaskInt_{productName}");
                            Console.WriteLine("Ok.");
                        }
                        else
                        {
                            Console.WriteLine("This product is not in warehouse.");
                        }
                        break;
                    
                    case "show":
                        string[] keys = RedisClient.GetKeys("TaskInt_");
                        foreach (var key in keys)
                        {
                            long count = RedisClient.GetAsLong(key);
                            Console.WriteLine($"{key.Replace("TaskInt_", "")}: {count} items.");
                        }
                        break;
                    
                    default:
                        Console.WriteLine("Unknown command");
                        break;
                }
            }

        }
    }
}