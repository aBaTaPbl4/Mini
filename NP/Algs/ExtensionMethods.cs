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
            if (  index < 0 || source.Count() <= index)
            {
                return default(TSource);
            }
            return source.ElementAt(index);
        }
    }
}
