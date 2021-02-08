using System;

namespace LevelUpCSharp.Products
{
    public class GrilledChicken : IKeyIngredient
    {
        public SandwichKind Kind => SandwichKind.Chicken;
        public string Name => "grilled chicken";
        public DateTimeOffset ExpirationDate => DateTimeOffset.Now.AddDays(10);
    }
}