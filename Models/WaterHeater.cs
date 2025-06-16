using System;

namespace HomeController.Models
{
    public class WaterHeater : IDevice
    {
        public double CurrentTemperature { get; set; }
        public double TargetTemperature { get; set; }
        public string LastUpdated { get; set; } = string.Empty;

        public override void TurnOn()
        {
            LastUpdated = DateTime.Now.ToString();
        }

        public override void TurnOff()
        {
            LastUpdated = DateTime.Now.ToString();
        }

        public void SetTargetTemperature(double temperature)
        {
            TargetTemperature = temperature;
            LastUpdated = DateTime.Now.ToString();
        }
    }
}
