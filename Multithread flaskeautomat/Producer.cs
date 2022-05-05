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
            int bottleNum = 1;

            while (true)
            {
                Monitor.Enter(producedBottles);

                // Produceren laver maks 6 flasker
                if (producedBottles.Count < 6)
                {
                    for (int i = 0; i < 6; i++)
                    {
                        int whichBottle = randomBottle.Next(0, 7);

                        if (whichBottle < 3)
                        {
                            Bottle bottle = new Bottle("Soda ", bottleNum);
                            producedBottles.Enqueue(bottle);
                        }
                        else
                        {
                            Bottle bottle = new Bottle("Beer ", bottleNum);
                            producedBottles.Enqueue(bottle);
                        }
                        bottleNum++;
                    }
                }
                else if (producedBottles.Count == 6)
                    Debug.WriteLine("Producer venter..");
                Thread.Sleep(1000);
                Monitor.PulseAll(producedBottles);
                Monitor.Exit(producedBottles);
            }
        }
    }
}