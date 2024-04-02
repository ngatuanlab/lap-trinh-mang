using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace threadSample
{
    class threadSample
    {
        public static void Main(string[] args)
        {

            threadSample ts = new threadSample();

        }

        void counter()
        {
            int i;
            for (i=0; i<10; i++)
            {
                Console.WriteLine("Thread: {0}", i);
                Thread.Sleep(2000);
            }
        }

        void counter2()
        {
            int i;
            for (i=0; i<10; i++)
            {
                Console.WriteLine("Thread2: {0}", i);
                Thread.Sleep(3000);
            }
        }

        public threadSample()
        {
            int i;
            Thread newCounter = new Thread(new ThreadStart(counter));
            Thread newCounter2 = new Thread(new ThreadStart(counter2));

            newCounter.Start();
            newCounter2.Start();

            for (i = 0; i < 10; i++)
			{
			    Console.WriteLine("main: {0}", i);
                Thread.Sleep(1000);
			}
        }
    }
}
