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
        private readonly List<GoodsPack> _goodsPacks;

        public FullEnumerationAlg(int bagMaxWeight)
        {
            _bagMaxWeight = bagMaxWeight;
            _goodsPacks = new List<GoodsPack>();
        }

        public void Calc(Good[] goods)
        {
            int price;
            List<Good> bag = new List<Good>(); // это рюкзак 
            List<string> goodNames = new List<string>(); // буфер id 
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

            goodNames.Clear();
            priceTotal = 0;
            // подсчет общей цены коллекции 
            foreach (Good item in bag)
            {
                price = item.Price;
                goodNames.Add(item.Name);
                priceTotal += price;
            }


            // сбор id в строку 
            idLine = "";
            int count = 0;
            foreach (string i in goodNames)
            {
                if (count != (goodNames.Count - 1))
                    idLine += i + ",";
                else
                    idLine += i;
                count++;
            }

            if (!prices.Contains(Convert.ToString(priceTotal) + ";" + idLine))
            {
                var pack = new GoodsPack();
                pack.AddRange(bag);
                _goodsPacks.Add(pack);
                prices.Add(Convert.ToString(priceTotal) + ";" + idLine);
            }
                
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

            var maximumPrice = _goodsPacks.Max(p => p.Price);
            BestPriceGoodsPack = _goodsPacks.First(p => p.Price == maximumPrice);
            BestPriceGoodsPack.SetTaken();
        }


        public GoodsPack BestPriceGoodsPack { get; private set; }

        public GoodsPack[] UnbestPacks
        {
            get { return _goodsPacks.Where(p => p.Price != BestPriceGoodsPack.Price).ToArray(); }
        }

    }
}
