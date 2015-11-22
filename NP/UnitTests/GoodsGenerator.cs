using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Algs;

namespace UnitTests
{
    public class GoodsGenerator
    {
        public Good[] GenerateGoods(int count, int maxPrice, int maxWeight)
        {
            var rand = new Random();
            var goods = new Good[count];
            for (int i = 0; i < count; i++)
            {
                string name = (i + 1).ToString();
                goods[i] = new Good(name);
                goods[i].Price = rand.Next(1, maxPrice + 1);
                goods[i].Weight = rand.Next(1, maxWeight + 1);
            }
            return goods;
        }

    }
}
