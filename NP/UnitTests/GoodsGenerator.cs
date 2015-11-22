using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;
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

        internal Good[] GenerateGoods(string elementsStringRepresentation)
        {
            string[] strElements = elementsStringRepresentation.Split('|');
            List<Good> goods = new List<Good>(strElements.Length);
            int i = 0;
            foreach (var strElement in strElements)
            {
                i++;
                var values = strElement.Split(':');
                var good = new Good(i.ToString());
                good.Price = int.Parse(values[0]);
                good.Weight = int.Parse(values[1]);
                goods.Add(good);
            }
            return goods.ToArray();
        }
    }
}
