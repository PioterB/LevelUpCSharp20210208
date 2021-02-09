using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using LevelUpCSharp.Collections;
using LevelUpCSharp.Helpers;
using LevelUpCSharp.Products;

namespace LevelUpCSharp.Retail
{
    public class Retailer
    {
        private static Retailer _instance;
        // private readonly IDictionary<SandwichKind, Queue<Sandwich>> _lines;
        private readonly SandwichesStore _lines;

        protected Retailer(string name)
        {
            Name = name;
            _lines = new SandwichesStore();
        }

        public static Retailer Instance => _instance ?? (_instance = new Retailer("Build-in"));

        public event Action<PackingSummary> Packed;
        public event Action<DateTimeOffset, Sandwich> Purchase;

        public string Name { get; }

        public Task<Result<Sandwich>> Sell(SandwichKind kind)
        {
            var sandwich =  _lines[kind];

            OnPurchase(DateTimeOffset.Now, sandwich);
            return Task.Factory.StartNew<Result<Sandwich>>(() => _lines[kind]);
        }

        public async void Pack(IEnumerable<Sandwich> package, string deliver)
        {
            var summary = await Task.Factory.StartNew<PackingSummary>(() =>
            {
                package.ForEach(sandwich => _lines.Put(sandwich));
                // or in a old way without lamda expression: package.ForEach(_lines.Put);

                var positions = package
                    .GroupBy(p => p.Kind)
                    .Select(g => new LineSummary(g.Key, g.Count()))
                    .ToArray();

                return new PackingSummary(positions, deliver);
            });

            OnPacked(summary);
        }

        protected virtual void OnPacked(PackingSummary summary)
        {
            Packed?.Invoke(summary);
        }

        protected virtual void OnPurchase(DateTimeOffset time, Sandwich product)
        {
            Purchase?.Invoke(time, product);
        }

        private IDictionary<SandwichKind, Queue<Sandwich>> InitializeLines()
        {
            var result = new Dictionary<SandwichKind, Queue<Sandwich>>();

            foreach (var sandwichKind in EnumHelper.GetValues<SandwichKind>())
            {
                result.Add(sandwichKind, new Queue<Sandwich>());
            }

            return result;
        }

        private void LoopSandwiches()
        {
            foreach (var sandwich in _lines)
            {
                
            }

            var list = new List<Sandwich>(_lines);
        }
    }
}
