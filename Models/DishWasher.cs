using System;

namespace HomeController.Models
{
    public class DishWasher : IDevice
    {
        public int CurrentCycle { get; set; } // e.g., 0 = Idle, 1 = Wash, 2 = Rinse, 3 = Dry
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public int WaterLevel { get; set; } // Estimated water usage
        public int TotalCycles { get; set; } // Estimated energy usage

        public void StartCycle(int cycle)
        {
            CurrentCycle = cycle;
            StartTime = DateTime.Now;
            Console.WriteLine($"Dishwasher started on cycle {cycle}.");
        }

        public void StopCycle()
        {
            EndTime = DateTime.Now;
        }

        public void Reset()
        {
            CurrentCycle = 0;
            StartTime = null;
            EndTime = null;
            Console.WriteLine("Dishwasher has been reset.");
        }

        public override void TurnOn() { }

        public override void TurnOff() { }
    }
}
