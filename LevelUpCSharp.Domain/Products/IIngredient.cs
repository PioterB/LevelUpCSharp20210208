using System;

namespace LevelUpCSharp.Products
{
    public interface IIngredient
    {
        string Name { get; }
        DateTimeOffset ExpirationDate { get; }
    }
}