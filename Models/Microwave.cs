using System;

namespace HomeController.Models
{
    public class Microwave : IDevice
    {
        public TimeSpan Timer { get; set; }

        public override void TurnOn() { }

        public override void TurnOff() { }

        public void SetTimer(TimeSpan time)
        {
            if (time.TotalSeconds <= 0)
                throw new ArgumentOutOfRangeException(
                    nameof(time),
                    "Timer must be greater than zero."
                );

            Timer = time;
            Console.WriteLine($"Timer set to {Timer.TotalSeconds} seconds.");
        }
    }
}
