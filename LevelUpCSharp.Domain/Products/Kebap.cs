using System;

namespace LevelUpCSharp.Products
{
    public class Kebap : IKeyIngredient
    {
        public SandwichKind Kind => SandwichKind.Pork;
        public string Name => "kebap";
        public DateTimeOffset ExpirationDate => DateTimeOffset.Now.AddDays(10);
    }
}