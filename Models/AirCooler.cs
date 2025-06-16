namespace HomeController.Models
{
    public class AirCooler : IDevice
    {
        public int FanSpeed { get; set; } // 1 to 5
        public int Temperature { get; set; } // in Celsius

        public override void TurnOn() { }

        public override void TurnOff() { }

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

        public void SetTemperature(int temperature)
        {
            Temperature = temperature;
            Console.WriteLine($"Temperature set to {Temperature}°C.");
        }
    }
}
