using System;
using System.Collections.Generic;
using System.Linq;
using LevelUpCSharp.Products;

namespace LevelUpCSharp.CommandLine
{
    class Program
    {
        static void Main(string[] args)
        {
            int myNumber = 5;

            int mySecondNuymbner = 4;

            if (myNumber % 2 == 1)
            {
                /* react on odd */
            }

            if (MyExtenstion.IsOddOldWay(myNumber))
            {
                /* react on odd */
            }

            if (myNumber.IsOdd())
            {
                /* react on odd */
            }

            if (myNumber % 2 == 1)
            {
            }

            mySecondNuymbner.IsOdd();

            var sandwiches = new List<Sandwich>();
            sandwiches
                .Where(sandwich => sandwich.ExpirationDate > DateTime.Now.AddDays(1))
                .Where(sandwich => sandwich.IngredientsCount > 2)
                .ToList()
                .Count()
                .IsOdd()
                .Equals(true);
        }
    }

    static class MyExtenstion
    {
        public static bool IsOdd(this int source)
        {
            return source % 2 == 1;
        }

        public static bool IsOddOldWay(int source)
        {
            return source % 2 == 1;
        }
    }
}
