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
                lock (producedBottles)
                {
                    while (producedBottles.Count == 0)
                    {
                        Monitor.Wait(producedBottles);
                    }

                    for (int i = 0; i < producedBottles.Count; i++)
                    {
                        string checkBottle = producedBottles.Peek().BottleName;

                        if (checkBottle == "Soda ")
                        {
                            Bottle moveSoda = producedBottles.Dequeue();
                            lock (sodaBottles)
                            {
                                sodaBottles.Enqueue(moveSoda);
                                Debug.WriteLine("Fordelt en " + moveSoda);
                                Monitor.PulseAll(sodaBottles);
                            }
                            Monitor.PulseAll(producedBottles);
                        }
                        else
                        {
                            Bottle moveBeer = producedBottles.Dequeue();
                            lock (beerBottles)
                            {
                                beerBottles.Enqueue(moveBeer);
                                Debug.WriteLine("Fordelt en " + moveBeer);
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