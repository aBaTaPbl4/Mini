using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algs
{
    public class GoodsPack : IEnumerable<Good>
    {
        private List<Good> _pack;

        public GoodsPack()
        {
            _pack = new List<Good>();
        }

        public bool Add(Good good)
        {
            if (!_pack.Contains(good))
            {
                _pack.Add(good);
                return true;
            }
            return false;
        }

        public void AddRange(IEnumerable<Good> goods)
        {
            foreach (var item in goods)
            {
                Add(item);
            }
        }

        public int Count
        {
            get { return _pack.Count;}
        }

        public Good[] Goods
        {
            get { return _pack.ToArray(); }
        }


        public int Price
        {
            get { return _pack.Sum(g => g.Price); }
        }

        public int Weight
        {
            get { return _pack.Sum(g => g.Weight); }
        }

        public bool ContainsGoodWithName(string name)
        {
            return _pack.Any(g => g.Name == name);
        }

        public void SetTaken()
        {
            foreach (var good in _pack)
            {
                good.IsTaken = true;
            }
        }

        public IEnumerator<Good> GetEnumerator()
        {
            return _pack.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _pack.GetEnumerator();
        }

        public override bool Equals(object obj)
        {
            GoodsPack goodsPack = obj as GoodsPack;
            if (goodsPack == null)
            {
                return false;
            }

            if (goodsPack.Count != Count)
            {
                return false;
            }
            foreach (Good item in goodsPack)
            {
                if (!_pack.Contains(item))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
