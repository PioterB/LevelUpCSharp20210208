using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.WebSockets;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using LevelUpCSharp.Production;
using LevelUpCSharp.Products;
using LevelUpCSharp.Retail;

namespace LevelUpCSharp.CommandLine
{
    class Program
    {
        static void Main(string[] args)
        {
            var vendor = new Vendor("Piotrek");

            ConsoleKeyInfo key;
            do
            {
                key = Console.ReadKey(true);
                switch (key.Key)
                {
                    case ConsoleKey.O: vendor.Order(SandwichKind.Beef, 2); break;
                }

            } while (key.Key != ConsoleKey.B);


            vendor.Shutdown();

            //Tasks();

            Console.ReadKey(true);

            //LinqTest();

            //int myNumber = 5;

            //int mySecondNuymbner = 4;

            //if (myNumber % 2 == 1)
            //{
            //    /* react on odd */
            //}

            //if (MyExtenstion.IsOddOldWay(myNumber))
            //{
            //    /* react on odd */
            //}

            //if (myNumber.IsOdd())
            //{
            //    /* react on odd */
            //}

            //if (myNumber % 2 == 1)
            //{
            //}

            //mySecondNuymbner.IsOdd();

            //var sandwiches = new List<Sandwich>();
            //sandwiches
            //    .Where(sandwich => sandwich.ExpirationDate > DateTime.Now.AddDays(1))
            //    .Where(sandwich => sandwich.IngredientsCount > 2)
            //    .ToList()
            //    .Count()
            //    .IsOdd()
            //    .Equals(true);
        }

        private static void Vending()
        {
            var v = new Vendor("Piotrek");
        }

        private static void LinqTest()
        {
            var retailer = Retailer.Instance;
            var package = new List<Sandwich>()
            {
                new Sandwich(SandwichKind.Pork, DateTime.Now, "cheese"),
                new Sandwich(SandwichKind.Cheese, DateTime.Now, "cheese"),
                new Sandwich(SandwichKind.Chicken, DateTime.Now, "cheese"),
                new Sandwich(SandwichKind.Pork, DateTime.Now, "cheese"),
            };

            retailer.Packed += summary => Console.WriteLine(summary.Positions.Count());


            retailer.Pack(package, "my");

        }

        private static void Threads()
        {
            var address = new Address() {Street = "s1", Flat = "f1", Number = "n1"};
            var address2 = new Address() {Street = "s1", Flat = "f1", Number = "n1"};

            Semaphore s = new Semaphore(0, 1);


            Thread t1 = new Thread(o =>
            {
                s.WaitOne();
                UpdateValues(o);
                s.Release();

            });

            Thread t2 = new Thread(o => {
                s.WaitOne();
                UpdateValues(o);
                s.Release();

            });

            Thread t3 = new Thread(o => UpdateValues(o) );
            Thread t4 = new Thread(o => UpdateValues(o) );

            Console.WriteLine("Lets play, release threads");
            s.Release();


            t1.Start(address);

            ThreadPool.QueueUserWorkItem(o => UpdateValues(o), address);
            
            t2.Start(address);
            t3.Start(address);
            t4.Start(address);



            t1.Join();
            t2.Join();

            Console.WriteLine("Modified address: " + address);
        }

        private static void Tasks()
        {
            var address = new Address() { Street = "s1", Flat = "f1", Number = "n1" };
            var address2 = new Address() { Street = "s1", Flat = "f1", Number = "n1" };

            Console.WriteLine("Oryginal address: " + address.ToString());

            var taskWithResult = new Task<Address>(UpdateByTask, address);
            var task1 = new Task(UpdateValues, address);
            task1.Start();

            var taskFromFactoryWithoutResult = Task.Factory.StartNew(UpdateValues, address);
            var taskFromFactoryWithResult = Task.Factory.StartNew<Address>(UpdateByTask, address);

            taskWithResult.Start();
            Console.WriteLine("before result read");

            var changes = taskWithResult.Result;
            Console.WriteLine("Oryginal address: " + changes.ToString());
            Console.WriteLine("Oryginal address: " + changes.ToString());
            Console.WriteLine("Oryginal address: " + address.ToString());

        }

        private static void UpdateValues(object obj)
        {
            var id = Thread.CurrentThread.ManagedThreadId;
            var adr = (Address)obj;
            Console.WriteLine(id + " Oryginal address: " + obj.ToString());

            lock (adr)
            {
                Console.WriteLine(id + " changing street");
                adr.Street += string.Format("[{0}]", id);
                Console.WriteLine(id + " changing number");
                adr.Number += string.Format("[{0}]", id);

                Thread.Sleep(100);

                Console.WriteLine(id + " changing flat");
                adr.Flat += string.Format("[{0}]", id);
            }
        }

        private static Address UpdateByTask(object obj)
        {
            var id = Thread.CurrentThread.ManagedThreadId;

            var adr = (Address)obj;
            return new Address()
            {
                Street = adr.Street + string.Format("[{0}]", id),
                Number = adr.Number + string.Format("[{0}]", id),
                Flat = adr.Flat + string.Format("[{0}]", id),
            };
        }
    }

    static class MyExtenstion
    {
        public static bool IsOdd(this int source)
        {
            return source % 2 == 1;
        }

        public static bool IsOddOldWay(int source)
        {
            return source % 2 == 1;
        }
    }

    public class Address
    {
        public string Street { get; set; }

        public string Number { get; set; }

        public string Flat { get; set; }

        public override string ToString()
        {
            return string.Format("Address: {0}, {1}, {2}", Street, Number, Flat);
        }
    }
}
