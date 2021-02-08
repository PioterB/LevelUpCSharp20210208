using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using LevelUpCSharp.Collections;
using LevelUpCSharp.Products;

namespace LevelUpCSharp.Retail
{
    public class SandwichesStore : ISandwichesQueue, IEnumerable<Sandwich>
    {
        private int _totalCount = 0;
        private readonly IDictionary<SandwichKind, Queue<Sandwich>> _lines = new Dictionary<SandwichKind, Queue<Sandwich>>();
        public IEnumerator<Sandwich> GetEnumerator()
        {
            List<Sandwich> items = new List<Sandwich>();
            _lines.ForEach(pair => items.AddRange(pair.Value));

            return new SandwichesStore.Enumerator(items);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public Result<Sandwich> this[SandwichKind kind] => Grab(kind);

        public Result<Sandwich> Grab(SandwichKind kind)
        {
            if (_lines.ContainsKey(kind) ||  _lines[kind].Count == 0)
            {
                return Result<Sandwich>.Failed();
            }

            _totalCount--;
            return Result<Sandwich>.Success(_lines[kind].Dequeue());
        }

        public void Put(Sandwich item)
        {
            _totalCount++;
            _lines[item.Kind].Enqueue(item);
        }

        public int Count(SandwichKind kind)
        {
            return _lines[kind].Count;
        }

        public int Count()
        {
            return _totalCount;
        }

        private class Enumerator : IEnumerator<Sandwich>
        {
            private readonly List<Sandwich> _items;
            private readonly IEnumerator<Sandwich> _enumerator;

            public Enumerator(List<Sandwich> items)
            {
                SortByExpirationDate(items);
                _items = items;
                _enumerator = _items.GetEnumerator();
            }

            public bool MoveNext()
            {
                return _enumerator.MoveNext();
            }

            public void Reset()
            {
                throw new NotSupportedException();
            }

            public Sandwich Current => _enumerator.Current;

            object IEnumerator.Current => Current;

            public void Dispose()
            {
                _enumerator.Dispose();
            }

            private void SortByExpirationDate(List<Sandwich> items)
            {
                items.Sort((x, y) => x.ExpirationDate.CompareTo(y.ExpirationDate));
            }
        }
    }

    public interface ISandwichesQueue: IEnumerable<Sandwich>
    {
        Result<Sandwich> this[SandwichKind kind] { get; }

        Result<Sandwich> Grab(SandwichKind kind);

        void Put(Sandwich item);

        int Count(SandwichKind kind);

        int Count();
    }
}
