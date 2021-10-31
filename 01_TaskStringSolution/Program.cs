using System;

namespace TaskStringSolution
{
    class Program
    {
        // Разработчики одной крупной компании (HSE company) столкнулись с такой проблемой:
        // HSE company выпускает множество программных продуктов и постоянно их обновляет. 
        // Разработчики поняли, что хранить в Excel-таблице актуальные версии приложений неудобно; 
        // но времени на решение этой задачи у них совсем не осталось.
        // Помогите им!
        //
        // На вход поступают запросы вида: name_of_application new_version.
        // Требуется вывести текущую версию приложения и заменить ее на новую (если оно было в Redis),
        // или же вывести, что такого приложения не существует, и добавить его в Redis.

        static void Main(string[] args)
        {
            try
            {
                RedisClient.Connect();
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return;
            }

            Console.WriteLine(@"Input query in format: <name_of_application> <new_version>
Or input empty line to exit");
            string query;
            while (!string.IsNullOrEmpty(query = Console.ReadLine()))
            {
                string[] inputLines = query.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                if (inputLines.Length != 2) {
                    Console.WriteLine("Format: <name_of_application> <new_version>");
                    continue;
                }
                string name = inputLines[0];
                string newVersion = inputLines[1];

                if (RedisClient.Exist($"TaskString_{name}"))
                {
                    Console.WriteLine($"Current version of the {name}: {RedisClient.GetSet($"TaskString_{name}", newVersion)}");
                }
                else
                {
                    Console.WriteLine($"Application {name} does not exist");
                    // Так как один и тот же сервер Redis могут использовать другие приложения для своих целей, 
                    // то хорошей практикой явлется называть ключи специфично (использкем префикс "TaskString_").
                    RedisClient.Set($"TaskString_{name}", newVersion);
                }
            }
        }
    }
}