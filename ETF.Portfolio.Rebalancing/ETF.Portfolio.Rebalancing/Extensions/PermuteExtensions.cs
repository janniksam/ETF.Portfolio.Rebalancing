using System;
using System.Collections.Generic;
using System.Linq;

namespace ETF.Portfolio.Rebalancing.Extensions
{
    public static class PermuteExtensions
    {
        // This algo is taken from an StackOverflow-Answer, for more information see:
        // https://stackoverflow.com/a/10629938/985798
        public static IEnumerable<IEnumerable<T>> GetKCombs<T>(this IEnumerable<T> list, int length) 
            where T : IComparable
        {
            if (length == 1)
            {
                return list.Select(t => new T[] { t });
            }

            return GetKCombs(list, length - 1)
                .SelectMany(t => list.Where(o => o.CompareTo(t.Last()) > 0),
                    (t1, t2) => t1.Concat(new T[] { t2 }));
        }
    }
}
