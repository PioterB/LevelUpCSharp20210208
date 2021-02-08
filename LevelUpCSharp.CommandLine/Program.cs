using System;

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

            mySecondNuymbner.IsOdd();
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
