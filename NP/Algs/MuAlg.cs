using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algs
{
    public class MuAlg
    {
        private readonly int _bagMaxWeight;

        public MuAlg(int bagMaxWeight)
        {
            _bagMaxWeight = bagMaxWeight;
            BestPriceGoodsPack = new GoodsPack();
        }

        public GoodsPack BestPriceGoodsPack { get; set; }

        public void Calc(Good[] goods)
        {
            var groupsCapacity = GetGroupsCapacity(goods);
            //сортируем товары по убыванию цены
            var goodsByPrice = goods.OrderByDescending(g => g.Price);
            Dictionary<int, GoodsPack> bestPacks = new Dictionary<int, GoodsPack>();
            //идем от мин-групп к маскимальным
            for (int groupRang = groupsCapacity.MinElementsCount; groupRang <= groupsCapacity.MaxElementsCount; groupRang++)
            {
                //берем n элементов с макс ценами (n - вместимость группы)
                Good[] mostExpensiveGoods = goodsByPrice.Take(groupRang).ToArray();
                //считаем веса, если общий вес > вместимости рюкзака, решаем какой элемент нужно откинуть из группы (иначе оптимальная группа найдена)
                if (mostExpensiveGoods.Sum(g => g.Weight) <= _bagMaxWeight)
                {
                    GoodsPack bestPack = CreatePack(mostExpensiveGoods);
                    bestPacks.Add(groupRang, bestPack);
                    continue;
                }

                //алгоритм выбора элемента для замены:
                
                var firstOutOfGroupElement = goodsByPrice.ElementAtSafe(groupRang + 1);
                if (firstOutOfGroupElement == null)
                {
                    bestPacks.Add(groupRang, new GoodsPack());
                    continue;
                }

                
                if (SubstituteElement(mostExpensiveGoods, firstOutOfGroupElement))
                {
                    GoodsPack bestPack = CreatePack(mostExpensiveGoods);
                    bestPacks.Add(groupRang, bestPack);
                    continue;
                }

                GoodsPack pack = CalcBestPackAlg(goodsByPrice, mostExpensiveGoods, firstOutOfGroupElement);
                bestPacks.Add(groupRang, pack);

                //Увеличиваем разрядность анализируемой группы
            }

            var packs = bestPacks.Values;
            var maxPackPrice = packs.Max(p => p.Price);
            BestPriceGoodsPack = packs.First(p => p.Price == maxPackPrice);
        }

        private GoodsPack CalcBestPackAlg(IOrderedEnumerable<Good> goodsByPrice, Good[] mostExpensiveGoods, Good firstOutOfGroupElement)
        {
            //Решаем переходить к следующему внегрупповому элементу или делать подстановку с целью уменьшения веса группы. 
            //Для решения сравниваем выигрыш в весе группы, если 
            //1) Заменим i-ый элемент, - всего n разниц
            //2) Перейдем к следующему элементу вне группы, =  Вес (n+1) элемента - вес (n+2) элемента

            var currentOutOfGroupElement = firstOutOfGroupElement;
            currentOutOfGroupElement.Index = mostExpensiveGoods.Length;

            while (currentOutOfGroupElement != null)
            {
                foreach (var good in mostExpensiveGoods)
                {
                    good.DeltaWeight = good.Weight - firstOutOfGroupElement.Weight;
                }

                var nextOutOfGroupElement = goodsByPrice.ElementAtSafe(currentOutOfGroupElement.Index + 1);
                if (nextOutOfGroupElement == null)
                {
                    currentOutOfGroupElement.DeltaWeight = int.MinValue;
                }
                else
                {
                    nextOutOfGroupElement.Index = currentOutOfGroupElement.Index + 1;
                }
                var allElements = new List<Good>(mostExpensiveGoods.Length + 1);
                allElements.AddRange(mostExpensiveGoods);
                allElements.Add(currentOutOfGroupElement);
                var maxDeltaWeight = allElements.Max(el => el.DeltaWeight);
                var mostPriorityByWeightElement = allElements.OrderByDescending(el => el.Price).First(e => e.DeltaWeight == maxDeltaWeight);
                if (mostPriorityByWeightElement.Index < mostExpensiveGoods.Length)
                {//значит нужно сделать ракировку элемента группы с элементом вне группы
                    mostExpensiveGoods[mostPriorityByWeightElement.Index] = currentOutOfGroupElement;
                }

                if (mostExpensiveGoods.Sum(g => g.Price) <= _bagMaxWeight)
                {//Как только найдена группа с суммой элементов < вместимости рюкзака, группаз разрядности n считается найеденнной
                    return CreatePack(mostExpensiveGoods);
                }
                currentOutOfGroupElement = nextOutOfGroupElement;
            }
            return new GoodsPack(); // групп ранга n с  сумой весов < вместимости рюкзака - не существует

        }

        private bool SubstituteElement(Good[] mostExpensiveGoods, Good outOfGroupElement)
        {
            //берем элемент вне группы с макс ценой, и пытаемся его вставить вместо каждого элемента группы (от меньш цены к большей), 
            //если для всех комбинаций вес > вместимости рюкзаке, то группа не найдена (иначе группа найдена)
            for (int i = mostExpensiveGoods.Length - 1; i >= 0; i--)
            {
                var temp = mostExpensiveGoods[i];
                mostExpensiveGoods[i] = outOfGroupElement;
                if (mostExpensiveGoods.Sum(g => g.Weight) <= _bagMaxWeight)
                {
                    return true;
                }
                mostExpensiveGoods[i] = temp;
            }
            return false;
        }

        private GoodsPack CreatePack(IEnumerable<Good> goodsWithMaxPrice)
        {
            var pack = new GoodsPack();
            pack.AddRange(goodsWithMaxPrice);
            return pack;
        }

        /// <summary>
        /// Посчитать группы какой вместимости возможны на переданном наборе элементов
        /// </summary>
        /// <param name="goods"></param>
        /// <returns></returns>
        private GroupsCapacity GetGroupsCapacity(Good[] goods)
        {
            //Отсортировать товары по весам
            //найти мин кол-во элементов в группе и максимальное кол-во элементов            
                        
            var goodsByWeightAsc = goods.OrderBy(g => g.Weight);
            int totalWeight = 0;
            var enumerator = goodsByWeightAsc.GetEnumerator(); 
            //calc max elements count
            GroupsCapacity capacity = new GroupsCapacity();
            //calc max elements count
            while (enumerator.MoveNext())
            {                
                if (totalWeight + enumerator.Current.Weight > _bagMaxWeight)
                {
                    break;
                }
                totalWeight += enumerator.Current.Weight;
                capacity.MaxElementsCount++;
            }

            var goodsByWeightDesc =  goodsByWeightAsc.Reverse();
            enumerator = goodsByWeightDesc.GetEnumerator();
            //calc min elements count
            while (enumerator.MoveNext())
            {
                if (totalWeight + enumerator.Current.Weight > _bagMaxWeight)
                {
                    break;
                }
                totalWeight += enumerator.Current.Weight;
                capacity.MinElementsCount++;
            }
            return capacity;
        }

        private class GroupsCapacity
        {
            public int MinElementsCount { get; set; }
            public int MaxElementsCount { get; set; }
        }

    }
}
