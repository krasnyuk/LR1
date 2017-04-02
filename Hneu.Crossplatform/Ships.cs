namespace LR1KovalenkoKrasnyuk
{
    abstract class Ship
    { 
        public int Carrying { get; }
        public int CountOfContainers { get; set; }

        public int ShipId { get; }
        public int AvailableCapacity { get { return Carrying - CountOfContainers; } }

        public Ship(int numberOfShip, int bearingCapacity)
        {
            ShipId = numberOfShip;
            Carrying = bearingCapacity;
        }
    }

    class LightShip : Ship
    {
        public LightShip(int numberOfShip, int capacityOfShip = 100) : base(numberOfShip, capacityOfShip)
        {
            this.CountOfContainers = Program.rand.Next(0, capacityOfShip);
        }
    }

    class CargoShip : Ship
    {
        public CargoShip(int numberOfShip, int capacityOfShip = 200) : base(numberOfShip, capacityOfShip)
        {    
            this.CountOfContainers = Program.rand.Next(0, capacityOfShip);
        }
    }
}
