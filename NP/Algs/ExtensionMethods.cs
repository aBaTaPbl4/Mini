using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algs
{
    public static class ExtensionMethods
    {
        public static TSource ElementAtSafe<TSource>(this IEnumerable<TSource> source, int index)
        {
            if (source.Count() >= index)
            {
                return default;
            }
            return source.ElementAt(index);
        }
    }
}
