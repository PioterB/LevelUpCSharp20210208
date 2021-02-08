using System;

namespace LevelUpCSharp.Products
{
    public class Ketchup : ITopping
    {
        public string Name => "Ketchup";
        public DateTimeOffset ExpirationDate => DateTimeOffset.Now.AddDays(3);
    }

    public class Mayo : ITopping
    {
        public string Name => "Mayo";
        public DateTimeOffset ExpirationDate => DateTimeOffset.Now.AddDays(3);
    }
}