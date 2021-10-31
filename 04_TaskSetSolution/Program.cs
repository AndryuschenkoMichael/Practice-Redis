using System;

namespace TaskSetSolution
{
    // Сиквел.
    // У Склад.LIFE большое количество различных складов с различными видами товаров.
    // Руководству важно знать, какие виды товаров находятся на различных складах. Помогите Склад.LIFE. 
    // P.S. В последнее время с заказами все плохо, поэтому на склад только завозят новые виды товаров.
    //
    // На вход программе поступают следующие запросы:
    // 1) add <storage_name> <product_name> - добавить вид товара на склад.
    // 2) get <storage_name> - получить список всех видов товаров на складе.
    // 3) exist <storage_name> <product_name> - узнать находится ли вид товара на складе.
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
            Console.WriteLine(@"Input command (add/exist/get)
 Or input empty line to exit");
            while ((command = Console.ReadLine()) != "exit")
            {
                string storageName;
                string productName;
                switch (command)
                {
                    case "add":
                        Console.Write("Input storage name: ");
                        storageName = Console.ReadLine();
                        Console.Write("Input product name: ");
                        productName = Console.ReadLine();

                        RedisClient.Add($"TaskSet_{storageName}", productName);
                        Console.WriteLine("Ok.");
                        break;
                    
                    case "exist":
                        Console.Write("Input storage name: ");
                        storageName = Console.ReadLine();
                        Console.Write("Input product name: ");
                        productName = Console.ReadLine();
                        
                        if (RedisClient.Exist($"TaskSet_{storageName}"))
                        {
                            if (RedisClient.ExistProduct($"TaskSet_{storageName}", productName))
                            {
                                Console.WriteLine($"Product {productName} is in storage {storageName}.");
                            }
                            else
                            {
                                Console.WriteLine($"Product {productName} is out of storage {storageName}");
                            }
                        }
                        else
                        {
                            Console.WriteLine($"Storage {storageName} does not exist");
                        }
                        break;
                    
                    case "get":
                        Console.Write("Input storage name: ");
                        storageName = Console.ReadLine();
                        
                        if (RedisClient.Exist($"TaskSet_{storageName}"))
                        {
                            Console.WriteLine($"Products in {storageName}: " +
                                              $"{string.Join(", ", RedisClient.GetProducts($"TaskSet_{storageName}"))}");
                        }
                        else
                        {
                            Console.WriteLine($"Storage {storageName} does not exist");
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