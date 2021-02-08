using System;

namespace LevelUpCSharp.Products
{
    public class Lettuce : IAddition
    {
        public string Name => "lettuce";
        public DateTimeOffset ExpirationDate => DateTimeOffset.Now.AddMonths(1);
    }
}