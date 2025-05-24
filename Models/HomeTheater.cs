using System;

namespace HomeController.Models
{
    public class HomeTheater : IDevice
    {
        public string DeviceId { get; set; }
        public string Name { get; set; }
        public bool IsPoweredOn { get; set; }
        public int VolumeLevel { get; set; }
        public string InputSource { get; set; }

        public string Ipaddr { get; set; }
        public DateTime LastUpdated { get; set; }

        public HomeTheater()
        {
        }

        public HomeTheater(string deviceId, string name)
        {
            DeviceId = deviceId;
            Name = name;
            IsPoweredOn = false;
            VolumeLevel = 50; // Default volume level
            InputSource = "HDMI1"; // Default input source
            LastUpdated = DateTime.Now;
        }

        public void PowerOn()
        {
            IsPoweredOn = true;
            LastUpdated = DateTime.Now;
        }

        public void PowerOff()
        {
            IsPoweredOn = false;
            LastUpdated = DateTime.Now;
        }

        override public void TurnOn()
        {
            if (!IsPoweredOn)
            {
                PowerOn();
            }
        }

        override public void TurnOff()
        {
            if (IsPoweredOn)
            {
                PowerOff();
            }
        }

        public void SetVolume(int level)
        {
            if (level < 0 || level > 100)
                throw new ArgumentOutOfRangeException(nameof(level), "Volume level must be between 0 and 100.");
            VolumeLevel = level;
            LastUpdated = DateTime.Now;
        }

        public void ChangeInputSource(string source)
        {
            InputSource = source;
            LastUpdated = DateTime.Now;
        }

        public override string ToString()
        {
            return $"HomeTheater: {Name} (ID: {DeviceId}), Powered On: {IsPoweredOn}, Volume: {VolumeLevel}, Input: {InputSource}";
        }
    }
}