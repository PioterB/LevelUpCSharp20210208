using System;

namespace LevelUpCSharp.Products
{
    public class Ham : IKeyIngredient
    {
        public SandwichKind Kind => SandwichKind.Pork;
        public string Name => "ham";
        public DateTimeOffset ExpirationDate => DateTimeOffset.Now.AddDays(10);
    }
}