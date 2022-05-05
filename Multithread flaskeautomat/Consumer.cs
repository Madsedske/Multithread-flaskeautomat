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
                // Lock for the sodaBottles
                lock (sodaBottles)
                {
                    // A while loop to check if sodaBottles contains a object.
                    while (sodaBottles.Count == 0)
                    {
                        // As long the sodaBottles is empty, the thread will wait.
                        Monitor.Wait(sodaBottles);
                    }

                    // Removing the latest soda from sodaBottles.
                    Bottle removedSoda = sodaBottles.Dequeue();
                    Thread.Sleep(2000);
                    Debug.WriteLine("Consumer har taget " + removedSoda);
                }
            }
        }

        public void BeerConsumer()
        {
            while (true)
            {
                // Lock for the beerBottles.
                lock (beerBottles)
                {
                    // A while loop to check if beerBottles contains a object.
                    while (beerBottles.Count == 0)
                    {
                        // As long the beerBottles is empty, the thread will wait.
                        Monitor.Wait(beerBottles);
                    }

                    // Removing the latest beer from beerBottles.
                    Bottle removedBeer = beerBottles.Dequeue();
                    Thread.Sleep(2000);
                    Debug.WriteLine("Consumer har taget " + removedBeer);
                }
            }
        }
    }
}