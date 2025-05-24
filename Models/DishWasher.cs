using System;

namespace HomeController.Models
{
    public class DishWasher : IDevice
    {
        public int Id { get; set; }
        public string ModelName { get; set; }
        public bool IsRunning { get; set; }
        public int CurrentCycle { get; set; } // e.g., 0 = Idle, 1 = Wash, 2 = Rinse, 3 = Dry
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public int WaterUsageLiters { get; set; } // Estimated water usage
        public int EnergyUsageWatts { get; set; } // Estimated energy usage

        public string Ipaddr { get; set; }
        public void StartCycle(int cycle)
        {
            if (!IsRunning)
            {
                IsRunning = true;
                CurrentCycle = cycle;
                StartTime = DateTime.Now;
                Console.WriteLine($"Dishwasher started on cycle {cycle}.");
            }
            else
            {
                Console.WriteLine("Dishwasher is already running.");
            }
        }

        public void StopCycle()
        {
            if (IsRunning)
            {
                IsRunning = false;
                EndTime = DateTime.Now;
                Console.WriteLine("Dishwasher cycle stopped.");
            }
            else
            {
                Console.WriteLine("Dishwasher is not running.");
            }
        }

        public void Reset()
        {
            IsRunning = false;
            CurrentCycle = 0;
            StartTime = null;
            EndTime = null;
            Console.WriteLine("Dishwasher has been reset.");
        }

        override public void TurnOn()
        {
            if (!IsRunning)
            {
                IsRunning = true;
                Console.WriteLine("Dishwasher turned on.");
            }
            else
            {
                Console.WriteLine("Dishwasher is already on.");
            }
        }

        override public void TurnOff()
        {
            if (IsRunning)
            {
                IsRunning = false;
                CurrentCycle = 0;
                StartTime = null;
                EndTime = null;
                Console.WriteLine("Dishwasher turned off.");
            }
            else
            {
                Console.WriteLine("Dishwasher is already off.");
            }
        }

    }
}