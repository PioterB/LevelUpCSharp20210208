using System;

namespace LevelUpCSharp.Products
{
    public class Olives : IAddition
    {
        public string Name => "olives";
        public DateTimeOffset ExpirationDate => DateTimeOffset.Now.AddMonths(1);
    }
}