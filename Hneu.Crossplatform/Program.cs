using System;
using System.Threading;

namespace LR1KovalenkoKrasnyuk
{
    class Program
    {
        public static Random rand = new Random();
        static void Main(string[] args)
        {
            Console.SetWindowSize(120, 44);

            Port.PrintPortInfo();

            for (int i = 1; i < 6; i++)
            {
                new Thread(Port.LoadContainers).Start(new CargoShip(i));
                new Thread(Port.UnLoadContainers).Start(new CargoShip(i+5));
            }
            Console.ReadKey();
        }
    }
}
