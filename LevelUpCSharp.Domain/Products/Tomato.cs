using System;

namespace LevelUpCSharp.Products
{
    public class Tomato : IAddition
    {
        public string Name => "tomato";
        public DateTimeOffset ExpirationDate => DateTimeOffset.Now.AddMonths(1);
    }
}