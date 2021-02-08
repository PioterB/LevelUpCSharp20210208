namespace LevelUpCSharp.Products
{
    public interface IKeyIngredient : IIngredient
    {
        SandwichKind Kind { get; }
    }
}