using System;
using System.Collections.Generic;

namespace TaskIntSolution
{
    // Компания Склад.LIFE занимается грузоперевозками.
    // Напишите программу, которая отвечает за учет товаров на складе.
    
    // Существует 4 типа запросов:
    // 1) add <product_name> - добавить на склад товар с названием product_name.
    // 2) remove <product_name> - убрать со склада 1 товар с названием product_name
    //  (Если товаров с таким именем на складе нет, то уведомить пользователя об этом)
    // 3) show - вывести содержимое скалада(товары и их колличество).
    // 4) exit - завершить программу.
    
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                RedisClient.Connect("localhost");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return;
            }

            string command;
            while ((command = Console.ReadLine()) != "exit")
            {
                string productName;
                switch (command)
                {
                    case "add":
                        Console.Write("Enter name of product: ");
                        productName = Console.ReadLine();
                        
                        RedisClient.Add($"TaskInt_{productName}");
                        Console.WriteLine("Ok.");
                        break;
                    
                    case "remove":
                        Console.Write("Enter name of product: ");
                        productName = Console.ReadLine();
                        
                        if (RedisClient.Exist($"TaskInt_{productName}"))
                        {
                            RedisClient.Remove($"TaskInt_{productName}");
                            Console.WriteLine("Ok.");
                        }
                        else
                        {
                            Console.WriteLine("This product is not in storage.");
                        }
                        break;
                    
                    case "show":
                        List<string> keys = RedisClient.GetKeys("TaskInt_");
                        foreach (var key in keys)
                        {
                            long count = RedisClient.Get(key);
                            Console.WriteLine($"{key.Replace("TaskInt_", "")}: {count} pc.");
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