using System;

namespace TaskSetSolution
{
    // Сиквел.
    // У Склад.LIFE большое колличество различных складов с различными видами товаров.
    // Руководству важно знать, какие товары находятся. Помогите Склад.LIFE. 
    // P.S. В последнее время с заказами все плохо, поэтому на склад только завозят новые товары.
    //
    // На вход программе поступают следующие запросы:
    // 1) add <storage_name> <product_name> - добавить товар на склад.
    // 2) get <storage_name> - получить список всех товаров на складе.
    // 3) exist <storage_name> <product_name> - узнать находится ли товар на складе.
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
                string storageName;
                string productName;
                switch (command)
                {
                    case "add":
                        Console.Write("Enter name of the storage: ");
                        storageName = Console.ReadLine();
                        Console.Write("Enter name of the product: ");
                        productName = Console.ReadLine();

                        RedisClient.Add($"TaskSet_{storageName}", productName);
                        Console.WriteLine("Ok.");
                        break;
                    
                    case "exist":
                        Console.Write("Enter name of the storage: ");
                        storageName = Console.ReadLine();
                        Console.Write("Enter name of the product: ");
                        productName = Console.ReadLine();
                        
                        if (RedisClient.Exist($"TaskSet_{storageName}"))
                        {
                            if (RedisClient.ExistProduct($"TaskSet_{storageName}", productName))
                            {
                                Console.WriteLine("Product in storage.");
                            }
                            else
                            {
                                Console.WriteLine("Product out of storage");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Storage is not exist");
                        }
                        break;
                    
                    case "get":
                        Console.Write("Enter name of the storage: ");
                        storageName = Console.ReadLine();
                        
                        if (RedisClient.Exist($"TaskSet_{storageName}"))
                        {
                            Console.WriteLine($"Products in {storageName}: " +
                                              $"{string.Join(", ", RedisClient.GetProducts($"TaskSet_{storageName}"))}");
                        }
                        else
                        {
                            Console.WriteLine("Storage is not exist");
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