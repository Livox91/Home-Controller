namespace HomeController.Models
{
    public class WashingMachine : IDevice
    {
        public string Id { get; set; }
        public string Model { get; set; }
        public string Brand { get; set; }
        public bool IsRunning { get; set; }
        public int CurrentCycle { get; set; } // e.g., 0 = Off, 1 = Wash, 2 = Rinse, 3 = Spin
        public int WaterLevel { get; set; } // Percentage (0-100)
        public int LoadWeight { get; set; } // In kilograms
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public string Ipaddr { get; set; }
        public void Start(int cycle)
        {
            if (!IsRunning)
            {
                IsRunning = true;
                CurrentCycle = cycle;
                StartTime = DateTime.Now;
            }
        }

        public void Stop()
        {
            if (IsRunning)
            {
                IsRunning = false;
                CurrentCycle = 0;
                EndTime = DateTime.Now;
            }
        }

        public void UpdateWaterLevel(int level)
        {
            WaterLevel = level;
        }

        public void UpdateLoadWeight(int weight)
        {
            LoadWeight = weight;
        }

        override public void TurnOn()
        {
            if (!IsRunning)
            {
                IsRunning = true;
                CurrentCycle = 0; // Machine is on but not running a cycle
            }
        }

        override public void TurnOff()
        {
            if (IsRunning)
            {
                IsRunning = false;
                CurrentCycle = 0;
                EndTime = DateTime.Now;
            }
        }
    }
}