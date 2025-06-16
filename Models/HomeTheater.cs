using System;

namespace HomeController.Models
{
    public class HomeTheater : IDevice
    {
        public int VolumeLevel { get; set; }
        public string InputSource { get; set; } = "HDMI1";

        public DateTime LastUpdated { get; set; }

        public override void TurnOn() { }

        public override void TurnOff() { }

        public void SetVolume(int level)
        {
            if (level < 0 || level > 100)
                throw new ArgumentOutOfRangeException(
                    nameof(level),
                    "Volume level must be between 0 and 100."
                );
            VolumeLevel = level;
            LastUpdated = DateTime.Now;
        }

        public void SetInputSource(string source)
        {
            InputSource = source;
            LastUpdated = DateTime.Now;
        }
    }
}
