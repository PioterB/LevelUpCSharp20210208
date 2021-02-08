namespace LevelUpCSharp.Products
{
    public interface IAdditionable : IToppingable, ISandwichBuilder
    {
        IAdditionable AddAddition(IAddition addition);
    }
}