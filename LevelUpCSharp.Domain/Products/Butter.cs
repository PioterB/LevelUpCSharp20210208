using System;

namespace LevelUpCSharp.Products
{
    public class Butter : IIngredient
    {
        public string Name { get; }
        public DateTimeOffset ExpirationDate { get; }
    }
}