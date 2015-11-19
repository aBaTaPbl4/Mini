using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algs
{
    public class Good //товар
    {
        public string Name { get; set; }
        public int Weight { get; set; }
        public int Price { get; set; } //цена товара
        public bool IsTaken { get; set; } //берем ли товар

        public Good(string n, int w, int p)
        {
            Name = n;
            Weight = w;
            Price = p;
        }
    }
}
