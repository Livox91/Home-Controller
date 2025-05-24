using System;

namespace HomeController.Models
{
    public static class DeviceFactory
    {
        public static IDevice CreateDevice(string deviceType)
        {
            switch (deviceType.ToLower())
            {
                case "aircooler":
                    return new AirCooler();
                case "airpurifier":
                    return new AirPurifier();
                case "dishwasher":
                    return new DishWasher();
                case "fan":
                    return new Fan();
                case "hometheater":
                    return new HomeTheater();
                case "light":
                    return new Light();
                case "microwave":
                    return new Microwave();
                case "television":
                    return new Television();
                case "washingmachine":
                    return new WashingMachine();
                case "waterheater":
                    return new WaterHeater();
                default:
                    throw new ArgumentException($"Unknown device type: {deviceType}");
            }
        }
    }
}
