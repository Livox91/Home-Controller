namespace HomeController.Models
{
    public class AirCooler : IDevice
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsOn { get; set; }
        public int FanSpeed { get; set; } // 1 to 5
        public double Temperature { get; set; } // in Celsius
        public bool IsConnected { get; set; } // IoT connectivity status
        public string Ipaddr { get; set; } // Device IP address

        override public void TurnOn()
        {
            IsOn = true;
            Console.WriteLine($"{Name} is turned on.");
        }

        override public void TurnOff()
        {
            IsOn = false;
            Console.WriteLine($"{Name} is turned off.");
        }

        public void SetFanSpeed(int speed)
        {
            if (speed < 1 || speed > 5)
            {
                Console.WriteLine("Invalid fan speed. Please set a value between 1 and 5.");
                return;
            }
            FanSpeed = speed;
            Console.WriteLine($"Fan speed set to {FanSpeed}.");
        }

        public void SetTemperature(double temperature)
        {
            Temperature = temperature;
            Console.WriteLine($"Temperature set to {Temperature}°C.");
        }

        public void ConnectToNetwork()
        {
            IsConnected = true;
            Console.WriteLine($"{Name} is connected to the network.");
        }

        public void DisconnectFromNetwork()
        {
            IsConnected = false;
            Console.WriteLine($"{Name} is disconnected from the network.");
        }
    }
}