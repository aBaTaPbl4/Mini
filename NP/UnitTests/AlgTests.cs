using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Algs;
using ExistingAlgs;
using NUnit.Framework;

namespace UnitTests
{
    [TestFixture]
    public class AlgTests
    {
        private PackPrinter _printer = new PackPrinter();

        [Test]
        public void BellmanAlgTest_With9Goods()
        {
            var goods = Create9GoodsArray();

            var alg = new BellmanAlg(100, 4);
            alg.Calc(goods);
            _printer.PrintPack(alg.BestPriceGoodsPack);
        }


        [Test]
        public void FullEnumerationAlgTest_With3Goods()
        {            
            int bagMaxWeight = 80;
            var alg = new FullEnumerationAlg(bagMaxWeight);
            Good[] goods = Create3GoodsArray();
            alg.Calc(goods);
            Assert.AreEqual(190, alg.BestPriceGoodsPack.Price);
            Assert.IsTrue(alg.BestPriceGoodsPack.ContainsGoodWithName("2"));
            Assert.IsTrue(alg.BestPriceGoodsPack.ContainsGoodWithName("3"));
            _printer.PrintPack(alg.BestPriceGoodsPack);
        }

        [Test]
        public void FullEnumerationAlgTest_With9Goods()
        {
            int bagMaxWeight = 100;
            var alg = new FullEnumerationAlg(bagMaxWeight);
            Good[] goods = Create9GoodsArray();
            alg.Calc(goods);
            _printer.PrintPack(alg.BestPriceGoodsPack);
            Assert.AreEqual(193, alg.BestPriceGoodsPack.Price);
            Assert.IsTrue(alg.BestPriceGoodsPack.ContainsGoodWithName("5"));
            Assert.IsTrue(alg.BestPriceGoodsPack.ContainsGoodWithName("7"));
            Assert.IsTrue(alg.BestPriceGoodsPack.ContainsGoodWithName("1"));
            _printer.PrintPackShort(alg.UnbestPacks);
        }


        [Test]
        public void MuAlgTest_With3Goods()
        {
            int bagMaxWeight = 80;
            var alg = new MuAlg(bagMaxWeight);
            Good[] goods = Create3GoodsArray();
            alg.Calc(goods);
            Assert.AreEqual(190, alg.BestPriceGoodsPack.Price);
            Assert.IsTrue(alg.BestPriceGoodsPack.ContainsGoodWithName("2"));
            Assert.IsTrue(alg.BestPriceGoodsPack.ContainsGoodWithName("3"));
            _printer.PrintPack(alg.BestPriceGoodsPack);
        }

        [Test]
        public void MuAlgTest_With9Goods()
        {
            int bagMaxWeight = 100;
            var alg = new MuAlg(bagMaxWeight);
            Good[] goods = Create9GoodsArray();
            alg.Calc(goods);
            _printer.PrintPack(alg.BestPriceGoodsPack);
            Assert.AreEqual(193, alg.BestPriceGoodsPack.Price);
            Assert.IsTrue(alg.BestPriceGoodsPack.ContainsGoodWithName("5"));
            Assert.IsTrue(alg.BestPriceGoodsPack.ContainsGoodWithName("7"));
            Assert.IsTrue(alg.BestPriceGoodsPack.ContainsGoodWithName("1"));
        }


        private static Good[] Create3GoodsArray()
        {
            var goods = new Good[3];
            for (int i = 0; i < 3; i++)
            {
                string name = (i + 1).ToString();
                goods[i] = new Good(name);
            }

            goods[0].Weight = 20;
            goods[0].Price = 60;

            goods[1].Weight = 30;
            goods[1].Price = 90;

            goods[2].Weight = 50;
            goods[2].Price = 100;
            return goods;
        }


        private static Good[] Create9GoodsArray()
        {
            var goods = new Good[9];
            for (int i = 0; i < 9; i++)
            {
                string name = (i + 1).ToString();
                goods[i] = new Good(name);
            }

            goods[0].Weight = 36;
            goods[0].Price = 68;

            goods[1].Weight = 18;
            goods[1].Price = 10;

            goods[2].Weight = 19;
            goods[2].Price = 50;

            goods[3].Weight = 23;
            goods[3].Price = 20;

            goods[4].Weight = 29;
            goods[4].Price = 65;

            goods[5].Weight = 32;
            goods[5].Price = 30;

            goods[6].Weight = 35;
            goods[6].Price = 60;

            goods[7].Weight = 44;
            goods[7].Price = 40;

            goods[8].Weight = 67;
            goods[8].Price = 70;
            return goods;
        }
    }
}
