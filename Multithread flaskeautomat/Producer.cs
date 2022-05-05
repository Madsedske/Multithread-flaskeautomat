using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Multithread_flaskeautomat
{
    public class Producer
    {
        Random randomBottle = new Random();

        Queue<Bottle> producedBottles;

        public Producer(Queue<Bottle> produced)
        {
            producedBottles = produced;
        }

        public void ProducerThread()
        {
            // A int variable for the count on the bottlenumbers
            int bottleNum = 1;

            while (true)
            {
                Monitor.Enter(producedBottles);

                // The producer has a max capacity of 6 and will not make more bottles than that.
                if (producedBottles.Count < 6)
                {
                    for (int i = 0; i < 6; i++)
                    {
                        int whichBottle = randomBottle.Next(0, 7);

                        // Making a soda.
                        if (whichBottle < 3)
                        {
                            Bottle bottle = new Bottle("Soda ", bottleNum);
                            producedBottles.Enqueue(bottle);
                        }
                        // Making a beer.
                        else
                        {
                            Bottle bottle = new Bottle("Beer ", bottleNum);
                            producedBottles.Enqueue(bottle);
                        }
                        bottleNum++;
                    }
                }
                // A 'else if' if the capacity of 6 has been reached. 
                else if (producedBottles.Count == 6)
                    Debug.WriteLine("Producer venter..");
                Thread.Sleep(1000);
                Monitor.PulseAll(producedBottles);
                Monitor.Exit(producedBottles);
            }
        }
    }
}