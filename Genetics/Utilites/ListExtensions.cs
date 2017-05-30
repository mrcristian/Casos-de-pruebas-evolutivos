using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Genetics.Utilites
{
    static class ListExtensions
    {
        private static Random rng = new Random();

        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
        public static T Random<T>(this IList<T> list)
        {
            int n = list.Count;
            return list[rng.Next(n)];
        }

        
    }
}
