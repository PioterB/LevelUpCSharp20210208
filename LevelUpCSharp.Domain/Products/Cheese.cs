using System;

namespace LevelUpCSharp.Products
{
    public class Cheese : IKeyIngredient, IAddition
    {
        public SandwichKind Kind => SandwichKind.Cheese;
        public string Name => "cheese";
        public DateTimeOffset ExpirationDate => DateTimeOffset.Now.AddDays(10);
    }
}
