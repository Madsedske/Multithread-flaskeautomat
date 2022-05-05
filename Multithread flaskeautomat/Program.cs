using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Multithread_flaskeautomat
{
    public class Program
    {
        static Queue<Bottle> producedBottles  = new Queue<Bottle>();
        static Queue<Bottle> sodaBottles = new Queue<Bottle>();
        static Queue<Bottle> beerBottles = new Queue<Bottle>();

        static void Main(string[] args)
        {
            Producer produced = new Producer(producedBottles);
            Splitter splitter = new Splitter(producedBottles, sodaBottles, beerBottles);
            Consumer consumer = new Consumer(sodaBottles, beerBottles);

            Thread threadProducer = new Thread(produced.ProducerThread);
            Thread threadSplitter = new Thread(splitter.SplitterCon);
            Thread threadSodaConsumer = new Thread(consumer.SodaConsumer);
            Thread threadBeerConsumer = new Thread(consumer.BeerConsumer);

            threadProducer.Start();
            threadSplitter.Start();
            threadBeerConsumer.Start();
            threadSodaConsumer.Start();

            threadProducer.Join();
            threadSplitter.Join();
            threadSodaConsumer.Join();
            threadBeerConsumer.Join();

            Console.ReadKey();
        }
    }
}