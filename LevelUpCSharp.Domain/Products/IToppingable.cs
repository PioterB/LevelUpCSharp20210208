namespace LevelUpCSharp.Products
{
    public interface IToppingable: ISandwichBuilder
    {
        IToppingable AddTopping(ITopping topping);
    }
}