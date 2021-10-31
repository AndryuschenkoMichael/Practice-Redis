using System;

namespace TaskString
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
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return;
            }

            // place your code here!
        }
    }
}