﻿using System;
using Algs;
using ExistingAlgs;

namespace ryukzak
{
    class Program
    {
        private static int _goodsCount; // Количество товаров
        private static bool incorrEnter; //Переменная, что проверяет корректность ввода
        private static string _enteredName; // Имя товара
        private static int _enteredWeight, _enteredPrice, _bagMaxWeight; // Вес и цена товара, размер рюкзака


        // Метод выводит на экран рюкзак
        static void Print(Good[] goods)
        {
            Console.WriteLine("\nМаксимальная стоимость: " + BellmanAlg.func[_bagMaxWeight, _goodsCount - 1]);
            Console.Write("Взяты следующие предметы: ");
            foreach (Good good in goods)
                if (good.Take)
                    Console.Write(good.Name + " ");
        }

        static void Main(string[] args)
        {
            int i, j; //просто переменные :)

            Console.WriteLine("Данная программа поможет Вам заполнить Ваш рюкзак максимально ценными товарами\nАвтор: Владимир Рыбалка");

            //Введем количество товаров
            do
            {
                incorrEnter = false;
                Console.Write("\nКакое количество товаров вы рассматриваете для приобретения: ");
                try
                {
                    _goodsCount = Convert.ToInt32(Console.ReadLine());
                }
                catch (FormatException)
                {
                    incorrEnter = true;
                    _goodsCount = 0;
                }
            } while (incorrEnter);

            var goods = new Good[_goodsCount]; //Создаем массив заданного размера

            //Создадим объекты товаров
            for (i = 0, j = i + 1; i < goods.Length; i++, j++)
            {
                Console.Write("\n\nВведите название " + j + "-го товара: ");
                _enteredName = Console.ReadLine();
                do
                {
                    incorrEnter = false;
                    Console.Write("Введите вес товара: ");
                    try
                    {
                        _enteredWeight = Convert.ToInt32(Console.ReadLine());
                    }
                    catch (FormatException)
                    {
                        incorrEnter = true;
                        _enteredWeight = 0;
                    }
                } while (incorrEnter);
                do
                {
                    incorrEnter = false;
                    Console.Write("Введите цену товара: ");
                    try
                    {
                        _enteredPrice = Convert.ToInt32(Console.ReadLine());
                    }
                    catch (FormatException)
                    {
                        incorrEnter = true;
                        _enteredPrice = 0;
                    }
                } while (incorrEnter);
                goods[i] = new Good(_enteredName, _enteredWeight, _enteredPrice); //Создаем объект
            }

            // Введем размер рюкзака
            do
            {
                incorrEnter = false;
                Console.Write("\nВведите размер вашего рюкзака (контейнера): ");
                try
                {
                    _bagMaxWeight = Convert.ToInt32(Console.ReadLine());
                }
                catch (FormatException)
                {
                    incorrEnter = true;
                    _bagMaxWeight = 0;
                }
            } while (incorrEnter);

            Print(goods);

            Console.ReadKey();
        }
    }
}