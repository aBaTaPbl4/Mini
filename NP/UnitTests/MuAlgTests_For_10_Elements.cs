using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Algs;
using NUnit.Framework;

namespace UnitTests
{
    [TestFixture]
    public class MuAlgTests_For_10_Elements
    {        
        GoodsGenerator _goodsGenerator = new GoodsGenerator();
        PackPrinter _printer = new PackPrinter();
        [Test]
        public void Test([Random(1, 100, 1000)] int bagCapacity)
        {
            var fuAlg = new FullEnumerationAlg(bagCapacity);
            var muAlg = new MuAlg(bagCapacity);
            var goods = _goodsGenerator.GenerateGoods(10, 200, 111);
            fuAlg.Calc(goods);
            muAlg.Calc(goods);
            if (!fuAlg.BestPriceGoodsPack.Equals(muAlg.BestPriceGoodsPack))
            {
                Console.WriteLine("Failed goods set:");
                _printer.PrintGoods(goods);
                Console.WriteLine();
                Console.WriteLine("Full enumeration alg results:");
                _printer.PrintPack(fuAlg.BestPriceGoodsPack);
                Console.WriteLine();
                Console.WriteLine("Mu enumeration alg results:");
                _printer.PrintPack(muAlg.BestPriceGoodsPack);
                Console.WriteLine();
                Assert.Fail("Different result received");
            }
            Assert.Pass("ok");
        }

        [TestCase(37, @"76:15|52:32|40:77|163:58|84:35|81:65|12:20|49:7|57:90|128:111")]
        public void FailedCaseTest(int bagCapacity, string elements )
        {
            var fuAlg = new FullEnumerationAlg(bagCapacity);
            var muAlg = new MuAlg(bagCapacity);
            Good[] goods = _goodsGenerator.GenerateGoods(elements);
            fuAlg.Calc(goods);
            muAlg.Calc(goods);
            Assert.AreEqual(fuAlg.BestPriceGoodsPack, muAlg.BestPriceGoodsPack);
        }
    }
}
