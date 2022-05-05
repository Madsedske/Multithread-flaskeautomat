using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Multithread_flaskeautomat
{
    public class Splitter
    {
        Queue<Bottle> producedBottles;
        Queue<Bottle> beerBottles;
        Queue<Bottle> sodaBottles;

        public Splitter(Queue<Bottle> produced, Queue<Bottle> producedSoda, Queue<Bottle> producedBeer)
        {
            producedBottles = produced;
            sodaBottles = producedSoda;
            beerBottles = producedBeer;
        }

        public void SplitterCon()
        {
            while (true)
            {
                // Lock for the producedBottles.
                lock (producedBottles)
                {
                    // A while loop to check if producedBottles contains a object.
                    while (producedBottles.Count == 0)
                    {
                        // As long the producedBottles is empty, the thread will wait.
                        Monitor.Wait(producedBottles);
                    }

                    // Looping as long as the producedBottles i filled.
                    for (int i = 0; i < producedBottles.Count; i++)
                    {
                        // Get the name for the latest bottle in producedBottle queue.
                        string checkBottle = producedBottles.Peek().BottleName;

                        // Checking if the latest item is a soda. Going to else if its a beer.
                        if (checkBottle == "Soda ")
                        {
                            // Moving the removed item in a variable.
                            Bottle moveSoda = producedBottles.Dequeue();
                            lock (sodaBottles)
                            {
                                // Fill the the sodaBottles queue with the removed item.
                                sodaBottles.Enqueue(moveSoda);
                                Debug.WriteLine("Fordelt " + moveSoda);
                                Monitor.PulseAll(sodaBottles);
                            }
                            Monitor.PulseAll(producedBottles);
                        }
                        else
                        {
                            // Moving the removed item in a variable.
                            Bottle moveBeer = producedBottles.Dequeue();
                            lock (beerBottles)
                            {
                                // Fill the the beerBottles queue with the removed item.
                                beerBottles.Enqueue(moveBeer);
                                Debug.WriteLine("Fordelt " + moveBeer);
                                Monitor.PulseAll(beerBottles);
                            }
                            Monitor.PulseAll(producedBottles);
                        }
                    }
                }
            }
        }
    }
}