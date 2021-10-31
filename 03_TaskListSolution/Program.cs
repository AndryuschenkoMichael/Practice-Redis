using System;

namespace TaskListSolution
{
    // Сиквел.
    // Разработчики из HSE company просят доработать ваше приложение!
    // Дело в том, что разработчики тоже ошибаются, и приходится откатывать приложение к предыдущей версии.
    // К тому же, HSE company не хочет расходовать много памяти,
    // поэтому было принято решение хранить только определенное колличество последних версий приложений.
    // 
    // На вход программе подаются запросы следующего типа:
    // 1) add <application_name> <version> - добавить актуальную версию приложения.
    // 2) back <application_name> - откатить приложение до предыдущей версии. Если предыдущей нет, то удалить приложение.
    // 3) get <application_name> - получить актуальную версию приложения. Если приложения нет, то сообщить об этом.
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
            Console.WriteLine(@"Input command (add/back/get)
 Or input empty line to exit");
            while ((command = Console.ReadLine()) != "exit")
            {
                string applicationName;
                string version;
                switch (command)
                {
                    case "add":
                        Console.Write("Input application name: ");
                        applicationName = Console.ReadLine();
                        Console.Write("Input application version: ");
                        version = Console.ReadLine();
                        
                        RedisClient.Add($"TaskList_{applicationName}", version);
                        Console.WriteLine("Ok.");
                        break;
                    
                    case "back":
                        Console.Write("Input application name: ");
                        applicationName = Console.ReadLine();
                        
                        if (RedisClient.Exist($"TaskList_{applicationName}"))
                        {
                            version = RedisClient.Back($"TaskList_{applicationName}");
                            Console.WriteLine($"{applicationName}. Deleted version: {version}");
                        }
                        else
                        {
                            Console.WriteLine($"Application {applicationName} does not exist.");
                        }
                        break;
                    
                    case "get":
                        Console.Write("Input application name: ");
                        applicationName = Console.ReadLine();
                        
                        if (RedisClient.Exist($"TaskList_{applicationName}"))
                        {
                            version = RedisClient.Get($"TaskList_{applicationName}");
                            Console.WriteLine($"The current version of the application {applicationName}: {version}");
                        }
                        else
                        {
                            Console.WriteLine($"Application {applicationName} does not exist.");
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