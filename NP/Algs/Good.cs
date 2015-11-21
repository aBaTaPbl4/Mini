using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace Algs
{
    public class Good //товар
    {
        private readonly string _name;

        public string Name
        {
            get { return _name; }
        }
        public int Weight { get; set; }
        public int Price { get; set; } //цена товара
        public bool IsTaken { get; set; } //берем ли товар


        public int DeltaWeight { get; set; }
        public int Index { get; set; }

        public Good(string name)
        {
            _name = name;
        }


        public static bool operator ==(Good good1, Good good2)
        {
            if (Object.ReferenceEquals(good1, null) || Object.ReferenceEquals(good2, null))
            {
                return Object.ReferenceEquals(good1, good2);
            }
            return good1.Equals(good2);
        }

        public static bool operator !=(Good good1, Good good2)
        {
            return !(good1 == good2);
        }

        public override bool Equals(object obj)
        {
            var g = obj as Good;
            if (g == null)
            {
                return false;
            }
            return Name == g.Name;
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }

        public override string ToString()
        {
            return $"{Name}(W:{Weight},P:{Price})";
        }
    }
}
