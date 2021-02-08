namespace LevelUpCSharp.Products
{
    public interface IStarter : IAdditionable, IToppingable, ISandwichBuilder
    {
        IAdditionable Add(IKeyIngredient ingredient);
    }
}