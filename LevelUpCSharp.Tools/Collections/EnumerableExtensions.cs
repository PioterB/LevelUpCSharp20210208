using System;
using System.Collections.Generic;

namespace LevelUpCSharp.Collections
{
    public static class EnumerableExtensions
    {
        public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            foreach (var item in source)
            {
                action(item);
            }
        }

        public static void ForEach(this IEnumerable<int> source, Action<int> action)
        {
            foreach (var item in source)
            {
                action(item);
            }
        }


        public static void ForEach(this IEnumerable<int> source)
        {
            foreach (var item in source)
            {
                /* work on item - hard code logic here */
            }
        }

        public static bool IsOdd(this int source)
        {
            return source % 2 == 1;
        }
    }
}