using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algs
{
    public class FullEnumerationAlg
    {
        private readonly int _bagMaxWeight;

        public FullEnumerationAlg(int bagMaxWeight)
        {
            _bagMaxWeight = bagMaxWeight;
        }

        public void Calc(Good[] goods)
        {
            int price;
            List<Good> bag = new List<Good>(); // это рюкзак 
            List<string> ids = new List<string>(); // буфер id 
            List<string> prices = new List<string>(); // список цен коллекций 
            Random random = new Random();
            Good good;
            int weightTotal = 0;
            int priceTotal = 0;
            string idLine = "";
            int errorCount = 0;
            int maxPrice;

            startLoop:
            bag.Clear();
            nextAdd:
            // добавление предметов в рюкзак в разном порядке
            good = goods[random.Next(0, Convert.ToInt32(goods.Length))];
            if (!bag.Contains(good))
                bag.Add(good);
            else
                goto nextAdd;
            weightTotal = 0;
            // проверка общего веса 
            foreach (Good item in bag)
            {
                weightTotal += item.Weight;
            }
            if (weightTotal < _bagMaxWeight)
                goto nextAdd;
            else
            {
                for (int i = 0; ; i++)
                {
                    if (weightTotal <= _bagMaxWeight) break;
                    else
                    {
                        bag.RemoveAt(Convert.ToInt32(bag.Count - 1));
                        // проверка общего веса 
                        weightTotal = 0;
                        foreach (Good item in bag)
                        {
                            weightTotal += item.Weight;
                        }
                    }
                }
            }

            ids.Clear();
            priceTotal = 0;
            // подсчет общей цены коллекции 
            foreach (Good item in bag)
            {
                price = item.Price;

                ids.Add(item.Name);
                priceTotal += price;
            }

            // сбор id в строку 
            idLine = "";
            int count = 0;
            foreach (string i in ids)
            {
                if (count != (ids.Count - 1)) idLine += i + ",";
                else idLine += i;
                count++;
            }

            if (!prices.Contains(Convert.ToString(priceTotal) + ";" + idLine)) prices.Add(Convert.ToString(priceTotal) + ";" + idLine);
            else
            {
                errorCount++;
                if (errorCount >= ((goods.Length) * (goods.Length))) goto countingPrice;
            }
            goto startLoop;

            countingPrice:
            // находим максимальную сумму коллекции из собранных вариантов 
            maxPrice = 0;
            string idBufer = "";
            foreach (string i in prices)
            {
                string[] bufer = i.Split(';');
                price = Convert.ToInt32(bufer[0]);
                idLine = bufer[1];

                if (price > maxPrice)
                {
                    maxPrice = price;
                    idBufer = idLine;
                }
            }

            Console.WriteLine("Most expensive pack from Knapsack:\ncost: {0} id: {1}", maxPrice, idBufer);
            Console.WriteLine("\nAnother variants of pack:");
            foreach (string i in prices)
            {
                if (!i.Contains(Convert.ToString(maxPrice))) Console.WriteLine(i);
            }

            TotalPrice = maxPrice;
            Ids = idBufer;
        }

        public int TotalPrice { get; private set; }

        public string Ids { get; private set; }
    }
}
