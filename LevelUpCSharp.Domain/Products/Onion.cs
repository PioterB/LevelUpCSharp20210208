using System;

namespace LevelUpCSharp.Products
{
    public class Onion : IAddition
    {
        public string Name => "onion";
        public DateTimeOffset ExpirationDate => DateTimeOffset.Now.AddMonths(1);
    }
}