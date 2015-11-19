using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Algs;
using ExistingAlgs;
using NUnit.Framework;

namespace UnitTests
{
    [TestFixture]
    public class BellmanAlgTests
    {

        [Test]
        public void Test1()
        {
            var goods = new Good[9];
            goods[0].Name = "36";
            goods[0].Weight = 36;
            goods[0].Price = 68;

            goods[1].Name = "18";
            goods[1].Weight = 18;
            goods[1].Price = 10;

            goods[2].Name = "19";
            goods[2].Weight = 19;
            goods[2].Price = 50;


            goods[3].Name = "23";
            goods[3].Weight = 23;
            goods[3].Price = 20;

            goods[4].Name = "29";
            goods[4].Weight = 29;
            goods[4].Price = 65;

            goods[5].Name = "32";
            goods[5].Weight = 32;
            goods[5].Price = 30;

            goods[6].Name = "35";
            goods[6].Weight = 35;
            goods[6].Price = 60;

            goods[7].Name = "44";
            goods[7].Weight = 44;
            goods[7].Price = 40;

            goods[8].Name = "67";
            goods[8].Weight = 67;
            goods[8].Price = 70;

            var alg = new BellmanAlg(100, 10);
            alg.Calc(goods);
            StringBuilder sb = new StringBuilder();
            foreach (var good in goods)
            {
                if (good.IsTaken)
                {
                    sb.Append("{0}(Value:{1}) +", good.Weight, good.Price);
                }
            }
            string result = sb.ToString(0, sb.Length - 2);
            Console.WriteLine("{0} = {1}", result, alg.MaxValueGoodSet);
        }
    }
}
