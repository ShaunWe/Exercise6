using System;
using System.Collections.Generic;

namespace Exercise6
{
    static class MyExtensions
    {
        public static Random rng = new Random();

        public static void Shuffle<T>(this IList<T> list)
        {
            int i = list.Count;
            //This code block randomly shuffles the list
            while (i > 1)
            {
                i--;
                int j = rng.Next(i + 1);
                T value = list[j];
                list[j] = list[i];
                list[i] = value;
            }
        }
    }
}
