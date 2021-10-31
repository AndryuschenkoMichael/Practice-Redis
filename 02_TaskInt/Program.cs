using System;

namespace TaskInt
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

            // place your code here!
        }
    }
}