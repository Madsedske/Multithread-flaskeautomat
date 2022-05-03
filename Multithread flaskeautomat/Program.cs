using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Multithread_flaskeautomat
{
    internal class Program
    {
        static int bottleNumber { get; set; }
        static Queue<string> bottles = new Queue<string>();

        static Random random = new Random();

        static Queue<string> soda = new Queue<string>();
        static Queue<string> beer = new Queue<string>();

        static void Main(string[] args)
        {
            Thread producer = new Thread(Producer);

            Thread splitter = new Thread(SplitterCon);

            Thread sodaConsumer = new Thread(SodaConsumer);
            Thread beerConsumer = new Thread(BeerConsumer);

            producer.Start();
            splitter.Start();

            beerConsumer.Start();
            sodaConsumer.Start();

            producer.Join();
            splitter.Join();

            sodaConsumer.Join();
            beerConsumer.Join();

        }

        static void Producer()
        {
            while (true)
            {
                Monitor.Enter(bottles);

                if (bottles.Count < 6)
                {
                    for (int i = 0; i < 6; i++)
                    {
                        int whichBottle = random.Next(0, 7);

                        if (whichBottle < 3)
                        {
                            bottles.Enqueue("Soda");
                            Console.WriteLine("produceret en soda " + bottles.Count);

                        }
                        else
                        {
                            bottles.Enqueue("Beer");
                            Console.WriteLine("produceret en beer " + bottles.Count);

                        }
                    }
                }
                else if (bottles.Count == 6)
                    Console.WriteLine("Producer venter..");
                Thread.Sleep(5000);
                Monitor.PulseAll(bottles);
                Monitor.Exit(bottles);
            }
        }

        static void SplitterCon()
        {
            while (true)
            {
                Monitor.Enter(bottles);
                while (bottles.Count == 0)
                {
                    Monitor.Wait(bottles);
                }
                Monitor.Exit(bottles);


                if (bottles.Contains("Soda"))
                {
                    Monitor.Enter(soda);

                    bottles.Dequeue();

                    soda.Enqueue("Soda");
                    Console.WriteLine("Fordelt en soda");
                    Monitor.PulseAll(soda);
                    Monitor.Exit(soda);
                }
                else if (bottles.Contains("Beer"))
                {
                    Monitor.Enter(soda);

                    bottles.Dequeue();
                    
                    beer.Enqueue("Beer");
                    Console.WriteLine("Fordelt en beer");
                    Monitor.PulseAll(beer);
                    Monitor.Exit(beer);
                }
            }
        }


        static void SodaConsumer()
        {
            while (true)
            {
                Monitor.Enter(soda);
                while (soda.Count == 0)
                {
                    Monitor.Wait(soda);
                }
                soda.Dequeue();
                Console.WriteLine("Consumer har consumeret en soda " + " - Queue count is " + soda.Count);

                Monitor.Exit(soda);
            }
        }

        static void BeerConsumer()
        {
            while (true)
            {
                Monitor.Enter(beer);
                while (beer.Count == 0)
                {
                    Monitor.Wait(beer);
                }
                beer.Dequeue();
                Console.WriteLine("Consumer har consumeret en beer " + " - Queue count is " + beer.Count);

                Monitor.Exit(beer);
            }
        }

    }
}
