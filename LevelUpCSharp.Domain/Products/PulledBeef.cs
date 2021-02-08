using System;

namespace LevelUpCSharp.Products
{
    public class PulledBeef : IKeyIngredient
    {
        public SandwichKind Kind => SandwichKind.Beef;
        public string Name => "pullled beef";
        public DateTimeOffset ExpirationDate => DateTimeOffset.Now.AddDays(10);
    }
}