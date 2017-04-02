using System;
using System.Threading;

namespace LR1KovalenkoKrasnyuk
{
    static class Port
    {
        static int conteinersInPort = 200;
        static readonly int maxCountOfContainers = 250;
        static int availableSpace { get { return maxCountOfContainers - conteinersInPort; } }

        // total of 8 berths. 2 of them work
        static SemaphoreSlim semaphore = new SemaphoreSlim(2, 8);

        public static void LoadContainers(object _ship)
        {
            semaphore.Wait();
            Thread.Sleep(800);

                if (conteinersInPort == 0)
                {
                    Console.WriteLine("Port is empty!\n");
                    semaphore.Release();
                    return;
                }

            var ship = _ship as Ship;

            Console.WriteLine("Ship number {0} arrived for download. Available for download {1} \n", ship.ShipId, ship.AvailableCapacity);

            if (ship.AvailableCapacity == 0)
            {
                Console.WriteLine("Ship {0} is full. I'm starting to unload. \n", ship.ShipId);
                UnLoadContainers(ship);
                semaphore.Release();
                return;
            }

            if (ship.AvailableCapacity >= conteinersInPort)
            {
                ship.CountOfContainers += conteinersInPort;
                Console.WriteLine("The {0} ship was loaded with {1} containers. The remaining port is 0 \n", ship.ShipId, conteinersInPort);
                conteinersInPort = 0;
                semaphore.Release();
                return;
            }

            conteinersInPort -= ship.AvailableCapacity;
            Console.WriteLine("The {0} ship was loaded with {1} containers. {2} remaining in the port \n", ship.ShipId, ship.AvailableCapacity, conteinersInPort);
            ship.CountOfContainers += ship.AvailableCapacity;

            semaphore.Release();
        }

        public static void UnLoadContainers(object _ship)
        {
            semaphore.Wait();
            Thread.Sleep(600);

            if (availableSpace == 0)
            {
                Console.WriteLine("The port is full! \n");
                semaphore.Release();
                return;
            }

            var ship = _ship as Ship;

            Console.WriteLine("Ship number {0} arrived for unloading. Containers for unloading - {1}. Free space in the port - {2} \n"
                , ship.ShipId, ship.CountOfContainers, availableSpace);

            if (availableSpace >= ship.CountOfContainers)
            {
                conteinersInPort += ship.CountOfContainers;
                Console.WriteLine("Ship number {0} unloaded {1} containers \n", ship.ShipId, ship.CountOfContainers);
                ship.CountOfContainers = 0;

                semaphore.Release();
                return;
            }

            ship.CountOfContainers -= availableSpace;
            Console.WriteLine("Ship number {0} unloaded {1} containers\n", ship.ShipId, availableSpace);

            conteinersInPort = maxCountOfContainers;

            semaphore.Release();
        }

        public static void PrintPortInfo()
        {
            Console.WriteLine("Currently containers: " + conteinersInPort);
            Console.WriteLine("Maximum capacity of the port: " + maxCountOfContainers);
            Console.WriteLine("Working berths: " + semaphore.CurrentCount);
            Console.WriteLine(new string('-', 20));
        }
    }
}
