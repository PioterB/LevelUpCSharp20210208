using System;

namespace LevelUpCSharp.Products
{
    public class KebapChicken : IKeyIngredient
    {
        public SandwichKind Kind => SandwichKind.Chicken;
        public string Name => "chicken kebap";
        public DateTimeOffset ExpirationDate => DateTimeOffset.Now.AddDays(10);
    }
}