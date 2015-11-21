using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algs
{
    public class PackPrinter
    {
        public void PrintPack(GoodsPack pack)
        {
            var sb = new StringBuilder();
            sb.AppendFormat("cost: {0} weight: {1}\n", pack.Price, pack.Weight);
            sb.AppendLine("items:");
            foreach (var good in pack)
            {
                sb.AppendFormat("{0} + ", good);
            }
            Console.WriteLine(sb.ToString(0, sb.Length - 3));
        }

        public void PrintPackShort(GoodsPack pack)
        {
            var sb = new StringBuilder();
            sb.AppendFormat("cost: {0} weight: {1} items: ", pack.Price, pack.Weight);
            foreach (var good in pack)
            {
                sb.AppendFormat("{0},", good.Name);
            }
            Console.WriteLine(sb.ToString(0, sb.Length - 1));
        }

        public void PrintPackShort(GoodsPack[] packs)
        {
            Console.WriteLine();
            var packsOrdered = packs.OrderByDescending(p => p.Price);
            foreach (var unbestPack in packsOrdered)
            {
                PrintPackShort(unbestPack);
            }
        }
    }
}
