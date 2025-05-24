using System;

namespace HomeController.Models
{
    public class AirPurifier : IDevice
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public bool IsOn { get; set; }
        public int FanSpeed { get; set; } // 1 to 5
        public double AirQuality { get; set; } // e.g., PM2.5 level
        public DateTime LastMaintenanceDate { get; set; }

        public string Ipaddr { get; set; }

        public AirPurifier()
        {
        }
        public AirPurifier(string id, string name)
        {
            Id = id;
            Name = name;
            IsOn = false;
            FanSpeed = 1;
            AirQuality = 0.0;
            LastMaintenanceDate = DateTime.Now;
        }

        override public void TurnOn()
        {
            IsOn = true;
        }

        override public void TurnOff()
        {
            IsOn = false;
        }

        public void SetFanSpeed(int speed)
        {
            if (speed < 1 || speed > 5)
                throw new ArgumentOutOfRangeException(nameof(speed), "Fan speed must be between 1 and 5.");
            FanSpeed = speed;
        }

        public void UpdateAirQuality(double airQuality)
        {
            if (airQuality < 0)
                throw new ArgumentOutOfRangeException(nameof(airQuality), "Air quality must be a non-negative value.");
            AirQuality = airQuality;
        }

        public bool NeedsMaintenance()
        {
            return (DateTime.Now - LastMaintenanceDate).TotalDays > 180;
        }

        public override string ToString()
        {
            return $"{Name} (ID: {Id}) - Status: {(IsOn ? "On" : "Off")}, Fan Speed: {FanSpeed}, Air Quality: {AirQuality}";
        }
    }
}