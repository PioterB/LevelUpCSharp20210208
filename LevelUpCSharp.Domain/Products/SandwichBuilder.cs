using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LevelUpCSharp.Products
{
    public class SandwichBuilder : IStarter, IAdditionable, IToppingable, ISandwichBuilder
    {
        private SandwichKind _type;

        private readonly List<IIngredient> _ingredients = new List<IIngredient>(4);
        private bool _hasButter;

        public Sandwich Wrap()
        {
            return new Sandwich(_type, DateTimeOffset.Now.AddHours(3), _ingredients.AsStrings().ToArray());
        }

        public IToppingable AddTopping(ITopping topping)
        {
            _ingredients.Add(topping);
            return this;
        }

        public IAdditionable AddAddition(IAddition ingredient)
        {
            _ingredients.Add(ingredient);
            return this;
        }

        public IAdditionable Add(IKeyIngredient ingredient)
        {
            _type = ingredient.Kind;
            _ingredients.Add(ingredient);
            return this;
        }

        public IAdditionable AddButter()
        {
            if (_hasButter)
            {
                return this;
            }

            _hasButter = true;
            _ingredients.Add(new Butter());

            return this;
        }

        public string Name { get; }
        public DateTimeOffset ExpirationDate { get; }
    }

    public static class IngredientsSetExtensions
    {
        public static IEnumerable<string> AsStrings(this List<IIngredient> source)
        {
            foreach (IIngredient ingredient in source)
            {
                yield return ingredient.Name;
            }
        }
    }
}
