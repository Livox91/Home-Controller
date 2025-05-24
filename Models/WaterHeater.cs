using System;

namespace HomeController.Models
{
    public class WaterHeater : IDevice
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsOn { get; set; }
        public double CurrentTemperature { get; set; }
        public double TargetTemperature { get; set; }
        public DateTime LastUpdated { get; set; }

        public string Ipaddr { get; set; }
        override public void TurnOn()
        {
            IsOn = true;
            LastUpdated = DateTime.Now;
        }

        override public void TurnOff()
        {
            IsOn = false;
            LastUpdated = DateTime.Now;
        }

        public void SetTargetTemperature(double temperature)
        {
            TargetTemperature = temperature;
            LastUpdated = DateTime.Now;
        }

        public override string ToString()
        {
            return $"WaterHeater: {Name}, IsOn: {IsOn}, CurrentTemp: {CurrentTemperature}, TargetTemp: {TargetTemperature}";
        }
    }
}