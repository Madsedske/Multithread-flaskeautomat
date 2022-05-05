using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Multithread_flaskeautomat
{
    public class Consumer
    {
        Queue<Bottle> sodaBottles;
        Queue<Bottle> beerBottles;


        public Consumer(Queue<Bottle> producedSoda, Queue<Bottle> producedBeer)
        {
            sodaBottles = producedSoda;
            beerBottles = producedBeer;
        }

        public void SodaConsumer()
        {
            while (true)
            {
                lock (sodaBottles)
                {
                    while (sodaBottles.Count == 0)
                    {
                        Monitor.Wait(sodaBottles);
                    }
                    Bottle removedSoda = sodaBottles.Dequeue();
                    Thread.Sleep(2000);
                    Console.WriteLine("Consumer har consumeret " + removedSoda);
                }
            }
        }

        public void BeerConsumer()
        {
            while (true)
            {
                lock (beerBottles)
                {
                    while (beerBottles.Count == 0)
                    {
                        Monitor.Wait(beerBottles);
                    }
                    Bottle removedBeer = beerBottles.Dequeue();
                    Thread.Sleep(2000);
                    Console.WriteLine("Consumer har consumeret " + removedBeer);
                }
            }
        }
    }
}