using System;

namespace LevelUpCSharp.Products
{
    public class Mustard : ITopping
    {
        public string Name => "mustard";
        public DateTimeOffset ExpirationDate => DateTimeOffset.Now.AddDays(3);
    }
}