using System;

namespace HomeController.Models
{
    public class AirPurifier : IDevice
    {
        public int FanSpeed { get; set; } // 1 to 5
        public double AirQuality { get; set; } // e.g., PM2.5 level
        public DateTime LastMaintenanceDate { get; set; }

        public override void TurnOn() { }

        public override void TurnOff() { }

        public void SetFanSpeed(int speed)
        {
            if (speed < 1 || speed > 5)
                throw new ArgumentOutOfRangeException(
                    nameof(speed),
                    "Fan speed must be between 1 and 5."
                );
            FanSpeed = speed;
        }

        public void UpdateAirQuality(double airQuality)
        {
            if (airQuality < 0)
                throw new ArgumentOutOfRangeException(
                    nameof(airQuality),
                    "Air quality must be a non-negative value."
                );
            AirQuality = airQuality;
        }

        public bool NeedsMaintenance()
        {
            return (DateTime.Now - LastMaintenanceDate).TotalDays > 180;
        }
    }
}
