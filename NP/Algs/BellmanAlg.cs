using System.Linq;
using Algs;


namespace ExistingAlgs
{
    public class BellmanAlg
    {
        private readonly int _maxWeight;
        private readonly int _goodsCount;
        private static int[,] _matrix; //массив для хранения значений функции
        private Good[] _goods;


        public BellmanAlg(int maxWeight, int goodsCount)
        {
            _maxWeight = maxWeight;
            _goodsCount = goodsCount;
        }

        public void Calc(Good[] goods)
        {
            _goods = goods;
            _matrix = new int[_maxWeight + 1, _goodsCount]; //Реализуем массив функции

            //Реализуем алгоритм Беллмана
            for (int weight = 1; weight <= _maxWeight; weight++) // Загружаем рюкзак если его вместимость = Weight
                for (int i = 1; i < _goodsCount; i++) // берем предметы с 1 по goodsCount
                    //если вес предмета больше Weight, или предыдущий набор лучше выбираемого
                    if (goods[i].Weight > weight)
                    {
                        _matrix[weight, i] = _matrix[weight, i - 1]; //тогда берем предыдущий набор
                        goods[i].IsTaken = false;
                    }
                    else if (_matrix[weight, i - 1] >= (_matrix[weight - goods[i].Weight, i - 1] + goods[i].Price))
                    {
                        _matrix[weight, i] = _matrix[weight, i - 1]; //тогда берем предыдущий набор
                        goods[i].IsTaken = false;
                    }
                    else
                    {
                        _matrix[weight, i] = _matrix[weight - goods[i].Weight, i - 1] + goods[i].Price; //иначе добавляем к предыдущему набору текущий предмет
                        goods[i].IsTaken = true;
                    }
        }

        public GoodsPack BestPriceGoodsPack
        {
            get
            {
                if (_goods == null)
                {
                    return new GoodsPack();
                }
                var pack = new GoodsPack();
                var takenGoods = _goods.Where(g => g.IsTaken);
                pack.AddRange(takenGoods);
                return pack;
            }
        }
    }
}
