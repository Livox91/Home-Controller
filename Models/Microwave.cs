using System;

namespace HomeController.Models
{
    public class Microwave : IDevice
    {
        public string Id { get; set; }
        public string Model { get; set; }
        public bool IsOn { get; private set; }
        public int PowerLevel { get; private set; }
        public TimeSpan Timer { get; private set; }
        public string Ipaddr { get; set; }
        public Microwave(string id, string model)
        {
            Id = id;
            Model = model;
            IsOn = false;
            PowerLevel = 5; // Default power level
            Timer = TimeSpan.Zero;
        }

        public Microwave()
        {
        }

        override public void TurnOn()
        {
            if (!IsOn)
            {
                IsOn = true;
                Console.WriteLine("Microwave is now ON.");
            }
        }

        override public void TurnOff()
        {
            if (IsOn)
            {
                IsOn = false;
                Timer = TimeSpan.Zero;
                Console.WriteLine("Microwave is now OFF.");
            }
        }

        public void SetPowerLevel(int level)
        {
            if (level < 1 || level > 10)
                throw new ArgumentOutOfRangeException(nameof(level), "Power level must be between 1 and 10.");

            PowerLevel = level;
            Console.WriteLine($"Power level set to {PowerLevel}.");
        }

        public void SetTimer(TimeSpan time)
        {
            if (time.TotalSeconds <= 0)
                throw new ArgumentOutOfRangeException(nameof(time), "Timer must be greater than zero.");

            Timer = time;
            Console.WriteLine($"Timer set to {Timer.TotalSeconds} seconds.");
        }

        public void Start()
        {
            if (!IsOn)
                throw new InvalidOperationException("Microwave must be turned on before starting.");

            if (Timer == TimeSpan.Zero)
                throw new InvalidOperationException("Timer must be set before starting.");

            Console.WriteLine($"Microwave started for {Timer.TotalSeconds} seconds at power level {PowerLevel}.");
            // Simulate cooking process (in a real IoT scenario, this would trigger hardware operations)
        }
    }
}