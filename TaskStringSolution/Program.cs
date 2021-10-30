using System;

namespace TaskStringSolution
{
    class Program
    {
        // Разработчики одной крупной компании (HSE company) столкнулись с такой проблемой:
        // HSE company выпускает множество продуктов и постоянно их обновляет. 
        // Разработчики поняли, что хранить в одной Exel таблице актуальные версии приложений неудобно; 
        // но времени на решение этой задачи у них совсем не осталось.
        // Помогите им!
        //
        // На вход поступают запросы вида: name_of_application new_version.
        // Требуется вывести текущую версию приложения и заменить ее на новую,
        // или же вывести, что такого приложения не существует, и добавить его в список.
        
        static void Main(string[] args)
        {
            try
            {
                RedisClient.Connect("localhost");
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }

            string query;
            while ((query = Console.ReadLine()) != "")
            {
                string[] inputLines = query.Split(' ');
                string name = inputLines[0];
                string newVersion = inputLines[1];

                if (!RedisClient.Exist($"TaskString_{name}"))
                {
                    Console.WriteLine("Application is not exist");
                    // Так как один и тот же сервер Redis могут использовать другие приложения для своих целей, 
                    // то хорошей практикой явлется называть ключи специфично.
                    RedisClient.Set($"TaskString_{name}", newVersion);
                }
                else
                {
                    Console.WriteLine($"Current version of the {name}: {RedisClient.GetSet($"TaskString_{name}", newVersion)}");
                }
            }
        }
    }
}