using System;

namespace TaskInt
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
            
        }
    }
}