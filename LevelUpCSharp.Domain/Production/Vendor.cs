using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LevelUpCSharp.Collections;
using LevelUpCSharp.Products;

namespace LevelUpCSharp.Production
{
    public class Vendor
    {
        private Task _worker;
        private readonly ConcurrentQueue<Sandwich> _warehouse = new ConcurrentQueue<Sandwich>();
        private bool _upAndRunning = true;
        private readonly ConcurrentQueue<ProductionRequest> _requests = new ConcurrentQueue<ProductionRequest>();

        public Vendor(string name)
        {
            Name = name;

            _worker = new Task(Production, TaskCreationOptions.LongRunning);
            _worker.Start();
        }

        public event Action<Sandwich[]> Produced;

        public string Name { get; }

        public IEnumerable<Sandwich> Buy(int howMuch = 0)
        {
            var toSell = new List<Sandwich>();
            for (int i = 0; i < howMuch; i++)
            {
                Sandwich taken;
                var wasFound = _warehouse.TryDequeue(out taken);
                if (wasFound == false)
                {
                    break;
                }

                toSell.Add(taken);
            }

            return toSell;
        }

        public void Order(SandwichKind kind, int count)
        {
            _requests.Enqueue(new ProductionRequest(kind, count));

            Console.WriteLine("[V: {0}] new order waits in queue  {1} - {2}", Name, kind, count);


            //var sandwiches = new List<Sandwich>();
            //for (int i = 0; i < count; i++)
            //{
            //    sandwiches.Add(Produce(kind));
            //}
            //_warehouse.AddRange(sandwiches);
            //Produced?.Invoke(sandwiches.ToArray());
        }

        public IEnumerable<StockItem> GetStock()
        {
            Dictionary<SandwichKind, int> counts = new Dictionary<SandwichKind, int>()
            {
                {SandwichKind.Cheese, 0},
                {SandwichKind.Chicken, 0},
                {SandwichKind.Beef, 0},
                {SandwichKind.Pork, 0},
            };

            foreach (var sandwich in _warehouse.ToArray())
            {
                counts[sandwich.Kind] += 1;
            }

            var result = new StockItem[counts.Count];

            int i = 0;
            foreach (var count in counts)
            {
                result[i] = new StockItem(count.Key, count.Value);
                i++;
            }

            return result;
        }

        public void Shutdown()
        {
            Console.WriteLine("[V: {0}] closing...", Name);
            lock (this)
            {
                _upAndRunning = false;
            }
        }

        private void Production()
        {
            var randomGenerator = new Random();

            bool isRunning = false;
            lock (this)
            {
                isRunning = _upAndRunning;
            }

            int round = 0;

            while (isRunning)
            {
                var type = (SandwichKind)randomGenerator.Next(1, 4);
                var producer = Task.Delay(TimeSpan.FromSeconds(2)).ContinueWith(_ => DoBatch(new ProductionRequest(type, 1)));

                if (round == 0)
                {
                    ProductionRequest request;
                    if (_requests.TryDequeue(out request))
                    {
                        var orderExecutor = Task.Factory.StartNew(() => DoBatch(request));

                        var fromOrder = orderExecutor.Result;
                        fromOrder.ForEach(s => _warehouse.Enqueue(s));
                    }
                }

                var sandwiches = producer.Result;
                sandwiches.ForEach(s => _warehouse.Enqueue(s));

                lock (this)
                {
                    isRunning = _upAndRunning;
                }

                round = (round + 1) % 3;

                Thread.Sleep(TimeSpan.FromSeconds(1));
            }
        }

        private IEnumerable<Sandwich> DoBatch(ProductionRequest productionRequest)
        {
            List<Sandwich> result = new List<Sandwich>(productionRequest.Count);
            for (int i = 0; i < productionRequest.Count; i++)
            {
                var requested = Produce(productionRequest.Kind);
                result.Add(requested);
            }

            Console.WriteLine("[V: {0}] Batch made: {1} of {2}", Name, productionRequest.Count, productionRequest.Kind);

            return result;
        }

        private Sandwich Produce(SandwichKind kind)
        {
            var result = kind switch
            {
                SandwichKind.Beef => new SandwichBuilder()
                    .Add(new PulledBeef())
                    .AddAddition(new Lettuce())
                    .AddAddition(new Onion())
                    .AddAddition(new Vegetable("carrot"))
                    .AddAddition(new Vegetable("perslay"))
                    .AddAddition(new Cheese())
                    .AddTopping(new Mustard())
                    .AddTopping(new Ketchup())
                    .Wrap(),

                SandwichKind.Cheese =>  new SandwichBuilder()
                    .Add(new Cheese())
                    .AddAddition(new Cheese())
                    .AddTopping(new Mustard())
                    .Wrap(),//ProduceSandwich(kind, DateTimeOffset.Now.AddSeconds(90)),
                SandwichKind.Chicken => ProduceSandwich(kind, DateTimeOffset.Now.AddMinutes(4)),
                SandwichKind.Pork => ProduceSandwich(kind, DateTimeOffset.Now.AddSeconds(150)),
                _ => throw new ArgumentOutOfRangeException(nameof(kind), kind, null)
            };

            // Console.WriteLine("[V: {0}] sandwich {1} made ", Name, result.Kind);

            return result;
        }

        private Sandwich ProduceSandwich(SandwichKind kind, DateTimeOffset addMinutes)
        {
            return new Sandwich(kind, addMinutes);
        }

        private class ProductionRequest
        {
            public SandwichKind Kind { get; }
            public int Count { get; }

            public ProductionRequest(SandwichKind kind, int count)
            {
                Kind = kind;
                Count = count;
            }
        }
    }

    internal class Vegetable : IAddition
    {
        public Vegetable(string name)
        {
            Name = name;
        }

        public string Name { get; }

        public DateTimeOffset ExpirationDate { get; }
    }
}
